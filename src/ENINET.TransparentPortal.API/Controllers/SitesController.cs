using AnalisiHubApi.Dtos.App;
using AutoMapper;
using ENINET.TransaprentPortal.Persistence.Configuration;
using ENINET.TransaprentPortal.Persistence.Entities;
using ENINET.TransparentPortal.API.Dtos;
using ENINET.TransparentPortal.Repository.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ENINET.TransparentPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SitesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public SitesController(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet("list")]
        [Authorize(Roles = "Administrators,Contributors,Viewers")]
        [ProducesResponseType(typeof(IList<SiteDto>), 200)]
        public async Task<ApiResult<IList<SiteDto>>> GetSite()
        {
            var sites = await _repository.Site
                .FindAll(false)
                .OrderBy(o => o.Acronym)
                .ToListAsync();
            return new ApiResult<IList<SiteDto>> { Data = _mapper.Map<IList<SiteDto>>(sites), Message = "Ok", StatusCode = StatusCodes.Status200OK };
        }

        [HttpGet("listauthorized")]
        [Authorize(Roles = "Administrators,Contributors,Viewers")]
        [ProducesResponseType(typeof(IList<SiteDto>), 200)]
        public async Task<ApiResult<IList<SiteDto>>> GetSiteAuthorized()
        {
            var email = User.Claims.Where(t => t.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").FirstOrDefault();
            var authorizedSites = User.Claims.Where(t => t.Type == "TransparentSites").Select(s => s.Value).ToArray();
            if (authorizedSites != null)
            {
                var sites = await _repository.Site
                .FindByCondition(null, s => authorizedSites.Contains(s.Acronym), false)
                .OrderBy(o => o.Acronym)
                .ToListAsync();
                return new ApiResult<IList<SiteDto>> { Data = _mapper.Map<IList<SiteDto>>(sites), Message = "Ok", StatusCode = StatusCodes.Status200OK };
            }
            throw new BadHttpRequestException("User not has authorized site!");
        }



        [HttpPost("add")]
        [Authorize(Roles = nameof(ApplicationPermissionConfiguration.ADD_SITES))]
        [ProducesResponseType(typeof(SiteDto), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 400)]
        public async Task<ApiResult<SiteDto>> AddSite(SiteDto siteDto)
        {
            var site = _mapper.Map<Site>(siteDto);
            if (_repository.Site.Any(s => s.Acronym == siteDto.Acronym))
            {
                throw new BadHttpRequestException($"Already exists a Site with Acronym: {siteDto.Acronym}");
            }
            _repository.Site.Create(site);
            _repository.Save();
            return await Task.FromResult(new ApiResult<SiteDto> { Data = _mapper.Map<SiteDto>(site), Message = "Ok", StatusCode = StatusCodes.Status201Created, PageInfo = null });
        }

        [HttpDelete("delete/{acronym}")]
        [Authorize(Roles = nameof(ApplicationPermissionConfiguration.DELETE_SITES))]
        [ProducesResponseType(typeof(CommandResultDto), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 400)]
        public async Task<ApiResult<CommandResultDto>> DeleteSite(string acronym)
        {
            var site = await _repository.Site.FindByCondition(null, s => s.Acronym == acronym, true).FirstOrDefaultAsync();
            if (site != null)
            {
                _repository.Site.Delete(site!);
                _repository.Save();
                return new ApiResult<CommandResultDto> { Data = new CommandResultDto("Delete", "Cancellazione effettuata."), Message = "Ok", StatusCode = StatusCodes.Status200OK };
            }
            throw new BadHttpRequestException($"No site found with Acronym {acronym}");

        }
    }
}
