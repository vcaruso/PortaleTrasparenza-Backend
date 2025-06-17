using AnalisiHubApi.Dtos.App;
using AutoMapper;
using ENINET.TransaprentPortal.Persistence.Entities;
using ENINET.TransparentPortal.API.Controllers;
using ENINET.TransparentPortal.API.Dtos;
using ENINET.TransparentPortal.API.Dtos.App.Auth;
using ENINET.TransparentPortal.Repository.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ENINET.TransparentPortal.API.Controlles;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public AuthController(IRepositoryManager repository, IMapper mapper)
    {
        if (repository is null)
        {
            throw new ArgumentNullException(nameof(repository));
        }

        if (mapper is null)
        {
            throw new ArgumentNullException(nameof(mapper));
        }

        this._repository = repository;
        this._mapper = mapper;
    }
    [ProducesResponseType(typeof(ApiResult<UserAuthorizationDto>), 200)]
    [ProducesResponseType(typeof(ApiResult<string>), 401)]
    [ProducesResponseType(typeof(ApiResult<string>), 500)]
    [SwaggerOperation(Summary = "Ritorna le autorizzazioni dell'utente")]
    //[Authorize(Policy = "AuthZPolicy")]
    [Authorize(Roles = "Administrators,Users")]
    [HttpGet("authorization")]
    public ApiResult<UserAuthorizationDto> GetUserAuthorization()
    {
        var email = User.Claims.Where(t => t.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").FirstOrDefault();
        if (email != null)
        {

            var userApp = _repository.ApplicationUser.FindByCondition(q => q.UserGroups, p => p.UserId == email.Value, false).FirstOrDefault();

            if (userApp != null)
            {
                var raffineries = _repository.SitesUser.FindByCondition(q => q.Site, p => p.UserId == email.Value, false).Select(s => s.Site.Acronym).ToList();
                var groups = userApp.UserGroups.Select(s => s.GroupName).ToList();
                var permissions = new List<string>();

                foreach (var group in userApp.UserGroups)
                {
                    var permissionsGroup = _repository.ApplicationGroup
                        .FindByCondition(i => i.GroupPermissions, f => f.GroupName == group.GroupName, false)
                        .Select(s => s.GroupPermissions.Select(s => s.Permission)).ToList()[0].ToList();
                    permissions.AddRange(permissionsGroup);
                }
                permissions = permissions.Distinct().ToList();
                return new ApiResult<UserAuthorizationDto>
                {
                    Data = new UserAuthorizationDto { Gruppi = groups.ToArray(), Permessi = permissions.ToArray(), Raffinerie = raffineries.ToArray(), Userid = email.Value },
                    StatusCode = StatusCodes.Status200OK,
                    Message = "OK"
                };
            }
        }
        throw new UnauthorizedAccessException();
    }
    /*
    [ProducesResponseType(typeof(ApiResult<UserAuthorizationDto>), 200)]
    [ProducesResponseType(typeof(ApiResult<string>), 401)]
    [ProducesResponseType(typeof(ApiResult<string>), 500)]
    [SwaggerOperation(Summary = "Aggiunge un utente alle Raffinerie")]
    [Authorize(Policy = "AuthZPolicy")]*/
    [HttpPost("upsertuser")]
    public ApiResult<CommandResultDto> UpsertRaffinerieUser([FromBody] UpsertUserDto utente)
    {
        if (String.IsNullOrEmpty(utente.UserId))
        {
            throw new BadHttpRequestException("Utente vuoto", 401);
        }
        var userDb = _repository.ApplicationUser.FindByCondition(null, p => p.UserId.ToLower() == utente.UserId.ToLower(), true).FirstOrDefault();
        if (userDb == null)
        {
            _repository.ApplicationUser.Create(new ApplicationUser { UserId = utente.UserId, UserName = utente.Nominativo });
        }
        else
        {
            if (userDb.UserName != utente.Nominativo) userDb.UserName = utente.Nominativo;
        }
        //Rimuoviamo quelli non più autorizzati
        var toRemove = _repository.UserGroup.FindByCondition(null, p => p.Userid == utente.UserId.ToLower() && !utente.Groups.Contains(p.GroupName), true);
        foreach (var remove in toRemove) _repository.UserGroup.Delete(remove);

        foreach (var gruppo in utente.Groups)
        {
            if (!_repository.UserGroup.Any(p => p.Userid == utente.UserId.ToLower() && p.GroupName == gruppo))
                _repository.UserGroup.Create(new UserGroup { Userid = utente.UserId.ToLower(), GroupName = gruppo });
        }

        var toRemoveRaff = _repository.SitesUser.FindByCondition(null, p => p.UserId == utente.UserId.ToLower() && !utente.Raffinerie.Contains(p.Acronym), true);

        foreach (var remove in toRemoveRaff) _repository.SitesUser.Delete(remove);

        foreach (var raffineria in utente.Raffinerie)
        {
            if (!_repository.SitesUser.Any(p => p.Acronym == raffineria && p.UserId == utente.UserId.ToLower()))
            {
                _repository.SitesUser.Create(new SitesUser { Acronym = raffineria, UserId = utente.UserId.ToLower() });
            }

        }

        _repository.Save();
        return new ApiResult<CommandResultDto> { Data = new CommandResultDto("Upsert", "Comando riuscito"), Message = "OK", StatusCode = StatusCodes.Status200OK };
    }
    [HttpDelete("delete/user/{userid}")]
    public ApiResult<CommandResultDto> DeleteUser(string userid)
    {
        if (_repository.SitesUser.Any(p => p.UserId == userid.ToLower()) || _repository.UserGroup.Any(p => p.Userid == userid.ToLower()))
        {
            throw new BadHttpRequestException("L'utente ha ancora gruppi o è abilitato a raffinerie!");
        }
        _repository.ApplicationUser.Delete(_repository.ApplicationUser.FindByCondition(null, p => p.UserId == userid.ToLower(), true).FirstOrDefault()!);
        _repository.Save();
        return new ApiResult<CommandResultDto> { Data = new CommandResultDto("Delete User", "Comando riuscito"), Message = "OK", StatusCode = StatusCodes.Status200OK };
    }

    [ProducesResponseType(typeof(ApiResult<SiteDto[]>), 200)]
    [ProducesResponseType(typeof(ApiResult<string>), 400)]
    [ProducesResponseType(typeof(ApiResult<string>), 500)]
    [SwaggerOperation(Summary = "Ritorna l'elenco delle Raffinerie ")]
    [HttpGet("raffinerie")]
    [Authorize(Policy = "AuthZPolicy")]
    public ApiResult<SiteDto[]> GeSites()
    {

        var raffinerie = _repository.Site.FindAll(false);
        return new ApiResult<SiteDto[]> { Data = _mapper.Map<SiteDto[]>(raffinerie), StatusCode = StatusCodes.Status200OK, Message = "OK" };


    }



    [ProducesResponseType(typeof(ApiResult<UserAuthorizationDto>), 200)]
    [ProducesResponseType(typeof(ApiResult<string>), 401)]
    [ProducesResponseType(typeof(ApiResult<string>), 500)]
    [SwaggerOperation(Summary = "Ritorna tutti i gruppi autorizzativi")]
    [Authorize(Policy = "AuthZPolicy")]
    [HttpGet("groups")]
    public ApiResult<IList<ApplicationGroupDto>> GetGroups()
    {
        var groups = _repository.ApplicationGroup
            .FindAll(false)
            .Select(s => new ApplicationGroupDto(s.GroupName, s.GroupDescription))
            .ToList()
            .OrderBy(o => o.groupName)
            .ToList();
        return new ApiResult<IList<ApplicationGroupDto>> { Data = groups, Message = "OK", StatusCode = StatusCodes.Status200OK };
    }
    [ProducesResponseType(typeof(ApiResult<UserAuthorizationDto>), 200)]
    [ProducesResponseType(typeof(ApiResult<string>), 401)]
    [ProducesResponseType(typeof(ApiResult<string>), 500)]
    [SwaggerOperation(Summary = "Ritorna tutti i gruppi applicativi dell'utente")]
    [Authorize(Policy = "AuthZPolicy")]
    [HttpGet("usergroups/{userid}")]
    public ApiResult<IList<ApplicationGroupDto>> GetUserGroups(string userid)
    {
        var groups = _repository.UserGroup
            .FindByCondition(i => i.ApplicationGroup, p => p.Userid == userid, false)
            .Select(s => new ApplicationGroupDto(s.GroupName, s.ApplicationGroup.GroupDescription))
            .ToList()
            .OrderBy(o => o.groupName)
            .ToList();
        return new ApiResult<IList<ApplicationGroupDto>> { Data = groups, Message = "OK", StatusCode = StatusCodes.Status200OK };
    }

    [ProducesResponseType(typeof(ApiResult<UserAuthorizationDto>), 200)]
    [ProducesResponseType(typeof(ApiResult<string>), 401)]
    [ProducesResponseType(typeof(ApiResult<string>), 500)]
    [SwaggerOperation(Summary = "Ritorna tutti gli utenti applicativi")]
    [Authorize(Policy = "AuthZPolicy")]
    [HttpGet("users")]
    public ApiResult<IList<ApplicationUserDto>> GetUsers()
    {
        var users = _repository.ApplicationUser
            .FindAll(false)
            .Select(s => new ApplicationUserDto(s.UserId, s.UserName,
            String.Join(",", s.UserGroups.Select(s => s.GroupName).ToArray()), String.Join(",", s.SitesUsers.Select(s => s.Acronym).ToArray())
            )).ToList().OrderBy(o => o.nominativo).ToList();
        return new ApiResult<IList<ApplicationUserDto>> { Data = users, Message = "OK", StatusCode = StatusCodes.Status200OK };
    }

}
