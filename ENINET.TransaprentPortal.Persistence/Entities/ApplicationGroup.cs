using System.ComponentModel.DataAnnotations;

namespace ENINET.TransaprentPortal.Persistence.Entities
{
    public class ApplicationGroup
    {
        [Key]

        public string GroupName { get; set; } = default!;
        public string GroupDescription { get; set; } = default!;

        public IList<GroupPermission> GroupPermissions { get; set; } = default!;
        public virtual IList<UserGroup> UserGroups { get; set; } = default!;
    }
}
