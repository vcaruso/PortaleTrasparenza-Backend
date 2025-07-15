using System.ComponentModel.DataAnnotations;

namespace ENINET.TransparentPortal.Persistence.Entities
{
    public class Complaint
    {
        [Key]
        public Guid ComplaintId { get; set; }

        public string Acronym { get; set; } = string.Empty;

        public string Opener { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public DateTime CreationDate { get; set; }
        public DateTime? OpenedDate { get; set; }
        public DateTime? CancelledDate { get; set; }

        public DateTime? ResolutionDate { get; set; }

        public List<ComplaintStep> Steps { get; set; } = new List<ComplaintStep>();


    }
}
