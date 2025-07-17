using ENINET.TransaprentPortal.Persistence.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENINET.TransparentPortal.Persistence.Entities
{
    public class Complaint
    {
        [Key]
        public Guid ComplaintId { get; set; }


        public Guid RandomCode { get; set; }

        public string Acronym { get; set; } = string.Empty;

        public string Opener { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public DateTime CreationDate { get; set; }
        public DateTime? OpenedDate { get; set; }
        public DateTime? CancelledDate { get; set; }

        public DateTime? ResolutionDate { get; set; }

        public List<ComplaintStep> Steps { get; set; } = new List<ComplaintStep>();

        [ForeignKey(nameof(RandomCode))]
        public GuestAuth GuestAuth { get; set; } = default!;

        [ForeignKey(nameof(Acronym))]
        public Site Site { get; set; } = default!;


    }
}
