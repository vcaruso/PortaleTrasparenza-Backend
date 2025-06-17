using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENINET.TransaprentPortal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AggiuntoFileLengthatabellaReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FileLength",
                table: "Reports",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileLength",
                table: "Reports");
        }
    }
}
