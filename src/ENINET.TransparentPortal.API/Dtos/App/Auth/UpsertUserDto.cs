namespace ENINET.TransparentPortal.API.Dtos.App.Auth;

public class UpsertUserDto
{
    public string UserId { get; set; } = default!;
    public string Nominativo { get; set; } = default!;
    public string[] Groups { get; set; } = [];
    public string[] Raffinerie { get; set; } = [];
}
