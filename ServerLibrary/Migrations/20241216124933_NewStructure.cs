using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerLibrary.Migrations
{
    /// <inheritdoc />
    public partial class NewStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TipoDocs_Clientes_ClienteCodCli",
                table: "TipoDocs");

            migrationBuilder.DropIndex(
                name: "IX_TipoDocs_ClienteCodCli",
                table: "TipoDocs");

            migrationBuilder.DropColumn(
                name: "ClienteCodCli",
                table: "TipoDocs");

            migrationBuilder.AlterColumn<string>(
                name: "Grupo",
                table: "PlanCuentas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClienteCodCli",
                table: "TipoDocs",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Grupo",
                table: "PlanCuentas",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_TipoDocs_ClienteCodCli",
                table: "TipoDocs",
                column: "ClienteCodCli");

            migrationBuilder.AddForeignKey(
                name: "FK_TipoDocs_Clientes_ClienteCodCli",
                table: "TipoDocs",
                column: "ClienteCodCli",
                principalTable: "Clientes",
                principalColumn: "CodCli");
        }
    }
}
