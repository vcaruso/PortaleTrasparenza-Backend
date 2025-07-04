using ENINET.TransaprentPortal.Persistence.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENINET.TransparentPortal.Persistence.Entities
{
    public class ElementSite
    {
        public int MonthlyReport { get; set; }
        public string ElementName { get; set; } = default!;
        public string Acronym { get; set; } = default!;
        [ForeignKey(nameof(Acronym))]
        public Site Site { get; set; } = default!;
        [ForeignKey(nameof(ElementName))]
        public Element Element { get; set; } = default!;
    }
}
