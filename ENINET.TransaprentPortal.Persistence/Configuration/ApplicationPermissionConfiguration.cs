using ENINET.TransaprentPortal.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ENINET.TransaprentPortal.Persistence.Configuration;

public class ApplicationPermissionConfiguration : IEntityTypeConfiguration<ApplicationPermission>
{
    public static readonly string AddUser = "ADD_USER";
    public static readonly string DeleteUser = "DELETE_USER";
    public static readonly string ApplicationUsersManage = "APPLICATION_USERS_MANAGE";
    public void Configure(EntityTypeBuilder<ApplicationPermission> builder)
    {
        builder.HasData(
           new ApplicationPermission { Permission = AddUser, Description = "Aggiunge un utente all'applicazione" },
           new ApplicationPermission { Permission = DeleteUser, Description = "Rimuove un utente all'applicazione" },

           new ApplicationPermission { Permission = ApplicationUsersManage, Description = "Gestione utenti applicazione" }




       );
    }
}
