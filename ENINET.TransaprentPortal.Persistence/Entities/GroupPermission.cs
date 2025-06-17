using System.ComponentModel.DataAnnotations.Schema;

namespace ENINET.TransaprentPortal.Persistence.Entities
{
    public class GroupPermission
    {

        public string Permission { get; set; } = default!;
        public string GroupName { get; set; } = default!;

        [ForeignKey(nameof(GroupName))]
        public ApplicationGroup ApplicationGroup { get; set; } = default!;

        [ForeignKey(nameof(Permission))]
        public ApplicationPermission ApplicationPermission { get; set; } = default!;


    }
}
