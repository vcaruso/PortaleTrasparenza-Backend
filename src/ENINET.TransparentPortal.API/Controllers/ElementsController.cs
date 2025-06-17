using AutoMapper;
using ENINET.TransparentPortal.API.Dtos.App;
using ENINET.TransparentPortal.Repository.Contract;
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
        [HttpGet("elements")]
        public async Task<ApiResult<IList<ElementDto>>> GetElements()
        {
            var email = User.Claims.Where(t => t.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").FirstOrDefault();
            var authorizedSites = User.Claims.Where(t => t.Type == "TransparentSites").FirstOrDefault();
            var elements = await _repository.Element.FindByCondition(null, c => c.Acronym == authorizedSites!.Value, false).ToListAsync();
            return new ApiResult<IList<ElementDto>> { Data = _mapper.Map<List<ElementDto>>(elements), Message = "Ok", StatusCode = StatusCodes.Status200OK };
        }
    }
}
