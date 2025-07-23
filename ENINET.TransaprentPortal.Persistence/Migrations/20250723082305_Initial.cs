using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ENINET.TransparentPortal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationGroups",
                columns: table => new
                {
                    GroupName = table.Column<string>(type: "text", nullable: false),
                    GroupDescription = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationGroups", x => x.GroupName);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationPermissions",
                columns: table => new
                {
                    Permission = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationPermissions", x => x.Permission);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ComplaintOperations",
                columns: table => new
                {
                    OperationId = table.Column<Guid>(type: "uuid", nullable: false),
                    OperationName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintOperations", x => x.OperationId);
                });

            migrationBuilder.CreateTable(
                name: "Elements",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elements", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "GuestAuths",
                columns: table => new
                {
                    RandomCode = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestAuths", x => x.RandomCode);
                });

            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    Acronym = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.Acronym);
                });

            migrationBuilder.CreateTable(
                name: "GroupPermissions",
                columns: table => new
                {
                    Permission = table.Column<string>(type: "text", nullable: false),
                    GroupName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPermissions", x => new { x.Permission, x.GroupName });
                    table.ForeignKey(
                        name: "FK_GroupPermissions_ApplicationGroups_GroupName",
                        column: x => x.GroupName,
                        principalTable: "ApplicationGroups",
                        principalColumn: "GroupName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupPermissions_ApplicationPermissions_Permission",
                        column: x => x.Permission,
                        principalTable: "ApplicationPermissions",
                        principalColumn: "Permission",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    Userid = table.Column<string>(type: "text", nullable: false),
                    GroupName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => new { x.Userid, x.GroupName });
                    table.ForeignKey(
                        name: "FK_UserGroups_ApplicationGroups_GroupName",
                        column: x => x.GroupName,
                        principalTable: "ApplicationGroups",
                        principalColumn: "GroupName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroups_ApplicationUsers_Userid",
                        column: x => x.Userid,
                        principalTable: "ApplicationUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Complaints",
                columns: table => new
                {
                    ComplaintId = table.Column<Guid>(type: "uuid", nullable: false),
                    RandomCode = table.Column<Guid>(type: "uuid", nullable: false),
                    Acronym = table.Column<string>(type: "text", nullable: false),
                    Opener = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OpenedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CancelledDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ResolutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaints", x => x.ComplaintId);
                    table.ForeignKey(
                        name: "FK_Complaints_GuestAuths_RandomCode",
                        column: x => x.RandomCode,
                        principalTable: "GuestAuths",
                        principalColumn: "RandomCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Complaints_Sites_Acronym",
                        column: x => x.Acronym,
                        principalTable: "Sites",
                        principalColumn: "Acronym",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ElementsSite",
                columns: table => new
                {
                    ElementName = table.Column<string>(type: "text", nullable: false),
                    Acronym = table.Column<string>(type: "text", nullable: false),
                    MonthlyReport = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementsSite", x => new { x.ElementName, x.Acronym });
                    table.ForeignKey(
                        name: "FK_ElementsSite_Elements_ElementName",
                        column: x => x.ElementName,
                        principalTable: "Elements",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElementsSite_Sites_Acronym",
                        column: x => x.Acronym,
                        principalTable: "Sites",
                        principalColumn: "Acronym",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    FileName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    ElementName = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Year = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    Month = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    Progressive = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    Acronym = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    UploadDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserUpload = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    FileLength = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.FileName);
                    table.ForeignKey(
                        name: "FK_Reports_Elements_ElementName",
                        column: x => x.ElementName,
                        principalTable: "Elements",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_Sites_Acronym",
                        column: x => x.Acronym,
                        principalTable: "Sites",
                        principalColumn: "Acronym",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SitesUsers",
                columns: table => new
                {
                    Acronym = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SitesUsers", x => new { x.UserId, x.Acronym });
                    table.ForeignKey(
                        name: "FK_SitesUsers_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SitesUsers_Sites_Acronym",
                        column: x => x.Acronym,
                        principalTable: "Sites",
                        principalColumn: "Acronym",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComplaintSteps",
                columns: table => new
                {
                    ResolutionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ComplaintId = table.Column<Guid>(type: "uuid", nullable: false),
                    StepDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Operator = table.Column<string>(type: "text", nullable: false),
                    OperationId = table.Column<Guid>(type: "uuid", nullable: false),
                    OperationText = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintSteps", x => x.ResolutionId);
                    table.ForeignKey(
                        name: "FK_ComplaintSteps_ComplaintOperations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "ComplaintOperations",
                        principalColumn: "OperationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComplaintSteps_Complaints_ComplaintId",
                        column: x => x.ComplaintId,
                        principalTable: "Complaints",
                        principalColumn: "ComplaintId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ApplicationGroups",
                columns: new[] { "GroupName", "GroupDescription" },
                values: new object[,]
                {
                    { "Administrators", "Administrators Group" },
                    { "Contributors", "Users Group" },
                    { "Viewers", "Viewers Group" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationPermissions",
                columns: new[] { "Permission", "Description" },
                values: new object[,]
                {
                    { "ADD_COMPLAINT_STEP", "Add Complaint Operation" },
                    { "ADD_ELEMENTS", "Add Elements" },
                    { "ADD_SITES", "Add Sites" },
                    { "ADD_USER", "Aggiunge un utente all'applicazione" },
                    { "APPLICATION_USERS_MANAGE", "Gestione utenti applicazione" },
                    { "DELETE_COMPLAINT_STEP", "Delete Complaint Operation" },
                    { "DELETE_ELEMENTS", "Delete Elements" },
                    { "DELETE_REPORT", "Delete Report" },
                    { "DELETE_SITES", "Delete Sites" },
                    { "DELETE_USER", "Rimuove un utente all'applicazione" },
                    { "DOWNLOAD_REPORT", "Download Report" },
                    { "UPDATE_COMPLAINT_STEP", "Update Complaint Operation" },
                    { "UPDATE_ELEMENTS", "Update Elements" },
                    { "UPDATE_SITES", "Update Sites" },
                    { "UPLOAD_REPORT", "Upload Report" },
                    { "VIEW_COMPLAINT", "View Complaint" },
                    { "VIEW_ELEMENTS", "View Elements" },
                    { "VIEW_REPORT", "View Report" },
                    { "VIEW_SITES", "View Sites" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "UserId", "UserName" },
                values: new object[] { "vincenzo.caruso@external.enilive.com", "vincenzo.caruso@external.enilive.com" });

            migrationBuilder.InsertData(
                table: "ComplaintOperations",
                columns: new[] { "OperationId", "OperationName" },
                values: new object[,]
                {
                    { new Guid("0cba4bb0-d2fd-487c-8a14-6705a2ba5d1b"), "OPENED" },
                    { new Guid("97afc8a1-fae8-44ad-91c0-944874df8ecd"), "SOLVED" },
                    { new Guid("a1046896-6277-49c4-95d7-cd381fa09dc4"), "ACTION" },
                    { new Guid("abe46afb-f776-4e83-b51a-f2c95fc45526"), "CANCELED" }
                });

            migrationBuilder.InsertData(
                table: "Elements",
                column: "Name",
                values: new object[]
                {
                    "acqua",
                    "aria"
                });

            migrationBuilder.InsertData(
                table: "Sites",
                columns: new[] { "Acronym", "Description", "Latitude", "Longitude" },
                values: new object[] { "RO", "Sito di Rovigo", 45.069660315020052, 11.790947792917811 });

            migrationBuilder.InsertData(
                table: "ElementsSite",
                columns: new[] { "Acronym", "ElementName", "MonthlyReport" },
                values: new object[,]
                {
                    { "RO", "acqua", 6 },
                    { "RO", "aria", 4 }
                });

            migrationBuilder.InsertData(
                table: "GroupPermissions",
                columns: new[] { "GroupName", "Permission" },
                values: new object[,]
                {
                    { "Administrators", "ADD_COMPLAINT_STEP" },
                    { "Contributors", "ADD_COMPLAINT_STEP" },
                    { "Viewers", "ADD_COMPLAINT_STEP" },
                    { "Administrators", "ADD_ELEMENTS" },
                    { "Contributors", "ADD_ELEMENTS" },
                    { "Administrators", "ADD_SITES" },
                    { "Administrators", "ADD_USER" },
                    { "Administrators", "APPLICATION_USERS_MANAGE" },
                    { "Administrators", "DELETE_COMPLAINT_STEP" },
                    { "Contributors", "DELETE_COMPLAINT_STEP" },
                    { "Viewers", "DELETE_COMPLAINT_STEP" },
                    { "Administrators", "DELETE_ELEMENTS" },
                    { "Contributors", "DELETE_ELEMENTS" },
                    { "Administrators", "DELETE_REPORT" },
                    { "Contributors", "DELETE_REPORT" },
                    { "Viewers", "DELETE_REPORT" },
                    { "Administrators", "DELETE_SITES" },
                    { "Administrators", "DELETE_USER" },
                    { "Administrators", "DOWNLOAD_REPORT" },
                    { "Contributors", "DOWNLOAD_REPORT" },
                    { "Viewers", "DOWNLOAD_REPORT" },
                    { "Administrators", "UPDATE_COMPLAINT_STEP" },
                    { "Contributors", "UPDATE_COMPLAINT_STEP" },
                    { "Viewers", "UPDATE_COMPLAINT_STEP" },
                    { "Administrators", "UPDATE_ELEMENTS" },
                    { "Contributors", "UPDATE_ELEMENTS" },
                    { "Administrators", "UPDATE_SITES" },
                    { "Administrators", "UPLOAD_REPORT" },
                    { "Contributors", "UPLOAD_REPORT" },
                    { "Viewers", "UPLOAD_REPORT" },
                    { "Administrators", "VIEW_COMPLAINT" },
                    { "Contributors", "VIEW_COMPLAINT" },
                    { "Viewers", "VIEW_COMPLAINT" },
                    { "Administrators", "VIEW_ELEMENTS" },
                    { "Contributors", "VIEW_ELEMENTS" },
                    { "Viewers", "VIEW_ELEMENTS" },
                    { "Administrators", "VIEW_REPORT" },
                    { "Contributors", "VIEW_REPORT" },
                    { "Viewers", "VIEW_REPORT" },
                    { "Administrators", "VIEW_SITES" },
                    { "Contributors", "VIEW_SITES" },
                    { "Viewers", "VIEW_SITES" }
                });

            migrationBuilder.InsertData(
                table: "SitesUsers",
                columns: new[] { "Acronym", "UserId" },
                values: new object[] { "RO", "vincenzo.caruso@external.enilive.com" });

            migrationBuilder.InsertData(
                table: "UserGroups",
                columns: new[] { "GroupName", "Userid" },
                values: new object[] { "Administrators", "vincenzo.caruso@external.enilive.com" });

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_Acronym",
                table: "Complaints",
                column: "Acronym");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_RandomCode",
                table: "Complaints",
                column: "RandomCode");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintSteps_ComplaintId",
                table: "ComplaintSteps",
                column: "ComplaintId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintSteps_OperationId",
                table: "ComplaintSteps",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementsSite_Acronym",
                table: "ElementsSite",
                column: "Acronym");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPermissions_GroupName",
                table: "GroupPermissions",
                column: "GroupName");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_Acronym",
                table: "Reports",
                column: "Acronym");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ElementName",
                table: "Reports",
                column: "ElementName");

            migrationBuilder.CreateIndex(
                name: "IX_SitesUsers_Acronym",
                table: "SitesUsers",
                column: "Acronym");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_GroupName",
                table: "UserGroups",
                column: "GroupName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComplaintSteps");

            migrationBuilder.DropTable(
                name: "ElementsSite");

            migrationBuilder.DropTable(
                name: "GroupPermissions");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "SitesUsers");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropTable(
                name: "ComplaintOperations");

            migrationBuilder.DropTable(
                name: "Complaints");

            migrationBuilder.DropTable(
                name: "ApplicationPermissions");

            migrationBuilder.DropTable(
                name: "Elements");

            migrationBuilder.DropTable(
                name: "ApplicationGroups");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "GuestAuths");

            migrationBuilder.DropTable(
                name: "Sites");
        }
    }
}
