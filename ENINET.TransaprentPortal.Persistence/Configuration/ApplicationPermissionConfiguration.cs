using ENINET.TransaprentPortal.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ENINET.TransaprentPortal.Persistence.Configuration;

public class ApplicationPermissionConfiguration : IEntityTypeConfiguration<ApplicationPermission>
{
    public static readonly string ADD_USER = "ADD_USER";
    public static readonly string DELETE_USER = "DELETE_USER";
    public static readonly string APPLICATION_USERS_MANAGE = "APPLICATION_USERS_MANAGE";
    public static readonly string Upload_REPORT = "UPLOAD_REPORT";
    public static readonly string DELETE_REPORT = "DELETE_REPORT";
    public static readonly string VIEW_REPORT = "VIEW_REPORT";
    public static readonly string DOWNLOAD_REPORT = "DOWNLOAD_REPORT";
    public static readonly string VIEW_ELEMENTS = "VIEW_ELEMENTS";
    public static readonly string UPDATE_ELEMENTS = "UPDATE_ELEMENTS";
    public static readonly string DELETE_ELEMENTS = "DELETE_ELEMENTS";
    public static readonly string ADD_ELEMENTS = "ADD_ELEMENTS";
    public static readonly string VIEW_SITES = "VIEW_SITES";
    public static readonly string UPDATE_SITES = "UPDATE_SITES";
    public static readonly string DELETE_SITES = "DELETE_SITES";
    public static readonly string ADD_SITES = "ADD_SITES";

    public static readonly string VIEW_COMPLAINT = "VIEW_COMPLAINT";
    public static readonly string ADD_COMPLAINT_STEP = "ADD_COMPLAINT_STEP";
    public static readonly string DELETE_COMPLAINT_STEP = "DELETE_COMPLAINT_STEP";
    public static readonly string UPDATE_COMPLAINT_STEP = "UPDATE_COMPLAINT_STEP";
    public void Configure(EntityTypeBuilder<ApplicationPermission> builder)
    {
        builder.HasData(
           new ApplicationPermission { Permission = ADD_USER, Description = "Aggiunge un utente all'applicazione" },
           new ApplicationPermission { Permission = DELETE_USER, Description = "Rimuove un utente all'applicazione" },
           new ApplicationPermission { Permission = APPLICATION_USERS_MANAGE, Description = "Gestione utenti applicazione" },

           new ApplicationPermission { Permission = Upload_REPORT, Description = "Upload Report" },
           new ApplicationPermission { Permission = DELETE_REPORT, Description = "Delete Report" },
           new ApplicationPermission { Permission = VIEW_REPORT, Description = "View Report" },
           new ApplicationPermission { Permission = DOWNLOAD_REPORT, Description = "Download Report" },

           new ApplicationPermission { Permission = VIEW_ELEMENTS, Description = "View Elements" },
           new ApplicationPermission { Permission = ADD_ELEMENTS, Description = "Add Elements" },
           new ApplicationPermission { Permission = DELETE_ELEMENTS, Description = "Delete Elements" },
           new ApplicationPermission { Permission = UPDATE_ELEMENTS, Description = "Update Elements" },

           new ApplicationPermission { Permission = VIEW_SITES, Description = "View Sites" },
           new ApplicationPermission { Permission = ADD_SITES, Description = "Add Sites" },
           new ApplicationPermission { Permission = DELETE_SITES, Description = "Delete Sites" },
           new ApplicationPermission { Permission = UPDATE_SITES, Description = "Update Sites" },

           new ApplicationPermission { Permission = VIEW_COMPLAINT, Description = "View Complaint" },
           new ApplicationPermission { Permission = ADD_COMPLAINT_STEP, Description = "Add Complaint Operation" },
           new ApplicationPermission { Permission = DELETE_COMPLAINT_STEP, Description = "Delete Complaint Operation" },
           new ApplicationPermission { Permission = UPDATE_COMPLAINT_STEP, Description = "Update Complaint Operation" }



        );
    }
}
