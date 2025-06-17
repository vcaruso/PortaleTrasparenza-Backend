using ENINET.TransparentPortal.Repository.Contract;

namespace ENINET.TransparentPortal.API.Services.AuthService;

public class AuthService : IAuthService
{

    private readonly IRepositoryManager _repository;
    private readonly ILogger<AuthService> _logger;
    private Dictionary<string, string[]> _permission;
    private string[] _userGroup;
    private string[] _userPermission;
    private string[] _authorizedSites;

    public AuthService(IRepositoryManager repository, ILogger<AuthService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this._logger = logger;
    }
    public Dictionary<string, string[]> GetGroupPermission()
    {


        if (_permission == null)
        {
            var retVal = new Dictionary<string, string[]>();
            var groups = _repository.ApplicationGroup.FindByCondition(p => p.GroupPermissions, p => true, false).Select(s => new { Permissions = s.GroupPermissions.Select(s => s.Permission).ToArray(), GroupName = s.GroupName });
            foreach (var g in groups)
            {
                retVal.Add(g.GroupName, g.Permissions.ToArray());
            }
            _permission = retVal;
        }



        return _permission;
    }

    public string[] GetUserPermission(string userid)
    {
        if (_userPermission == null)
        {
            var retVal = new List<string>();
            var permissions = _repository.UserGroup.FindByCondition(p => p.ApplicationGroup.GroupPermissions, z => z.Userid == userid, false).Select(s => s.ApplicationGroup.GroupPermissions.Select(z => z.Permission));
            foreach (var permission in permissions) retVal.AddRange(permission);
            _userPermission = retVal.ToArray();
        }

        return _userPermission;
    }
    public string[] GetUserGroup(string userid)
    {
        if (_userGroup == null)
        {
            try
            {
                _userGroup = _repository.UserGroup.FindByCondition(null, z => z.Userid == userid, false).GroupBy(g => g.GroupName).Select(s => s.Key).ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Errore: {ex.Message}");
                _userGroup = new string[] { };
            }

        }

        return _userGroup;
    }
    public string[] GetAuthorizedSites(string userid)
    {
        if (_authorizedSites == null)
        {
            try
            {
                _authorizedSites = _repository.SitesUser.FindByCondition(null, z => z.UserId == userid, false).GroupBy(g => g.Acronym).Select(s => s.Key).ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Errore: {ex.Message}");
                _authorizedSites = new string[] { };
            }

        }

        return _authorizedSites;
    }
}
