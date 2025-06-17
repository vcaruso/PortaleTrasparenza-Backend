using System.ComponentModel.DataAnnotations.Schema;

namespace ENINET.TransaprentPortal.Persistence.Entities
{
    public class UserGroup
    {
        public string Userid { get; set; } = default!;
        public string GroupName { get; set; } = default!;

        [ForeignKey(nameof(GroupName))]
        public ApplicationGroup ApplicationGroup { get; set; } = default!;

        [ForeignKey(nameof(Userid))]
        public ApplicationUser ApplicationUser { get; set; } = default!;
    }
}
