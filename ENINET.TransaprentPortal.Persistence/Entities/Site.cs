using ENINET.TransparentPortal.Persistence.Entities;
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

        public IList<Complaint> Complaints { get; set; } = default!;
        public IList<ElementSite> ElementsSite { get; set; } = default!;
        public IList<SitesUser> SitesUsers { get; set; } = default!;


        public IList<Report> Reports { get; set; } = default!;






    }
}
