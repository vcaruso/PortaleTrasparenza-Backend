using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENINET.TransaprentPortal.Persistence.Entities
{
    public class Element
    {
        [Key]
        public string Name { get; set; } = default!;
        [MaxLength(20)]
        public string Acronym { get; set; } = default!;
        [ForeignKey(nameof(Acronym))]
        public Site Site { get; set; } = default!;
    }
}
