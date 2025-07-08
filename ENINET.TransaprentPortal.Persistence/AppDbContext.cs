using ENINET.TransaprentPortal.Persistence.Configuration;
using ENINET.TransaprentPortal.Persistence.Entities;
using ENINET.TransparentPortal.Persistence.Configuration;
using ENINET.TransparentPortal.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using static ENINET.TransaprentPortal.Persistence.Configuration.ApplicationUserConfiuration;

namespace ENINET.TransaprentPortal.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GroupPermission>().HasKey(k => new { k.Permission, k.GroupName });
            modelBuilder.Entity<UserGroup>().HasKey(k => new { k.Userid, k.GroupName });
            modelBuilder.Entity<SitesUser>().HasKey(k => new { k.UserId, k.Acronym });
            modelBuilder.Entity<ElementSite>().HasKey(k => new { k.ElementName, k.Acronym });



            modelBuilder.ApplyConfiguration(new SiteConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationGroupConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationPermissionConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new GroupPermissionConfiguration());
            modelBuilder.ApplyConfiguration(new UserGroupConfiguration());
            modelBuilder.ApplyConfiguration(new SitesUserConfiguration());
            modelBuilder.ApplyConfiguration(new ElementConfiguration());
            modelBuilder.ApplyConfiguration(new ElementSiteConfiguration());



        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Remove(typeof(CascadeDeleteConvention));

        }

        public DbSet<Site> Sites { get; set; }

        public DbSet<ApplicationGroup> ApplicationGroups { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<SitesUser> SitesUsers { get; set; }
        public DbSet<ApplicationPermission> ApplicationPermissions { get; set; }
        public DbSet<GroupPermission> GroupPermissions { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Element> Elements { get; set; }

        public DbSet<ElementSite> ElementsSite { get; set; }

    }
}
