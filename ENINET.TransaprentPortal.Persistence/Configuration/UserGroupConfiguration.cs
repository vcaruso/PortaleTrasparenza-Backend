using ENINET.TransaprentPortal.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static ENINET.TransaprentPortal.Persistence.Configuration.ApplicationUserConfiuration;

namespace ENINET.TransaprentPortal.Persistence.Configuration
{
    public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.HasData(new UserGroup { GroupName = ApplicationGroupConfiguration.Administrators, Userid = ApplicationUserConfiguration.EnzoCarusoMail });
        }
    }
}
