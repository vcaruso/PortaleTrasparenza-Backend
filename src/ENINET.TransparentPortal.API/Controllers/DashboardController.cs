using ENINET.TransparentPortal.API.Dtos.App;
using ENINET.TransparentPortal.Repository.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ENINET.TransparentPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IRepositoryManager _repository;

        public DashboardController(IRepositoryManager repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        [HttpGet("reportformonths/{acronym}/{year}/{elementName}")]

        [ProducesResponseType(typeof(ApiResult<IList<ElementStatisticDto>>), 200)]
        public async Task<ApiResult<IList<ElementStatisticDto>>> GetYearReport(string acronym, int year, string elementName)
        {
            var email = User.Claims.Where(t => t.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").FirstOrDefault();
            var authorizedSites = User.Claims.Where(t => t.Type == "TransparentSites").Select(s => s.Value).ToArray();
            var statisticsReports = await _repository.Report.FindByCondition(null, r => r.Acronym.ToLower() == acronym.ToLower() && authorizedSites.Contains(r.Acronym) && r.Year == year.ToString(), false).ToListAsync();
            var elements = await _repository.ElementSite.FindByCondition(null, r => r.Acronym.ToLower() == acronym.ToLower() && authorizedSites.Contains(r.Acronym) && r.ElementName == elementName, false).ToListAsync();
            var statistics = new List<ElementStatisticDto>();
            var months = Enumerable.Range(1, 12);
            foreach (var element in elements)
            {
                var values = new List<int>();
                foreach (var month in months)
                {

                    values.Add(statisticsReports.Where(e => int.Parse(e.Month) == month && e.ElementName == element.ElementName).Count());
                }
                statistics.
                       Add(new ElementStatisticDto(element.ElementName, values));
            }
            return new ApiResult<IList<ElementStatisticDto>> { Data = statistics, Message = "Ok", StatusCode = StatusCodes.Status200OK };
        }
    }
}
