using AutoMapper;
using ENINET.TransaprentPortal.Persistence.Entities;
using ENINET.TransparentPortal.API.Configuration;
using ENINET.TransparentPortal.API.Dtos;
using ENINET.TransparentPortal.API.Dtos.App;
using ENINET.TransparentPortal.API.Services.Storage;
using ENINET.TransparentPortal.API.Utility;
using ENINET.TransparentPortal.Repository.Contract;
using ENINET.TransparentPortal.Repository.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ENINET.TransparentPortal.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppController : ControllerBase
{
    private readonly IStorageManager _storageManager;
    private readonly IConfiguration _configuration;
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    private readonly StorageSettings _storageSettings = new StorageSettings();

    public AppController(IStorageManager storageManager, IConfiguration configuration, IRepositoryManager repository, IMapper mapper)
    {
        _storageManager = storageManager ?? throw new ArgumentNullException(nameof(storageManager));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _configuration.Bind("Storage", _storageSettings);
    }

    [HttpGet("reports/{element}/{year}/{pageNum}/{pageSize}")]
    public async Task<ApiResult<IList<ReportDto>>> GetReport(string element, string year, int pageNum = 1, int pageSize = 15)
    {
        var email = User.Claims.Where(t => t.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").FirstOrDefault();
        var authorizedSites = User.Claims.Where(t => t.Type == "TransparentSites").FirstOrDefault();
        var result = new List<Report>();
        int totalItems = 0;

        if (authorizedSites != null && !String.IsNullOrEmpty(authorizedSites.Value))
        {
            if (element != "*")
            {
                totalItems = await _repository.Report.FindByCondition(null, x => x.Acronym == authorizedSites.Value && x.ElementName == element && x.Year == year, false)
                    .OrderBy($"Element,Year,-Month,-Progressive")
                    .CountAsync();

                result = await _repository.Report.FindByCondition(null, x => x.Acronym == authorizedSites.Value && x.ElementName == element && x.Year == year, false)

                    .OrderBy($"Element,Year,-Month,-Progressive")

                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            else
            {
                totalItems = await _repository.Report.FindByCondition(null, x => x.Acronym == authorizedSites.Value && x.Year == year, false)
                    .OrderBy($"Element,Year-,Month,-Progressive")
                    .CountAsync();

                result = await _repository.Report.FindByCondition(null, x => x.Acronym == authorizedSites.Value && x.Year == year, false)
                    .OrderBy($"Element,Year,-Month,-Progressive")

                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            var numPages = (totalItems / pageSize) + (totalItems % pageSize != 0 ? 1 : 0);
            return new ApiResult<IList<ReportDto>>
            {
                Data = _mapper.Map<List<ReportDto>>(result),
                Message = "Ok",
                StatusCode = StatusCodes.Status200OK,
                PageInfo = new PagedInfo(totalItems, (int)numPages, pageSize, pageNum)
            };

        }
        throw new UnauthorizedAccessException();


    }

    [HttpPost("uploadsingle")]
    [Consumes("multipart/form-data")]
    [Authorize(Roles = "UPLOAD_REPORT")]
    public async Task<ApiResult<UploadResultDto>> UploadSingle([FromForm] AddReportDto formData)
    {
        if (formData.file.Length > 0 && formData.month > 0 && formData.year > 0 && !String.IsNullOrEmpty(formData.element) && formData.progressive > 0)
        {
            var email = User.Claims.Where(t => t.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").FirstOrDefault();
            var authorizedSites = User.Claims.Where(t => t.Type == "TransparentSites").FirstOrDefault();
            if (email != null)
            {
                var site = authorizedSites!.Value;
                var extension = Path.GetExtension(formData.file.FileName);
                var fileName = $"{site}-{formData.element}-{formData.progressive:00}-{formData.month:00}-{formData.year:0000}{extension}";
                var path = ReportUtility.SplitFileNameToPath(fileName);
                if (!_repository.Report.Any(p => p.FileName == fileName))
                {
                    _repository.Report.Create(new Report
                    {
                        Acronym = path.Site,
                        Year = path.Year,
                        Month = path.Month,
                        Progressive = path.Progressive,
                        UploadDate = DateTime.Now.ToUniversalTime(),
                        ElementName = path.Area,
                        FileName = fileName,
                        UserUpload = email.Value,
                        FileLength = formData.file.Length
                    });
                    await _storageManager.SaveFile(_storageSettings.Root, fileName, formData.file.OpenReadStream());
                    _repository.Save();
                    return new ApiResult<UploadResultDto> { Data = new UploadResultDto { Error = "", Esito = true, FileName = fileName }, Message = "Ok", StatusCode = StatusCodes.Status200OK };

                }
                else
                {
                    return new ApiResult<UploadResultDto> { Data = new UploadResultDto { Error = "Report già esistente. Non caricato.", Esito = false, FileName = fileName }, Message = "Ok", StatusCode = StatusCodes.Status200OK };

                }


            }
            throw new BadHttpRequestException("No User in Claims");
        }
        throw new BadHttpRequestException("No file or report parameters");


    }

    [HttpPost("uploadanalisi")]
    [Authorize(Roles = "UPLOAD_REPORT")]
    public async Task<ApiResult<IList<UploadResultDto>>> UploadAnalisi(List<IFormFile> files)
    {
        if (files.Count > 0)
        {
            var email = User.Claims.Where(t => t.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").FirstOrDefault();
            var authorizedSites = User.Claims.Where(t => t.Type == "TransparentSites").FirstOrDefault();
            if (email != null)
            {
                var result = new List<UploadResultDto>();
                foreach (var file in files)
                {

                    var site = authorizedSites!.Value;
                    var fileName = $"{site}-{file.FileName}";
                    var path = ReportUtility.SplitFileNameToPath(fileName);
                    if (!_repository.Report.Any(p => p.FileName == fileName))
                    {
                        _repository.Report.Create(new Report
                        {
                            Acronym = path.Site,
                            Year = path.Year,
                            Month = path.Month,
                            Progressive = path.Progressive,
                            UploadDate = DateTime.Now.ToUniversalTime(),
                            ElementName = path.Area,
                            FileName = fileName,
                            UserUpload = email.Value,
                            FileLength = file.Length
                        });
                        await _storageManager.SaveFile(_storageSettings.Root, fileName, file.OpenReadStream());
                        _repository.Save();
                        result.Add(new UploadResultDto { Error = "", Esito = true, FileName = fileName });
                    }
                    else
                    {
                        result.Add(new UploadResultDto { Error = "Report già esistente. Non caricato.", Esito = false, FileName = fileName });
                    }




                }

                return new ApiResult<IList<UploadResultDto>> { Data = result, Message = "File uploaded. See result for errors.", StatusCode = StatusCodes.Status200OK };
            }
            throw new BadHttpRequestException("No User in Claims");

        }
        throw new BadHttpRequestException("No file uploded");
    }

    [HttpGet("download/{filename}")]
    [Authorize(Roles = "DOWNLOAD_REPORT")]
    public async Task<ApiResult<FilePayloadDto>> Download(string fileName)
    {
        var payload = await _storageManager.Download(_storageSettings.Root, fileName);
        return new ApiResult<FilePayloadDto> { Data = new FilePayloadDto { Content = payload, FileName = fileName }, Message = "Ok", StatusCode = StatusCodes.Status200OK };
    }

    [HttpDelete("delete/{filename}")]
    [Authorize(Roles = "DELETE_REPORT")]
    public async Task<ApiResult<CommandResultDto>> Delete(string fileName)
    {
        var exist = await _repository.Report.FindByCondition(null, f => f.FileName == fileName, true).FirstOrDefaultAsync();
        if (exist != null)
        {
            _repository.Report.Delete(exist);
            await _storageManager.DeleteFile(_storageSettings.Root, fileName);
            _repository.Save();
            return new ApiResult<CommandResultDto> { Data = new CommandResultDto("Delete File", "Ok"), Message = "File cancellatto correttamente.", StatusCode = StatusCodes.Status200OK };
        }

        throw new BadHttpRequestException("File not found");


    }

}
