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

                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.AddComplaintOperation },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.DeleteComplaintOperation },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.UpdateComplaintOperation },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.ViewComplaint },



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
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.AddComplaintOperation },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.DeleteComplaintOperation },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.UpdateComplaintOperation },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.ViewComplaint },

                //Viewers
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.UploadReport },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.DeleteReport },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.ViewReport },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.DownloadReport },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.ViewElements },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.ViewSites },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.AddComplaintOperation },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.DeleteComplaintOperation },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.UpdateComplaintOperation },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.ViewComplaint }


            );
        }
    }
}
