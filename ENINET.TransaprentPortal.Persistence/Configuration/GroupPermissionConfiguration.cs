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
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.ADD_USER },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.DELETE_USER },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.APPLICATION_USERS_MANAGE },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.Upload_REPORT },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.DELETE_REPORT },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.VIEW_REPORT },

                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.DOWNLOAD_REPORT },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.VIEW_ELEMENTS },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.UPDATE_ELEMENTS },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.ADD_ELEMENTS },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.DELETE_ELEMENTS },

                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.VIEW_SITES },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.UPDATE_SITES },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.ADD_SITES },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.DELETE_SITES },

                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.VIEW_COMPLAINT_STEP },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.DELETE_COMPLAINT_STEP },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.UPDATE_COMPLAINT_STEP },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Administrators, Permission = ApplicationPermissionConfiguration.VIEW_COMPLAINT },



                //Contributors
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.Upload_REPORT },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.DELETE_REPORT },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.VIEW_REPORT },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.DOWNLOAD_REPORT },

                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.VIEW_ELEMENTS },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.UPDATE_ELEMENTS },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.ADD_ELEMENTS },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.DELETE_ELEMENTS },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.VIEW_SITES },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.VIEW_COMPLAINT_STEP },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.DELETE_COMPLAINT_STEP },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.UPDATE_COMPLAINT_STEP },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Contributors, Permission = ApplicationPermissionConfiguration.VIEW_COMPLAINT },

                //Viewers
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.Upload_REPORT },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.DELETE_REPORT },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.VIEW_REPORT },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.DOWNLOAD_REPORT },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.VIEW_ELEMENTS },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.VIEW_SITES },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.VIEW_COMPLAINT_STEP },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.DELETE_COMPLAINT_STEP },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.UPDATE_COMPLAINT_STEP },
                new GroupPermission { GroupName = ApplicationGroupConfiguration.Viewers, Permission = ApplicationPermissionConfiguration.VIEW_COMPLAINT }


            );
        }
    }
}
