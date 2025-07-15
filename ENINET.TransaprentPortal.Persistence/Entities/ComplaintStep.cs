using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENINET.TransparentPortal.Persistence.Entities
{
    public class ComplaintStep
    {
        [Key]
        public Guid ResolutionId { get; set; }

        public Guid ComplaintId { get; set; }

        public DateTime StepDate { get; set; }

        public string Operator { get; set; } = string.Empty;

        public Guid OperationId { get; set; }

        public string OperationText { get; set; } = string.Empty;

        [ForeignKey(nameof(ComplaintId))]
        public Complaint Complaint { get; set; } = null!;

        [ForeignKey(nameof(OperationId))]
        public ComplaintOperation Operation { get; set; } = null!;

    }
}
