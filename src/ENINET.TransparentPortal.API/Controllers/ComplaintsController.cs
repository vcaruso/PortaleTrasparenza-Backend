using AutoMapper;
using ENINET.TransaprentPortal.Persistence.Configuration;
using ENINET.TransparentPortal.API.Dtos;
using ENINET.TransparentPortal.API.Dtos.Compliant;
using ENINET.TransparentPortal.API.Services.MailAuth;
using ENINET.TransparentPortal.Persistence.Configuration;
using ENINET.TransparentPortal.Persistence.Entities;
using ENINET.TransparentPortal.Repository.Contract;
using ENINET.TransparentPortal.Repository.Extension;


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ENINET.TransparentPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IMailAuth _mailAuth;

        public ComplaintsController(IRepositoryManager repository, IMapper mapper, IMailAuth mailAuth)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._mailAuth = mailAuth ?? throw new ArgumentNullException(nameof(mailAuth));
        }

        [HttpGet("list/{site}/{year}/{pageNum}/{pageSize}/{orderby}")]
        [Authorize(Roles = nameof(ApplicationPermissionConfiguration.VIEW_COMPLAINT))]
        public async Task<ApiResult<IList<ComplaintDto>>> GetComplaint(string site, string year, int pageNum = 1, int pageSize = 15, string orderby = $"Acronym,CreationDate")
        {
            var email = User.Claims.Where(t => t.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").FirstOrDefault();
            var authorizedSites = User.Claims.Where(t => t.Type == "TransparentSites").Select(s => s.Value).ToArray();

            int totalItems = 0;
            if (authorizedSites.Length > 0)
            {
                totalItems = await _repository.Compliant.FindByCondition(null, c => authorizedSites.Contains(c.Site.Acronym) && c.CreationDate.Year.ToString() == year, false).CountAsync();
                var items = await _repository.Compliant.FindByCondition(null, c => authorizedSites.Contains(c.Site.Acronym) && c.CreationDate.Year.ToString() == year, false)
                    .OrderBy(orderby)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                var numPages = (totalItems / pageSize) + (totalItems % pageSize != 0 ? 1 : 0);

                return new ApiResult<IList<ComplaintDto>>
                {
                    Data = _mapper.Map<List<ComplaintDto>>(items),
                    Message = "Ok",
                    StatusCode = StatusCodes.Status200OK,
                    PageInfo = new PagedInfo(totalItems, (int)numPages, pageSize, pageNum)
                };

            }
            throw new UnauthorizedAccessException();

        }

        [HttpGet("listunopened/{site}")]
        [Authorize(Roles = nameof(ApplicationPermissionConfiguration.VIEW_COMPLAINT))]
        public async Task<ApiResult<IList<ComplaintDto>>> GetComplaintUnopened(string site)
        {
            var email = User.Claims.Where(t => t.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").FirstOrDefault();
            var authorizedSites = User.Claims.Where(t => t.Type == "TransparentSites").Select(s => s.Value).ToArray();

            int totalItems = 0;
            if (authorizedSites.Length > 0)
            {

                var items = await _repository.Compliant.FindByCondition(null, c => authorizedSites.Contains(c.Site.Acronym) && c.OpenedDate == null && c.Acronym == site, false)
                    .OrderBy(o => o.CreationDate)
                    .ToListAsync();


                return new ApiResult<IList<ComplaintDto>>
                {
                    Data = _mapper.Map<List<ComplaintDto>>(items),
                    Message = "Ok",
                    StatusCode = StatusCodes.Status200OK,
                    PageInfo = null
                };

            }
            throw new UnauthorizedAccessException();

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
        public async Task<ApiResult<Guid>> AddCompliant([FromBody] AddComplaintDto compliantDto)
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
        [Authorize(Roles = nameof(ApplicationPermissionConfiguration.ADD_COMPLAINT_STEP))]
        public async Task<ApiResult<Guid>> AddStep([FromBody] AddComplaintStepDto stepDto)
        {

            var email = User.Claims.Where(t => t.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").FirstOrDefault();
            var authorizedSites = User.Claims.Where(t => t.Type == "TransparentSites").Select(s => s.Value).ToArray();
            var step = _mapper.Map<ComplaintStep>(stepDto);
            step.StepDate = DateTime.UtcNow;
            step.Operator = email.Value;
            var compliant = await _repository.Compliant.FindByCondition(null, c => c.ComplaintId == stepDto.ComplaintId, true).FirstOrDefaultAsync();
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
                compliant.CancelledDate = DateTime.UtcNow;
            }
            if (operation.OperationName == ComplaintOperationConfiguration.Opened)
            {
                compliant.OpenedDate = DateTime.UtcNow;
            }
            _repository.Save();

            return await Task.FromResult(new ApiResult<Guid> { Data = step.ResolutionId, Message = "Ok", StatusCode = StatusCodes.Status201Created });
        }

        [HttpGet("listavailableoperation/{complaint}")]
        [Authorize(Roles = nameof(ApplicationPermissionConfiguration.ADD_COMPLAINT_STEP))]
        public async Task<ApiResult<IList<ComplaintOperationDto>>> GetComplaintAvailableOperation(Guid complaint)
        {
            var complaintStep = await _repository.CompliantStep.FindByCondition(null, c => c.ComplaintId == complaint, false).ToListAsync();
            var operations = _repository.CompliantOperation.FindAll(false);
            var availableOperation = new List<ComplaintOperation>();

            if (!complaintStep.Any(o => o.OperationText == ComplaintOperationConfiguration.Canceled))
            {
                if (complaintStep.Count == 0) // Solo l'apertura è possibile
                {
                    var result = operations.Where(p => p.OperationName == ComplaintOperationConfiguration.Opened).FirstOrDefault();
                    if (result != null)
                    {
                        availableOperation.Add(result);
                    }
                }
                else
                {

                    if (!complaintStep.Any(o => o.OperationText == ComplaintOperationConfiguration.Canceled))
                    {
                        if (!complaintStep.Any(o => o.OperationText == ComplaintOperationConfiguration.Solved))
                        {
                            if (complaintStep.Any(o => o.OperationText == ComplaintOperationConfiguration.Opened))
                            {
                                var result = operations.Where(p => p.OperationName == ComplaintOperationConfiguration.Action || p.OperationName == ComplaintOperationConfiguration.Solved || p.OperationName == ComplaintOperationConfiguration.Canceled);

                                availableOperation.AddRange(result);

                            }
                        }
                    }
                }
            }
            var retVal = _mapper.Map<IList<ComplaintOperationDto>>(availableOperation);
            return new ApiResult<IList<ComplaintOperationDto>> { Data = retVal, Message = "Ok", StatusCode = StatusCodes.Status200OK };
        }

        [HttpGet("listoperations/{complaint}")]
        [Authorize(Roles = nameof(ApplicationPermissionConfiguration.ADD_COMPLAINT_STEP))]
        public async Task<ApiResult<IList<OperationStepDto>>> GetComplaintOperation(Guid complaint)
        {
            var email = User.Claims.Where(t => t.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").FirstOrDefault();
            var authorizedSites = User.Claims.Where(t => t.Type == "TransparentSites").Select(s => s.Value).ToArray();
            if (authorizedSites.Length > 0)
            {
                var result = await _repository.CompliantStep
                    .FindByCondition(null, c => c.ComplaintId == complaint && authorizedSites.Contains(c.Complaint.Acronym), false)
                    .Select(s => new OperationStepDto { Operation = s.Operation.OperationName, OperationText = s.OperationText, Operator = s.Operator, StepDate = s.StepDate, StepId = s.ResolutionId }).ToListAsync();

                return new ApiResult<IList<OperationStepDto>> { Data = result, Message = "Ok", StatusCode = StatusCodes.Status200OK };
            }
            throw new UnauthorizedAccessException();

        }

        [HttpDelete("deletestep/{complaintid}/{stepid}")]
        [Authorize(Roles = nameof(ApplicationPermissionConfiguration.DELETE_COMPLAINT_STEP))]
        public async Task<ApiResult<CommandResultDto>> DeleteStep(Guid complaintid, Guid stepid)
        {
            var email = User.Claims.Where(t => t.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").FirstOrDefault();
            var authorizedSites = User.Claims.Where(t => t.Type == "TransparentSites").Select(s => s.Value).ToArray();
            var step = await _repository.CompliantStep
                  .FindByCondition(i => i.Complaint, c => c.ComplaintId == complaintid && authorizedSites
                  .Contains(c.Complaint.Acronym), true)
                  .OrderByDescending(o => o.StepDate)
                  .FirstOrDefaultAsync();
            if (step != null)
            {
                _repository.CompliantStep.Delete(step);
                if (step.OperationText == ComplaintOperationConfiguration.Opened)
                {
                    step.Complaint.OpenedDate = null;
                }
                if (step.OperationText == ComplaintOperationConfiguration.Canceled)
                {
                    step.Complaint.CancelledDate = null;
                }
                if (step.OperationText == ComplaintOperationConfiguration.Solved)
                {
                    step.Complaint.ResolutionDate = null;
                }
                _repository.Save();
                return new ApiResult<CommandResultDto> { Data = new CommandResultDto("Delete", "OK"), Message = "OK", StatusCode = StatusCodes.Status200OK };

            }
            throw new BadHttpRequestException($"Step non authorized to delete!");
        }

    }
}
