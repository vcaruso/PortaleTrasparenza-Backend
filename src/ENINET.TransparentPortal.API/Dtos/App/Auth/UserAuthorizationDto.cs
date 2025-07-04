namespace ENINET.TransparentPortal.API.Dtos.App.Auth;

public class UserAuthorizationDto
{
    public string Userid { get; set; } = default!;
    public string[] Sites { get; set; } = default!;
    public string[] Gruppi { get; set; } = default!;
    public string[] Permessi { get; set; } = default!;

}
