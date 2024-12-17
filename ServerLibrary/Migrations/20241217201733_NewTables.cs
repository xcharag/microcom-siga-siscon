using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerLibrary.Migrations
{
    /// <inheritdoc />
    public partial class NewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlanCuentaCodCuenta",
                table: "DetalleDocumentos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Moneda",
                table: "Bancos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Correlativos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Correlativos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sucursal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gestiones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mes = table.Column<int>(type: "int", nullable: false),
                    Anho = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gestiones", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleDocumentos_PlanCuentaCodCuenta",
                table: "DetalleDocumentos",
                column: "PlanCuentaCodCuenta");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleDocumentos_PlanCuentas_PlanCuentaCodCuenta",
                table: "DetalleDocumentos",
                column: "PlanCuentaCodCuenta",
                principalTable: "PlanCuentas",
                principalColumn: "CodCuenta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleDocumentos_PlanCuentas_PlanCuentaCodCuenta",
                table: "DetalleDocumentos");

            migrationBuilder.DropTable(
                name: "Correlativos");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "Gestiones");

            migrationBuilder.DropIndex(
                name: "IX_DetalleDocumentos_PlanCuentaCodCuenta",
                table: "DetalleDocumentos");

            migrationBuilder.DropColumn(
                name: "PlanCuentaCodCuenta",
                table: "DetalleDocumentos");

            migrationBuilder.DropColumn(
                name: "Moneda",
                table: "Bancos");
        }
    }
}
