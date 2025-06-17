using ENINET.TransaprentPortal.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ENINET.TransaprentPortal.Persistence.Configuration
{
    public class GroupPermissionConfiguration : IEntityTypeConfiguration<GroupPermission>
    {
        public void Configure(EntityTypeBuilder<GroupPermission> builder)
        {
            builder.HasData
            (
                // Administrators
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.AddUser },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.DeleteUser },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.ApplicationUsersManage }
                //Users 


            );
        }
    }
}
