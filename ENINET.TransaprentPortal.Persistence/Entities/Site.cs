using System.ComponentModel.DataAnnotations;

namespace ENINET.TransaprentPortal.Persistence.Entities
{
    public class Site
    {
        [Key]

        public string Acronym { get; set; } = default!;
        public string Description { get; set; } = default!;

    }
}
