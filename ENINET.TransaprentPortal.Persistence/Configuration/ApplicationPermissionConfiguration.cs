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
    public static readonly string ViewElements = "VIEW_ELEMENTS";
    public static readonly string UpdateElements = "UPDATE_ELEMENTS";
    public static readonly string DeleteElements = "DELETE_ELEMENTS";
    public static readonly string AddElements = "ADD_ELEMENTS";
    public static readonly string ViewSites = "VIEW_SITES";
    public static readonly string UpdateSites = "UPDATE_SITES";
    public static readonly string DeleteSites = "DELETE_SITES";
    public static readonly string AddSites = "ADD_SITES";

    public static readonly string ViewComplaint = "VIEW_COMPLAINT";
    public static readonly string AddComplaintOperation = "ADD_COMPLAINT_OPERATION";
    public static readonly string DeleteComplaintOperation = "DELETE_COMPLAINT_OPERATION";
    public static readonly string UpdateComplaintOperation = "UPDATE_COMPLAINT_OPERATION";


    public void Configure(EntityTypeBuilder<ApplicationPermission> builder)
    {
        builder.HasData(
           new ApplicationPermission { Permission = AddUser, Description = "Aggiunge un utente all'applicazione" },
           new ApplicationPermission { Permission = DeleteUser, Description = "Rimuove un utente all'applicazione" },
           new ApplicationPermission { Permission = ApplicationUsersManage, Description = "Gestione utenti applicazione" },

           new ApplicationPermission { Permission = UploadReport, Description = "Upload Report" },
           new ApplicationPermission { Permission = DeleteReport, Description = "Delete Report" },
           new ApplicationPermission { Permission = ViewReport, Description = "View Report" },
           new ApplicationPermission { Permission = DownloadReport, Description = "Download Report" },

           new ApplicationPermission { Permission = ViewElements, Description = "View Elements" },
           new ApplicationPermission { Permission = AddElements, Description = "Add Elements" },
           new ApplicationPermission { Permission = DeleteElements, Description = "Delete Elements" },
           new ApplicationPermission { Permission = UpdateElements, Description = "Update Elements" },

           new ApplicationPermission { Permission = ViewSites, Description = "View Sites" },
           new ApplicationPermission { Permission = AddSites, Description = "Add Sites" },
           new ApplicationPermission { Permission = DeleteSites, Description = "Delete Sites" },
           new ApplicationPermission { Permission = UpdateSites, Description = "Update Sites" },

           new ApplicationPermission { Permission = ViewComplaint, Description = "View Complaint" },
           new ApplicationPermission { Permission = AddComplaintOperation, Description = "Add Complaint Operation" },
           new ApplicationPermission { Permission = DeleteComplaintOperation, Description = "Delete Complaint Operation" },
           new ApplicationPermission { Permission = UpdateComplaintOperation, Description = "Update Complaint Operation" }



        );
    }
}
