using System.ComponentModel.DataAnnotations;

namespace ENINET.TransparentPortal.Persistence.Entities
{
    public class GuestAuth
    {
        [Key]
        public Guid RandomCode { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;



    }
}
