using AutoMapper;
using ENINET.TransaprentPortal.Persistence.Entities;
using ENINET.TransparentPortal.API.Dtos;
using ENINET.TransparentPortal.API.Dtos.App;
using ENINET.TransparentPortal.Persistence.Entities;
using ENINET.TransparentPortal.Repository.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ENINET.TransparentPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ElementsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repository;

        public ElementsController(IMapper mapper, IRepositoryManager repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// Recupera gli elementi del sito se il sito è tra quelli autorizzati per l'utente
        /// </summary>
        /// <param name="acronym"></param>
        /// <returns></returns>
        /// <exception cref="BadHttpRequestException"></exception>
        [HttpGet("list/{acronym}")]
        [Authorize(Roles = "VIEW_ELEMENTS")]
        [ProducesResponseType(typeof(ApiResult<IList<ElementDto>>), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 400)]
        public async Task<ApiResult<IList<ElementDto>>> GetElements(string acronym)
        {
            var email = User.Claims.Where(t => t.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").FirstOrDefault();
            var authorizedSites = User.Claims.Where(t => t.Type == "TransparentSites").Select(s => s.Value).ToArray();
            if (authorizedSites != null)
            {
                var elements = await _repository.ElementSite.FindByCondition(null, c => authorizedSites.Contains(c.Acronym) && c.Acronym == acronym, false)
                    .OrderBy(o => o.ElementName)
                    .ToListAsync();
                return new ApiResult<IList<ElementDto>> { Data = _mapper.Map<List<ElementDto>>(elements), Message = "Ok", StatusCode = StatusCodes.Status200OK };
            }
            throw new BadHttpRequestException("User not has authorized site!");

        }
        /// <summary>
        /// Recupera tutti gli elementi per i siti autorizzati per l'utente
        /// </summary>
        /// <returns></returns>
        /// <exception cref="BadHttpRequestException"></exception>
        [HttpGet("list")]
        [Authorize(Roles = "VIEW_ELEMENTS")]
        [ProducesResponseType(typeof(ApiResult<IList<ElementDto>>), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 400)]
        public async Task<ApiResult<IList<ElementDto>>> GetElements()
        {
            var email = User.Claims.Where(t => t.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").FirstOrDefault();
            var authorizedSites = User.Claims.Where(t => t.Type == "TransparentSites").Select(s => s.Value).ToArray();
            if (authorizedSites != null)
            {
                var elements = await _repository.ElementSite.FindByCondition(null, c => authorizedSites.Contains(c.Acronym), false)
                    .OrderBy(o => o.ElementName)
                    .ToListAsync();
                return new ApiResult<IList<ElementDto>> { Data = _mapper.Map<List<ElementDto>>(elements), Message = "Ok", StatusCode = StatusCodes.Status200OK };
            }
            throw new BadHttpRequestException("User not has authorized site!");

        }

        /// <summary>
        /// Aggiunge un nuovo elemento
        /// </summary>
        /// <param name="elementName"></param>
        /// <returns></returns>
        /// <exception cref="BadHttpRequestException"></exception>
        [HttpPost("add")]
        [Authorize(Roles = "ADD_ELEMENTS")]
        [ProducesResponseType(typeof(ApiResult<ElementDto>), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 400)]
        public async Task<ApiResult<ElementDto>> AddElements(ElementDto elementName)
        {
            var email = User.Claims.Where(t => t.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").FirstOrDefault();
            var authorizedSites = User.Claims.Where(t => t.Type == "TransparentSites");
            if (authorizedSites.Where(a => a.Value == elementName.acronym).FirstOrDefault() != null)
            {
                var elementSite = _mapper.Map<ElementSite>(elementName);
                if (_repository.ElementSite.Any(e => e.ElementName.ToLower() == elementSite.ElementName.ToLower() && e.Acronym == elementSite.Acronym))
                {
                    throw new BadHttpRequestException($"Element {elementSite.ElementName} already exists on site {elementSite.Acronym}.");
                }
                var element = _repository.Element.FindByCondition(null, e => e.Name.ToLower() == elementName.elementName.ToLower(), true).FirstOrDefault();
                if (element == null)
                {
                    _repository.Element.Create(new Element { Name = elementName.elementName.ToLower() });
                }
                _repository.ElementSite.Create(elementSite);
                _repository.Save();
                return await Task.FromResult(new ApiResult<ElementDto> { Data = _mapper.Map<ElementDto>(elementSite), Message = "Ok", StatusCode = StatusCodes.Status201Created });
            }
            throw new BadHttpRequestException($"User not is authorized to add element to site {elementName.acronym}!");

        }

        [HttpDelete("delete/{elementName}")]
        [Authorize(Roles = "DELETE_ELEMENTS")]
        [ProducesResponseType(typeof(ApiResult<ElementDto>), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 400)]
        public async Task<ApiResult<CommandResultDto>> DeleteElements(string elementName)
        {
            var email = User.Claims.Where(t => t.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").FirstOrDefault();
            var authorizedSites = User.Claims.Where(t => t.Type == "TransparentSites").FirstOrDefault();
            if (authorizedSites != null)
            {
                var elementSite = await _repository.ElementSite.FindByCondition(null, e => e.ElementName == elementName && e.Acronym == authorizedSites.Value, true).ToListAsync();

                if (elementSite.Count() == 0)
                {
                    throw new BadHttpRequestException($"Element {elementName} Non trovato.");
                }
                else
                {
                    var es = elementSite.FirstOrDefault();
                    if (es != null)
                    {
                        _repository.ElementSite.Delete(es);
                        _repository.Save();
                        var elementRemaining = await _repository.ElementSite.FindByCondition(null, e => e.ElementName.ToLower() == elementName.ToLower(), true).CountAsync();
                        if (elementRemaining == 0)
                        {

                            var element = _repository.Element.FindByCondition(null, e => e.Name.ToLower() == elementName.ToLower(), true).FirstOrDefault();
                            if (element != null)
                            {
                                _repository.Element.Delete(element);
                                _repository.Save();
                            }
                        }

                    }


                }



                return await Task.FromResult(new ApiResult<CommandResultDto> { Data = new CommandResultDto("Delete", "Ok"), Message = "Elemento cancellato.", PageInfo = null, StatusCode = StatusCodes.Status200OK });
            }
            throw new BadHttpRequestException("User not has authorized site!");

        }
    }
}
