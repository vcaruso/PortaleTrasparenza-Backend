using AutoMapper;
using ENINET.TransparentPortal.API.Dtos.Compliant;
using ENINET.TransparentPortal.Persistence.Configuration;
using ENINET.TransparentPortal.Persistence.Entities;
using ENINET.TransparentPortal.Repository.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ENINET.TransparentPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompliantsController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;

        public CompliantsController(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<ApiResult<Guid>> AddCompliant([FromBody] AddCompliantDto compliantDto)
        {
            var compliant = mapper.Map<Complaint>(compliantDto);
            if (String.IsNullOrEmpty(compliantDto.Opener))
            {
                throw new BadHttpRequestException("Opener is required", StatusCodes.Status400BadRequest);
            }
            if (String.IsNullOrEmpty(compliantDto.Text))
            {
                throw new BadHttpRequestException("Text is required", StatusCodes.Status400BadRequest);
            }
            compliant.CreationDate = new DateTime();
            compliant.ComplaintId = Guid.NewGuid();
            repository.Compliant.Create(compliant);
            repository.Save();
            return await Task.FromResult(new ApiResult<Guid> { Data = compliant.ComplaintId, Message = "Ok", StatusCode = StatusCodes.Status201Created });

        }

        [HttpPost]
        public async Task<ApiResult<Guid>> AddStep([FromBody] AddCompliantStepDto stepDto)
        {

            var email = User.Claims.Where(t => t.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").FirstOrDefault();
            var authorizedSites = User.Claims.Where(t => t.Type == "TransparentSites").Select(s => s.Value).ToArray();
            var step = mapper.Map<ComplaintStep>(stepDto);
            var compliant = await repository.Compliant.FindByCondition(null, c => c.ComplaintId == stepDto.ComplaintId, false).FirstOrDefaultAsync();
            var operation = await repository.CompliantOperation.FindByCondition(null, c => c.OperationId == stepDto.OperationId, false).FirstOrDefaultAsync();
            if (compliant == null)
            {
                throw new BadHttpRequestException("Compliant not found.", StatusCodes.Status404NotFound);
            }
            if (!authorizedSites.Contains(compliant.Acronym))
            {
                throw new BadHttpRequestException("You are not authorized to add steps to this compliant.", StatusCodes.Status403Forbidden);
            }
            if (operation == null)
            {
                throw new BadHttpRequestException("Operation not found.", StatusCodes.Status404NotFound);
            }
            if (String.IsNullOrEmpty(step.OperationText) && (operation.OperationName != ComplaintOperationConfiguration.Solved || operation.OperationName != ComplaintOperationConfiguration.Canceled || operation.OperationName != ComplaintOperationConfiguration.Opened))
            {
                throw new BadHttpRequestException("Operation text is required", StatusCodes.Status400BadRequest);
            }
            if (compliant.OpenedDate == null && operation.OperationName != ComplaintOperationConfiguration.Opened)
            {
                throw new BadHttpRequestException("Compliant must be open first.", StatusCodes.Status400BadRequest);
            }
            if (compliant.OpenedDate != null && operation.OperationName == ComplaintOperationConfiguration.Opened)
            {
                throw new BadHttpRequestException("Compliant is already opened.", StatusCodes.Status400BadRequest);
            }
            if (compliant.ResolutionDate != null)
            {
                throw new BadHttpRequestException("Compliant is already resolved.", StatusCodes.Status400BadRequest);
            }
            if (compliant.CancelledDate != null)
            {
                throw new BadHttpRequestException("Compliant state is cancelled.", StatusCodes.Status400BadRequest);
            }
            compliant.ComplaintId = compliant.ComplaintId;
            repository.CompliantStep.Create(step);
            if (operation.OperationName == ComplaintOperationConfiguration.Solved)
            {
                compliant.ResolutionDate = step.StepDate;
            }
            if (operation.OperationName == ComplaintOperationConfiguration.Canceled)
            {
                compliant.CancelledDate = DateTime.Now;
            }
            if (operation.OperationName == ComplaintOperationConfiguration.Opened)
            {
                compliant.OpenedDate = DateTime.Now;
            }
            repository.Save();

            return await Task.FromResult(new ApiResult<Guid> { Data = step.ResolutionId, Message = "Ok", StatusCode = StatusCodes.Status201Created });
        }

    }
}
