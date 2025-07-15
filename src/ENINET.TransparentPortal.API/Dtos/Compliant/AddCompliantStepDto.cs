namespace ENINET.TransparentPortal.API.Dtos.Compliant
{
    public class AddCompliantStepDto
    {
        public Guid ComplaintId { get; set; }
        public Guid OperationId { get; set; }
        public string OperationText { get; set; } = string.Empty;
    }
}
