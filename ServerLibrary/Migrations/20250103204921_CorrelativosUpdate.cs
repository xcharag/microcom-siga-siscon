using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerLibrary.Migrations
{
    /// <inheritdoc />
    public partial class CorrelativosUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gestion",
                table: "Correlativos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Prefijo",
                table: "Correlativos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Relleno",
                table: "Correlativos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sufijo",
                table: "Correlativos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gestion",
                table: "Correlativos");

            migrationBuilder.DropColumn(
                name: "Prefijo",
                table: "Correlativos");

            migrationBuilder.DropColumn(
                name: "Relleno",
                table: "Correlativos");

            migrationBuilder.DropColumn(
                name: "Sufijo",
                table: "Correlativos");
        }
    }
}
