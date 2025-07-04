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
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.ViewElements },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.UpdateElements },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.AddElements },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.DeleteElements },

                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.ViewSites },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.UpdateSites },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.AddSites },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.DeleteSites },

                //Contributors
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.UploadReport },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.DeleteReport },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.ViewReport },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.DownloadReport },

                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.ViewElements },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.UpdateElements },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.AddElements },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.DeleteElements },

                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.ViewSites },

                //Viewers
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.UploadReport },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.DeleteReport },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.ViewReport },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.DownloadReport },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.ViewElements },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.ViewSites }


            );
        }
    }
}
