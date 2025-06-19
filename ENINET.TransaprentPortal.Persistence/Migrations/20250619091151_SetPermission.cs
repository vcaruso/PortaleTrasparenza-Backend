using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ENINET.TransaprentPortal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SetPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationGroups",
                keyColumn: "GroupName",
                keyValue: "Users");

            migrationBuilder.InsertData(
                table: "ApplicationGroups",
                columns: new[] { "GroupName", "GroupDescription" },
                values: new object[,]
                {
                    { "Contributors", "Users Group" },
                    { "Viewers", "Viewers Group" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationPermissions",
                columns: new[] { "Permission", "Description" },
                values: new object[,]
                {
                    { "DELETE_REPORT", "Delete Report" },
                    { "DOWNLOAD_REPORT", "Download Report" },
                    { "UPLOAD_REPORT", "Upload Report" },
                    { "VIEW_REPORT", "View Report" }
                });

            migrationBuilder.InsertData(
                table: "GroupPermissions",
                columns: new[] { "GroupName", "Permission" },
                values: new object[,]
                {
                    { "Administrators", "DELETE_REPORT" },
                    { "Contributors", "DELETE_REPORT" },
                    { "Viewers", "DELETE_REPORT" },
                    { "Administrators", "DOWNLOAD_REPORT" },
                    { "Contributors", "DOWNLOAD_REPORT" },
                    { "Viewers", "DOWNLOAD_REPORT" },
                    { "Administrators", "UPLOAD_REPORT" },
                    { "Contributors", "UPLOAD_REPORT" },
                    { "Viewers", "UPLOAD_REPORT" },
                    { "Administrators", "VIEW_REPORT" },
                    { "Contributors", "VIEW_REPORT" },
                    { "Viewers", "VIEW_REPORT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumns: new[] { "GroupName", "Permission" },
                keyValues: new object[] { "Administrators", "DELETE_REPORT" });

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumns: new[] { "GroupName", "Permission" },
                keyValues: new object[] { "Contributors", "DELETE_REPORT" });

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumns: new[] { "GroupName", "Permission" },
                keyValues: new object[] { "Viewers", "DELETE_REPORT" });

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumns: new[] { "GroupName", "Permission" },
                keyValues: new object[] { "Administrators", "DOWNLOAD_REPORT" });

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumns: new[] { "GroupName", "Permission" },
                keyValues: new object[] { "Contributors", "DOWNLOAD_REPORT" });

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumns: new[] { "GroupName", "Permission" },
                keyValues: new object[] { "Viewers", "DOWNLOAD_REPORT" });

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumns: new[] { "GroupName", "Permission" },
                keyValues: new object[] { "Administrators", "UPLOAD_REPORT" });

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumns: new[] { "GroupName", "Permission" },
                keyValues: new object[] { "Contributors", "UPLOAD_REPORT" });

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumns: new[] { "GroupName", "Permission" },
                keyValues: new object[] { "Viewers", "UPLOAD_REPORT" });

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumns: new[] { "GroupName", "Permission" },
                keyValues: new object[] { "Administrators", "VIEW_REPORT" });

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumns: new[] { "GroupName", "Permission" },
                keyValues: new object[] { "Contributors", "VIEW_REPORT" });

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumns: new[] { "GroupName", "Permission" },
                keyValues: new object[] { "Viewers", "VIEW_REPORT" });

            migrationBuilder.DeleteData(
                table: "ApplicationGroups",
                keyColumn: "GroupName",
                keyValue: "Contributors");

            migrationBuilder.DeleteData(
                table: "ApplicationGroups",
                keyColumn: "GroupName",
                keyValue: "Viewers");

            migrationBuilder.DeleteData(
                table: "ApplicationPermissions",
                keyColumn: "Permission",
                keyValue: "DELETE_REPORT");

            migrationBuilder.DeleteData(
                table: "ApplicationPermissions",
                keyColumn: "Permission",
                keyValue: "DOWNLOAD_REPORT");

            migrationBuilder.DeleteData(
                table: "ApplicationPermissions",
                keyColumn: "Permission",
                keyValue: "UPLOAD_REPORT");

            migrationBuilder.DeleteData(
                table: "ApplicationPermissions",
                keyColumn: "Permission",
                keyValue: "VIEW_REPORT");

            migrationBuilder.InsertData(
                table: "ApplicationGroups",
                columns: new[] { "GroupName", "GroupDescription" },
                values: new object[] { "Users", "Users Group" });
        }
    }
}
