namespace ENINET.TransparentPortal.API.Dtos.App
{
    public class UploadResultDto
    {

        public string FileName { get; set; } = default!;
        public bool Esito { get; set; }

        public string Error { get; set; } = default!;

    }
}
