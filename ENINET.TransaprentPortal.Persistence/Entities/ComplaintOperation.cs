using System.ComponentModel.DataAnnotations;

namespace ENINET.TransparentPortal.Persistence.Entities
{
    public class ComplaintOperation
    {
        [Key]
        public Guid OperationId { get; set; }
        public string OperationName { get; set; } = default!;
    }
}
