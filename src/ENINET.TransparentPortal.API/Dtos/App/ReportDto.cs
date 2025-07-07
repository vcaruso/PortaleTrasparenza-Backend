namespace ENINET.TransparentPortal.API.Dtos.App
{
    public class ReportDto
    {
        public string FileName { get; set; } = default!;
        public DateTime UploadDate { get; set; } = default!;
        public string UserUpload { get; set; } = default!;
        public string ElementName { get; set; } = default!;
        public string Year { get; set; } = default!;

        public string Month { get; set; } = default!;
        public string Progressive { get; set; } = default!;
        public long FileLength { get; set; } = default!;

        public string Acronym { get; set; } = default!;

    }
}
