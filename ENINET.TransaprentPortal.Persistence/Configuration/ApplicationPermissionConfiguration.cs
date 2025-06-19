using ENINET.TransaprentPortal.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ENINET.TransaprentPortal.Persistence.Configuration;

public class ApplicationPermissionConfiguration : IEntityTypeConfiguration<ApplicationPermission>
{
    public static readonly string AddUser = "ADD_USER";
    public static readonly string DeleteUser = "DELETE_USER";
    public static readonly string ApplicationUsersManage = "APPLICATION_USERS_MANAGE";
    public static readonly string UploadReport = "UPLOAD_REPORT";
    public static readonly string DeleteReport = "DELETE_REPORT";
    public static readonly string ViewReport = "VIEW_REPORT";
    public static readonly string DownloadReport = "DOWNLOAD_REPORT";
    public void Configure(EntityTypeBuilder<ApplicationPermission> builder)
    {
        builder.HasData(
           new ApplicationPermission { Permission = AddUser, Description = "Aggiunge un utente all'applicazione" },
           new ApplicationPermission { Permission = DeleteUser, Description = "Rimuove un utente all'applicazione" },
           new ApplicationPermission { Permission = ApplicationUsersManage, Description = "Gestione utenti applicazione" },
           new ApplicationPermission { Permission = UploadReport, Description = "Upload Report" },
           new ApplicationPermission { Permission = DeleteReport, Description = "Delete Report" },
           new ApplicationPermission { Permission = ViewReport, Description = "View Report" },
           new ApplicationPermission { Permission = DownloadReport, Description = "Download Report" }
        );
    }
}
