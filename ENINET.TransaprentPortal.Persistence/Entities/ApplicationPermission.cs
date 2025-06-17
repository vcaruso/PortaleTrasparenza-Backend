using System.ComponentModel.DataAnnotations;

namespace ENINET.TransaprentPortal.Persistence.Entities
{
    public class ApplicationPermission
    {
        [Key]
        public string Permission { get; set; } = default!;
        public string Description { get; set; } = default!;


    }
}
