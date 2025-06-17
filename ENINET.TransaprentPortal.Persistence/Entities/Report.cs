using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENINET.TransaprentPortal.Persistence.Entities
{
    public class Report
    {
        [Key]
        [MaxLength(30)]
        public string FileName { get; set; } = default!;
        [MaxLength(10)]

        public string ElementName { get; set; } = default!;
        [MaxLength(4)]
        public string Year { get; set; } = default!;
        [MaxLength(2)]
        public string Month { get; set; } = default!;
        [MaxLength(4)]
        public string Progressive { get; set; } = default!;
        [MaxLength(20)]
        public string Acronym { get; set; } = default!;
        public DateTime UploadDate { get; set; } = default;
        [MaxLength(80)]
        public string UserUpload { get; set; } = default!;

        [ForeignKey(nameof(Acronym))]
        public Site Site { get; set; } = default!;
        [ForeignKey(nameof(ElementName))]
        public Element Element { get; set; } = default!;

        public long FileLength { get; set; }
    }
}
