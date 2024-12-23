using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerLibrary.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Grupos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodGrupo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Niveles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodNivel = table.Column<int>(type: "int", nullable: false),
                    Largo = table.Column<int>(type: "int", nullable: false),
                    Cuantos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Niveles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanCuentas",
                columns: table => new
                {
                    CodCuenta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomCuenta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Moneda = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoCuenta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    Grupo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanCuentas", x => x.CodCuenta);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokenInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokenInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemPermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TcCostos",
                columns: table => new
                {
                    CodCc = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomCc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomResp = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TcCostos", x => x.CodCc);
                });

            migrationBuilder.CreateTable(
                name: "TipoClientes",
                columns: table => new
                {
                    CodTipoCli = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomTipoCli = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoClientes", x => x.CodTipoCli);
                });

            migrationBuilder.CreateTable(
                name: "TipoDocs",
                columns: table => new
                {
                    CodTipoDoc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomTipoDoc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDocs", x => x.CodTipoDoc);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    CodUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.CodUsuario);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Block = table.Column<int>(type: "int", nullable: true),
                    MenuId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItems_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bancos",
                columns: table => new
                {
                    CodBanco = table.Column<int>(type: "int", nullable: false),
                    NomBanco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Moneda = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanCuentaCodCuenta = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bancos", x => x.CodBanco);
                    table.ForeignKey(
                        name: "FK_Bancos_PlanCuentas_PlanCuentaCodCuenta",
                        column: x => x.PlanCuentaCodCuenta,
                        principalTable: "PlanCuentas",
                        principalColumn: "CodCuenta");
                });

            migrationBuilder.CreateTable(
                name: "TipoEgresos",
                columns: table => new
                {
                    CodTipoEgreso = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanCuentaCodCuenta = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEgresos", x => x.CodTipoEgreso);
                    table.ForeignKey(
                        name: "FK_TipoEgresos_PlanCuentas_PlanCuentaCodCuenta",
                        column: x => x.PlanCuentaCodCuenta,
                        principalTable: "PlanCuentas",
                        principalColumn: "CodCuenta");
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    CodCli = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomCli = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contacto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirCli = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelCli = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailCli = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoDoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NroDoc = table.Column<int>(type: "int", nullable: false),
                    CodigoEx = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LimiteCre = table.Column<float>(type: "real", nullable: false),
                    FacturarA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientTypeCodTipoCli = table.Column<int>(type: "int", nullable: true),
                    UserCodUsuario = table.Column<int>(type: "int", nullable: true),
                    PlanCuentaCodCuenta = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.CodCli);
                    table.ForeignKey(
                        name: "FK_Clientes_PlanCuentas_PlanCuentaCodCuenta",
                        column: x => x.PlanCuentaCodCuenta,
                        principalTable: "PlanCuentas",
                        principalColumn: "CodCuenta");
                    table.ForeignKey(
                        name: "FK_Clientes_TipoClientes_ClientTypeCodTipoCli",
                        column: x => x.ClientTypeCodTipoCli,
                        principalTable: "TipoClientes",
                        principalColumn: "CodTipoCli");
                    table.ForeignKey(
                        name: "FK_Clientes_Usuarios_UserCodUsuario",
                        column: x => x.UserCodUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "CodUsuario");
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    CodProveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomProv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirProv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelProv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailProv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contacto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoDoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NroDoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodigoEx = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LimiteCre = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlanCuentaCodCuenta = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UsuarioCodUsuario = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.CodProveedor);
                    table.ForeignKey(
                        name: "FK_Proveedores_PlanCuentas_PlanCuentaCodCuenta",
                        column: x => x.PlanCuentaCodCuenta,
                        principalTable: "PlanCuentas",
                        principalColumn: "CodCuenta");
                    table.ForeignKey(
                        name: "FK_Proveedores_Usuarios_UsuarioCodUsuario",
                        column: x => x.UsuarioCodUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "CodUsuario");
                });

            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    NroDoc = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FechaDoc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NroCheque = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Glosa1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Glosa2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Moneda = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Importe = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Origen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProveedorCodProveedor = table.Column<int>(type: "int", nullable: true),
                    BancoCodBanco = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentos", x => x.NroDoc);
                    table.ForeignKey(
                        name: "FK_Documentos_Bancos_BancoCodBanco",
                        column: x => x.BancoCodBanco,
                        principalTable: "Bancos",
                        principalColumn: "CodBanco");
                    table.ForeignKey(
                        name: "FK_Documentos_Proveedores_ProveedorCodProveedor",
                        column: x => x.ProveedorCodProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "CodProveedor");
                });

            migrationBuilder.CreateTable(
                name: "DetalleDocumentos",
                columns: table => new
                {
                    NroDetalleDoc = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Correl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mtobd = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Mtodd = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Mtobh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Mtodh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Glosa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TcCostoCodCc = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DocumentoNroDoc = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PlanCuentaCodCuenta = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleDocumentos", x => x.NroDetalleDoc);
                    table.ForeignKey(
                        name: "FK_DetalleDocumentos_Documentos_DocumentoNroDoc",
                        column: x => x.DocumentoNroDoc,
                        principalTable: "Documentos",
                        principalColumn: "NroDoc");
                    table.ForeignKey(
                        name: "FK_DetalleDocumentos_PlanCuentas_PlanCuentaCodCuenta",
                        column: x => x.PlanCuentaCodCuenta,
                        principalTable: "PlanCuentas",
                        principalColumn: "CodCuenta");
                    table.ForeignKey(
                        name: "FK_DetalleDocumentos_TcCostos_TcCostoCodCc",
                        column: x => x.TcCostoCodCc,
                        principalTable: "TcCostos",
                        principalColumn: "CodCc");
                });

            migrationBuilder.CreateTable(
                name: "Anexos",
                columns: table => new
                {
                    NroAnexo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Correl = table.Column<int>(type: "int", nullable: false),
                    Nit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaFactura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NroAutorizacion = table.Column<int>(type: "int", nullable: false),
                    NroFactura = table.Column<int>(type: "int", nullable: false),
                    Glosa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Importe = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sujeto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Mtoiva = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Excento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DetalleDocumentoNroDetalleDoc = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TipoEgresoCodTipoEgreso = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProveedorCodProveedor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexos", x => x.NroAnexo);
                    table.ForeignKey(
                        name: "FK_Anexos_DetalleDocumentos_DetalleDocumentoNroDetalleDoc",
                        column: x => x.DetalleDocumentoNroDetalleDoc,
                        principalTable: "DetalleDocumentos",
                        principalColumn: "NroDetalleDoc");
                    table.ForeignKey(
                        name: "FK_Anexos_Proveedores_ProveedorCodProveedor",
                        column: x => x.ProveedorCodProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "CodProveedor");
                    table.ForeignKey(
                        name: "FK_Anexos_TipoEgresos_TipoEgresoCodTipoEgreso",
                        column: x => x.TipoEgresoCodTipoEgreso,
                        principalTable: "TipoEgresos",
                        principalColumn: "CodTipoEgreso");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anexos_DetalleDocumentoNroDetalleDoc",
                table: "Anexos",
                column: "DetalleDocumentoNroDetalleDoc");

            migrationBuilder.CreateIndex(
                name: "IX_Anexos_ProveedorCodProveedor",
                table: "Anexos",
                column: "ProveedorCodProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Anexos_TipoEgresoCodTipoEgreso",
                table: "Anexos",
                column: "TipoEgresoCodTipoEgreso");

            migrationBuilder.CreateIndex(
                name: "IX_Bancos_PlanCuentaCodCuenta",
                table: "Bancos",
                column: "PlanCuentaCodCuenta");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_ClientTypeCodTipoCli",
                table: "Clientes",
                column: "ClientTypeCodTipoCli");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_PlanCuentaCodCuenta",
                table: "Clientes",
                column: "PlanCuentaCodCuenta");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_UserCodUsuario",
                table: "Clientes",
                column: "UserCodUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleDocumentos_DocumentoNroDoc",
                table: "DetalleDocumentos",
                column: "DocumentoNroDoc");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleDocumentos_PlanCuentaCodCuenta",
                table: "DetalleDocumentos",
                column: "PlanCuentaCodCuenta");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleDocumentos_TcCostoCodCc",
                table: "DetalleDocumentos",
                column: "TcCostoCodCc");

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_BancoCodBanco",
                table: "Documentos",
                column: "BancoCodBanco");

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_ProveedorCodProveedor",
                table: "Documentos",
                column: "ProveedorCodProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_MenuId",
                table: "MenuItems",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_PlanCuentaCodCuenta",
                table: "Proveedores",
                column: "PlanCuentaCodCuenta");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_UsuarioCodUsuario",
                table: "Proveedores",
                column: "UsuarioCodUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_TipoEgresos_PlanCuentaCodCuenta",
                table: "TipoEgresos",
                column: "PlanCuentaCodCuenta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anexos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Correlativos");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "Gestiones");

            migrationBuilder.DropTable(
                name: "Grupos");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Niveles");

            migrationBuilder.DropTable(
                name: "RefreshTokenInfos");

            migrationBuilder.DropTable(
                name: "SystemPermissions");

            migrationBuilder.DropTable(
                name: "SystemRoles");

            migrationBuilder.DropTable(
                name: "TipoDocs");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "DetalleDocumentos");

            migrationBuilder.DropTable(
                name: "TipoEgresos");

            migrationBuilder.DropTable(
                name: "TipoClientes");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.DropTable(
                name: "TcCostos");

            migrationBuilder.DropTable(
                name: "Bancos");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "PlanCuentas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
