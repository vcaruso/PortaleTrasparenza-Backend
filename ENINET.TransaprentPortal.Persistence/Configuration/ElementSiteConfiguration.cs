using ENINET.TransaprentPortal.Persistence.Configuration;
using ENINET.TransparentPortal.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ENINET.TransparentPortal.Persistence.Configuration
{
    public class ElementSiteConfiguration : IEntityTypeConfiguration<ElementSite>
    {
        public void Configure(EntityTypeBuilder<ElementSite> builder)
        {
            builder.HasData(
                new ElementSite { Acronym = SiteConfiguration.AcronimoRovigo, ElementName = ElementConfiguration.aria, MonthlyReport = 4 },
                new ElementSite { Acronym = SiteConfiguration.AcronimoRovigo, ElementName = ElementConfiguration.acqua, MonthlyReport = 6 }

            );
        }
    }
}
