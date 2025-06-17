using System.ComponentModel.DataAnnotations.Schema;

namespace ENINET.TransaprentPortal.Persistence.Entities
{
    public class SitesUser
    {


        public string Acronym { get; set; } = default!;

        public string UserId { get; set; } = default!;
        [ForeignKey(nameof(Acronym))]
        public Site Site { get; set; } = default!;
        [ForeignKey(nameof(UserId))]
        public ApplicationUser ApplicationUser { get; set; } = default!;


    }
}
