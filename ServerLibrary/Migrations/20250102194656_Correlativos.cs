using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerLibrary.Migrations
{
    /// <inheritdoc />
    public partial class Correlativos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Correlativos",
                newName: "Relacion");

            migrationBuilder.AddColumn<bool>(
                name: "Actualiza",
                table: "Correlativos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Detalle",
                table: "Correlativos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Largo",
                table: "Correlativos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Actualiza",
                table: "Correlativos");

            migrationBuilder.DropColumn(
                name: "Detalle",
                table: "Correlativos");

            migrationBuilder.DropColumn(
                name: "Largo",
                table: "Correlativos");

            migrationBuilder.RenameColumn(
                name: "Relacion",
                table: "Correlativos",
                newName: "Nombre");
        }
    }
}
