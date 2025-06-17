namespace ENINET.TransparentPortal.API.Services.AuthService;

public interface IAuthService
{
    Dictionary<string, string[]> GetGroupPermission();
    public string[] GetUserPermission(string userid);
    public string[] GetUserGroup(string userid);

    public string[] GetAuthorizedSites(string userid);

}