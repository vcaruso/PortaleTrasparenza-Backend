namespace ENINET.TransparentPortal.API.Dtos.App
{
    public class FilePayloadDto
    {
        public string FileName { get; set; } = default!;
        public byte[] Content { get; set; } = default!;

    }
}
