namespace ENINET.TransparentPortal.API.Services.MailAuth
{
    public interface IMailAuth
    {
        bool SendMail(string to, Guid authorizationCode);
        Exception Exception { get; }
    }
}
