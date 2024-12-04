using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace ServerLibrary.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<TipoCliente> TipoClientes { get; set; }
    public DbSet<TipoDoc> TipoDocs { get; set; }
    public DbSet<Proveedor> Proveedores { get; set; }
    public DbSet<Banco> Bancos { get; set; }
    public DbSet<PlanCuenta> PlanCuentas { get; set; }
    public DbSet<PlanCuentaMayor> PlanCuentaMayores { get; set; }
    public DbSet<Documento> Documentos { get; set; }
    public DbSet<DetalleDocumento> DetalleDocumentos { get; set; }
    public DbSet<TcCosto> TcCostos { get; set; }
    public DbSet<TipoEgreso> TipoEgresos { get; set; }
    public DbSet<Anexos> Anexos { get; set; }
    
    //This is for the security
    public DbSet<SystemRole> SystemRoles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    
    public DbSet<SystemPermission> SystemPermissions { get; set; }
    
    public DbSet<MenuItems> MenuItems { get; set; }
    
    //This is for the refresh token
    public DbSet<RefreshTokenInfo> RefreshTokenInfos { get; set; }
}