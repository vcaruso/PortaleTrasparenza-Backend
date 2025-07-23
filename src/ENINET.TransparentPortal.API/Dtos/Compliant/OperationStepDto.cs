namespace ENINET.TransparentPortal.API.Dtos.Compliant
{
    public class OperationStepDto
    {
        public string Operator { get; set; } = default!;
        public DateTime StepDate { get; set; }

        public string Operation { get; set; } = default!;
        public string OperationText { get; set; } = default!;

    }
}
