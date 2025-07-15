using ENINET.TransaprentPortal.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ENINET.TransaprentPortal.Persistence.Configuration;

public class ApplicationUserConfiuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public static readonly string EnzoCarusoMail = "vincenzo.caruso@external.enilive.com";
        public static readonly string EnzoCarusoNome = "vincenzo.caruso@external.enilive.com";
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(new ApplicationUser { UserId = EnzoCarusoMail, UserName = EnzoCarusoNome });
        }
    }
}
