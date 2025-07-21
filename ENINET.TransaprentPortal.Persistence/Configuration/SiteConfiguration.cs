using ENINET.TransaprentPortal.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ENINET.TransaprentPortal.Persistence.Configuration
{
    public class SiteConfiguration : IEntityTypeConfiguration<Site>
    {
        public static readonly string AcronimoRovigo = "RO";
        public static readonly string NomeRovigo = "Sito di Rovigo";

        public void Configure(EntityTypeBuilder<Site> builder)
        {
            builder.HasData
            (

                new Site { Acronym = AcronimoRovigo, Description = NomeRovigo, Latitude = 45.06966031502005, Longitude = 11.79094779291781 }


            );
        }
    }
}
