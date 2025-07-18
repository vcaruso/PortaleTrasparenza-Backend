namespace ENINET.TransparentPortal.API.Dtos.Compliant
{
    public class AddComplaintStepDto
    {
        public Guid ComplaintId { get; set; }
        public Guid OperationId { get; set; }
        public string OperationText { get; set; } = string.Empty;
    }
}
