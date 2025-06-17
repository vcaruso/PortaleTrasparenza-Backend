
using ENINET.TransaprentPortal.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ENINET.TransparentPortal.API.Extension;

public static class AutomaticMigration
{
    /// <summary>
    /// Applica le migrazioni se necessario
    /// </summary>
    /// <param name="app"></param>
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Check and apply pending migrations
            var pendingMigrations = dbContext.Database.GetPendingMigrations();
            if (pendingMigrations.Any())
            {
                Console.WriteLine("Applying pending migrations...");
                dbContext.Database.Migrate();
                Console.WriteLine("Migrations applied successfully.");
            }
            else
            {
                Console.WriteLine("No pending migrations found.");
            }

        }


    }
}
