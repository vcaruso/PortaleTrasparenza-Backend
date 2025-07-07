namespace ENINET.TransparentPortal.API.Dtos.App
{
    public record class AddReportDto(string acronym, int year, int month, int progressive, string element, IFormFile file);

}
