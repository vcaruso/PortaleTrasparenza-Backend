namespace ENINET.TransparentPortal.API.Dtos.Compliant
{
    public class ComplaintDto
    {
        public string Acronym { get; set; } = default!;
        public Guid ComplaintId { get; set; }
        public string Opener { get; set; } = default!;


        public DateTime CreationDate { get; set; }
        public DateTime OpenedDate { get; set; }
        public DateTime CancelledDate { get; set; }
        public DateTime ResolutionDate { get; set; }
    }
}
