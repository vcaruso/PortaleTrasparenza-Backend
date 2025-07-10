using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENINET.TransparentPortal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLatituteandLongitudetoSite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Sites",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Sites",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Sites",
                keyColumn: "Acronym",
                keyValue: "RO",
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 0.0, 0.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Sites");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Sites");
        }
    }
}
