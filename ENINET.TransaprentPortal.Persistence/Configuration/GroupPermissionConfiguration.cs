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
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.ApplicationUsersManage },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.UploadReport },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.DeleteReport },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.ViewReport },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.DownloadReport },
                //Contributors
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.UploadReport },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.DeleteReport },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.ViewReport },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.DownloadReport },
                 //Viewers
                 new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.UploadReport },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.DeleteReport },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.ViewReport },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.DownloadReport }


            );
        }
    }
}
