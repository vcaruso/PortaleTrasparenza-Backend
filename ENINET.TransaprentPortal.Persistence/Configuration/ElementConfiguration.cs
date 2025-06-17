using ENINET.TransaprentPortal.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ENINET.TransaprentPortal.Persistence.Configuration
{
    public class ElementConfiguration : IEntityTypeConfiguration<Element>
    {
        public void Configure(EntityTypeBuilder<Element> builder)
        {
            builder.HasData
            (

                new Element { Name = "aria", Acronym = SiteConfiguration.AcronimoRovigo },
                new Element { Name = "acqua", Acronym = SiteConfiguration.AcronimoRovigo }




            );
        }
    }
}
