using ENINET.TransaprentPortal.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ENINET.TransaprentPortal.Persistence.Configuration
{
    public class ElementConfiguration : IEntityTypeConfiguration<Element>
    {
        public static readonly string acqua = "acqua";
        public static readonly string aria = "aria";
        public void Configure(EntityTypeBuilder<Element> builder)
        {
            builder.HasData
            (

                new Element { Name = aria },
                new Element { Name = acqua }




            );
        }
    }
}
