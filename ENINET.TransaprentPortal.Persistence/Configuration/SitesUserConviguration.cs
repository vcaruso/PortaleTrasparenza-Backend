using ENINET.TransaprentPortal.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static ENINET.TransaprentPortal.Persistence.Configuration.ApplicationUserConfiuration;

namespace ENINET.TransaprentPortal.Persistence.Configuration
{
    public class SitesUserConfiguration : IEntityTypeConfiguration<SitesUser>
    {

        public void Configure(EntityTypeBuilder<SitesUser> builder)
        {
            builder.HasData(
                new SitesUser { Acronym = SiteConfiguration.AcronimoRovigo, UserId = ApplicationUserConfiguration.EnzoCarusoMail }

            );
        }
    }
}
