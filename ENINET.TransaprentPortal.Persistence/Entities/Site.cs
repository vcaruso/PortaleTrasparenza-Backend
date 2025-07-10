using System.ComponentModel.DataAnnotations;

namespace ENINET.TransaprentPortal.Persistence.Entities
{
    public class Site
    {
        [Key]

        public string Acronym { get; set; } = default!;
        public string Description { get; set; } = default!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}
