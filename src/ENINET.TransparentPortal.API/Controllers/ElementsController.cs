using AutoMapper;
using ENINET.TransparentPortal.API.Dtos;
using ENINET.TransparentPortal.API.Dtos.App;
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
        [HttpGet("list")]
        [Authorize(Roles = "VIEW_ELEMENTS")]
        [ProducesResponseType(typeof(ApiResult<IList<ElementDto>>), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 400)]
        public async Task<ApiResult<IList<ElementDto>>> GetElements()
        {
            var email = User.Claims.Where(t => t.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").FirstOrDefault();

            var authorizedSites = User.Claims.Where(t => t.Type == "TransparentSites").FirstOrDefault();
            if (authorizedSites != null)
            {
                var elements = await _repository.ElementSite.FindByCondition(null, c => c.Acronym == authorizedSites.Value, false).ToListAsync();
                return new ApiResult<IList<ElementDto>> { Data = _mapper.Map<List<ElementDto>>(elements), Message = "Ok", StatusCode = StatusCodes.Status200OK };
            }
            throw new BadHttpRequestException("User not has authorized site!");

        }
        /*
        [HttpPost("add")]
        [Authorize(Roles = "ADD_ELEMENTS")]
        [ProducesResponseType(typeof(ApiResult<ElementDto>), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 400)]
        public async Task<ApiResult<ElementDto>> AddElements(ElementDto elementName)
        {
            var email = User.Claims.Where(t => t.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").FirstOrDefault();
            var authorizedSites = User.Claims.Where(t => t.Type == "TransparentSites").FirstOrDefault();
            if (authorizedSites != null)
            {
                var element = _mapper.Map<Element>(elementName);
                element.Acronym = authorizedSites.Value;
                if (_repository.Element.Any(e => e.Name.ToLower() == element.Name.ToLower() && e.Acronym == authorizedSites.Value))
                {
                    throw new BadHttpRequestException($"Element {element.Name} already exists on site {authorizedSites.Value}.");
                }
                _repository.Element.Create(element);
                _repository.Save();
                return await Task.FromResult(new ApiResult<ElementDto> { Data = _mapper.Map<ElementDto>(element), Message = "Ok", StatusCode = StatusCodes.Status201Created });
            }
            throw new BadHttpRequestException("User not has authorized site!");

        }*/

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
                        var elementRemaining = await _repository.ElementSite.FindByCondition(null, e => e.ElementName.ToLower() == elementName.ToLower(), true).ToListAsync();
                        if (!_repository.ElementSite.Any(e => e.ElementName.ToLower() == elementName.ToLower()))
                        {
                            var element = _repository.Element.FindByCondition(null, e => e.Name.ToLower() == elementName.ToLower(), true).FirstOrDefault();
                            if (element != null)
                            {
                                _repository.Element.Delete(element);
                            }
                        }
                        _repository.Save();
                    }


                }



                return await Task.FromResult(new ApiResult<CommandResultDto> { Data = new CommandResultDto("Delete", "Ok"), Message = "Elemento cancellato.", PageInfo = null, StatusCode = StatusCodes.Status200OK });
            }
            throw new BadHttpRequestException("User not has authorized site!");

        }
    }
}
