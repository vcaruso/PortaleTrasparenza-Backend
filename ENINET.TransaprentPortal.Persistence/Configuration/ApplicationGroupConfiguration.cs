using ENINET.TransaprentPortal.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ENINET.TransaprentPortal.Persistence.Configuration
{
    public class ApplicationGroupConfiguration : IEntityTypeConfiguration<ApplicationGroup>

    {
        public static readonly string Administrators = "Administrators";
        public static readonly string Contributors = "Contributors";
        public static readonly string Viewers = "Viewers";

        public void Configure(EntityTypeBuilder<ApplicationGroup> builder)
        {
            builder.HasData(
                new ApplicationGroup { GroupName = Administrators, GroupDescription = "Administrators Group" },
                new ApplicationGroup { GroupName = Contributors, GroupDescription = "Users Group" },
                new ApplicationGroup { GroupName = Viewers, GroupDescription = "Viewers Group" }
            );
        }
    }
}
