using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ENINET.TransaprentPortal.Persistence.Migrations
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
                name: "Sites",
                columns: table => new
                {
                    Acronym = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
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
                name: "Elements",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    Acronym = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elements", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Elements_Sites_Acronym",
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
                    UserUpload = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false)
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

            migrationBuilder.InsertData(
                table: "ApplicationGroups",
                columns: new[] { "GroupName", "GroupDescription" },
                values: new object[,]
                {
                    { "Administrators", "Administrators Group" },
                    { "Users", "Users Group" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationPermissions",
                columns: new[] { "Permission", "Description" },
                values: new object[,]
                {
                    { "ADD_USER", "Aggiunge un utente all'applicazione" },
                    { "APPLICATION_USERS_MANAGE", "Gestione utenti applicazione" },
                    { "DELETE_USER", "Rimuove un utente all'applicazione" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "UserId", "UserName" },
                values: new object[] { "vincenzo.caruso@external.enilive.com", "vincenzo.caruso@external.enilive.com" });

            migrationBuilder.InsertData(
                table: "Sites",
                columns: new[] { "Acronym", "Description" },
                values: new object[] { "RO", "Sito di Rovigo" });

            migrationBuilder.InsertData(
                table: "Elements",
                columns: new[] { "Name", "Acronym" },
                values: new object[,]
                {
                    { "acqua", "RO" },
                    { "aria", "RO" }
                });

            migrationBuilder.InsertData(
                table: "GroupPermissions",
                columns: new[] { "GroupName", "Permission" },
                values: new object[,]
                {
                    { "Administrators", "ADD_USER" },
                    { "Administrators", "APPLICATION_USERS_MANAGE" },
                    { "Administrators", "DELETE_USER" }
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
                name: "IX_Elements_Acronym",
                table: "Elements",
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
                name: "GroupPermissions");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "SitesUsers");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropTable(
                name: "ApplicationPermissions");

            migrationBuilder.DropTable(
                name: "Elements");

            migrationBuilder.DropTable(
                name: "ApplicationGroups");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "Sites");
        }
    }
}
