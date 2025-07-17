using AutoMapper;
using ENINET.TransparentPortal.API.Dtos;
using ENINET.TransparentPortal.API.Dtos.Compliant;
using ENINET.TransparentPortal.API.Services.MailAuth;
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
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IMailAuth _mailAuth;

        public CompliantsController(IRepositoryManager repository, IMapper mapper, IMailAuth mailAuth)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._mailAuth = mailAuth ?? throw new ArgumentNullException(nameof(mailAuth));
        }

        [HttpPost("sendauthorization")]
        public Task<ApiResult<CommandResultDto>> GenerateAuthCode([FromBody] GuestAuthRequestDto request)
        {
            var guestAuth = _mapper.Map<GuestAuth>(request);
            if (String.IsNullOrEmpty(guestAuth.Email))
            {
                throw new BadHttpRequestException("Email is required", StatusCodes.Status400BadRequest);
            }
            if (!guestAuth.Email.Contains("@"))
            {
                throw new BadHttpRequestException("Email is not valid", StatusCodes.Status400BadRequest);
            }
            guestAuth.RandomCode = Guid.NewGuid();
            guestAuth.CreatedAt = DateTime.Now.ToUniversalTime();
            try
            {
                var result = _mailAuth.SendMail(guestAuth.Email, guestAuth.RandomCode);
                if (!result)
                {
                    return Task.FromResult(new ApiResult<CommandResultDto>
                    {
                        Data = new CommandResultDto("AuthRequest", "KO"),
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Message = "Error sending email"
                    });
                }
                _repository.GuestAuth.Create(guestAuth);
                _repository.Save();
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ApiResult<CommandResultDto>
                {
                    Data = new CommandResultDto(ex.Message, "KO"),
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Error sending email"
                });
            }
            return Task.FromResult(new ApiResult<CommandResultDto>
            {
                Data = new CommandResultDto("AuthRequest", "OK"),
                StatusCode = StatusCodes.Status200OK,
                Message = "Authorization code sent successfully"
            });
        }

        [HttpPost("add")]
        public async Task<ApiResult<Guid>> AddCompliant([FromBody] AddCompliantDto compliantDto)
        {
            var compliant = _mapper.Map<Complaint>(compliantDto);
            if (String.IsNullOrEmpty(compliantDto.Opener))
            {
                throw new BadHttpRequestException("Opener is required", StatusCodes.Status400BadRequest);
            }
            if (String.IsNullOrEmpty(compliantDto.Text))
            {
                throw new BadHttpRequestException("Text is required", StatusCodes.Status400BadRequest);
            }
            if (String.IsNullOrEmpty(compliantDto.RandomCode))
            {
                throw new BadHttpRequestException("Authorization Code Invalid", StatusCodes.Status400BadRequest);
            }
            var guestAuth = await _repository.GuestAuth.FindByCondition(null, c => c.RandomCode.ToString() == compliantDto.RandomCode, false).FirstOrDefaultAsync();
            if (guestAuth == null)
            {
                throw new BadHttpRequestException("Authorization Code Invalid", StatusCodes.Status400BadRequest);
            }
            compliant.CreationDate = DateTime.Now.ToUniversalTime();
            compliant.ComplaintId = Guid.NewGuid();


            _repository.Compliant.Create(compliant);
            _repository.Save();
            return await Task.FromResult(new ApiResult<Guid> { Data = compliant.ComplaintId, Message = "Ok", StatusCode = StatusCodes.Status201Created });

        }

        [HttpPost("addStep")]
        public async Task<ApiResult<Guid>> AddStep([FromBody] AddCompliantStepDto stepDto)
        {

            var email = User.Claims.Where(t => t.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").FirstOrDefault();
            var authorizedSites = User.Claims.Where(t => t.Type == "TransparentSites").Select(s => s.Value).ToArray();
            var step = _mapper.Map<ComplaintStep>(stepDto);
            var compliant = await _repository.Compliant.FindByCondition(null, c => c.ComplaintId == stepDto.ComplaintId, false).FirstOrDefaultAsync();
            var operation = await _repository.CompliantOperation.FindByCondition(null, c => c.OperationId == stepDto.OperationId, false).FirstOrDefaultAsync();
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
            _repository.CompliantStep.Create(step);
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
            _repository.Save();

            return await Task.FromResult(new ApiResult<Guid> { Data = step.ResolutionId, Message = "Ok", StatusCode = StatusCodes.Status201Created });
        }

    }
}
