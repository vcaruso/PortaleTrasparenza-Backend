using System.ComponentModel.DataAnnotations;

namespace ENINET.TransaprentPortal.Persistence.Entities
{
    public class ApplicationUser
    {
        [Key]
        public string UserId { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public virtual IList<UserGroup> UserGroups { get; set; } = default!;
        public virtual IList<SitesUser> SitesUsers { get; set; } = default!;

    }
}
