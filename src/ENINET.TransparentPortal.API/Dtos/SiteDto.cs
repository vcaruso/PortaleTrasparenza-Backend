namespace AnalisiHubApi.Dtos.App
{
    public class SiteDto
    {
        public string Acronym { get; set; } = default!;
        public string Description { get; set; } = default!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
