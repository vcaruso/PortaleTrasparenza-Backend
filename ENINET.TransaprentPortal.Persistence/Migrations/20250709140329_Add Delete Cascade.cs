using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENINET.TransparentPortal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDeleteCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElementsSite_Elements_ElementName",
                table: "ElementsSite");

            migrationBuilder.DropForeignKey(
                name: "FK_ElementsSite_Sites_Acronym",
                table: "ElementsSite");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPermissions_ApplicationGroups_GroupName",
                table: "GroupPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPermissions_ApplicationPermissions_Permission",
                table: "GroupPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Elements_ElementName",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Sites_Acronym",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_SitesUsers_ApplicationUsers_UserId",
                table: "SitesUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_SitesUsers_Sites_Acronym",
                table: "SitesUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_ApplicationGroups_GroupName",
                table: "UserGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_ApplicationUsers_Userid",
                table: "UserGroups");

            migrationBuilder.AddForeignKey(
                name: "FK_ElementsSite_Elements_ElementName",
                table: "ElementsSite",
                column: "ElementName",
                principalTable: "Elements",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ElementsSite_Sites_Acronym",
                table: "ElementsSite",
                column: "Acronym",
                principalTable: "Sites",
                principalColumn: "Acronym",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPermissions_ApplicationGroups_GroupName",
                table: "GroupPermissions",
                column: "GroupName",
                principalTable: "ApplicationGroups",
                principalColumn: "GroupName",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPermissions_ApplicationPermissions_Permission",
                table: "GroupPermissions",
                column: "Permission",
                principalTable: "ApplicationPermissions",
                principalColumn: "Permission",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Elements_ElementName",
                table: "Reports",
                column: "ElementName",
                principalTable: "Elements",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Sites_Acronym",
                table: "Reports",
                column: "Acronym",
                principalTable: "Sites",
                principalColumn: "Acronym",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SitesUsers_ApplicationUsers_UserId",
                table: "SitesUsers",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SitesUsers_Sites_Acronym",
                table: "SitesUsers",
                column: "Acronym",
                principalTable: "Sites",
                principalColumn: "Acronym",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_ApplicationGroups_GroupName",
                table: "UserGroups",
                column: "GroupName",
                principalTable: "ApplicationGroups",
                principalColumn: "GroupName",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_ApplicationUsers_Userid",
                table: "UserGroups",
                column: "Userid",
                principalTable: "ApplicationUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElementsSite_Elements_ElementName",
                table: "ElementsSite");

            migrationBuilder.DropForeignKey(
                name: "FK_ElementsSite_Sites_Acronym",
                table: "ElementsSite");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPermissions_ApplicationGroups_GroupName",
                table: "GroupPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPermissions_ApplicationPermissions_Permission",
                table: "GroupPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Elements_ElementName",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Sites_Acronym",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_SitesUsers_ApplicationUsers_UserId",
                table: "SitesUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_SitesUsers_Sites_Acronym",
                table: "SitesUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_ApplicationGroups_GroupName",
                table: "UserGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_ApplicationUsers_Userid",
                table: "UserGroups");

            migrationBuilder.AddForeignKey(
                name: "FK_ElementsSite_Elements_ElementName",
                table: "ElementsSite",
                column: "ElementName",
                principalTable: "Elements",
                principalColumn: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_ElementsSite_Sites_Acronym",
                table: "ElementsSite",
                column: "Acronym",
                principalTable: "Sites",
                principalColumn: "Acronym");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPermissions_ApplicationGroups_GroupName",
                table: "GroupPermissions",
                column: "GroupName",
                principalTable: "ApplicationGroups",
                principalColumn: "GroupName");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPermissions_ApplicationPermissions_Permission",
                table: "GroupPermissions",
                column: "Permission",
                principalTable: "ApplicationPermissions",
                principalColumn: "Permission");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Elements_ElementName",
                table: "Reports",
                column: "ElementName",
                principalTable: "Elements",
                principalColumn: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Sites_Acronym",
                table: "Reports",
                column: "Acronym",
                principalTable: "Sites",
                principalColumn: "Acronym");

            migrationBuilder.AddForeignKey(
                name: "FK_SitesUsers_ApplicationUsers_UserId",
                table: "SitesUsers",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SitesUsers_Sites_Acronym",
                table: "SitesUsers",
                column: "Acronym",
                principalTable: "Sites",
                principalColumn: "Acronym");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_ApplicationGroups_GroupName",
                table: "UserGroups",
                column: "GroupName",
                principalTable: "ApplicationGroups",
                principalColumn: "GroupName");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_ApplicationUsers_Userid",
                table: "UserGroups",
                column: "Userid",
                principalTable: "ApplicationUsers",
                principalColumn: "UserId");
        }
    }
}
