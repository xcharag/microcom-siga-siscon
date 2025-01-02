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
    public DbSet<Grupo> Grupos { get; set; }
    public DbSet<PlanCuenta> PlanCuentas { get; set; }
    public DbSet<Nivel> Niveles { get; set; }
    public DbSet<Documento> Documentos { get; set; }
    public DbSet<DetalleDocumento> DetalleDocumentos { get; set; }
    public DbSet<TcCosto> TcCostos { get; set; }
    public DbSet<TipoEgreso> TipoEgresos { get; set; }
    public DbSet<Anexos> Anexos { get; set; }
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Correlativo> Correlativos { get; set; }
    
    //This is for the security
    public DbSet<SystemRole> SystemRoles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    
    public DbSet<SystemPermission> SystemPermissions { get; set; }
    
    public DbSet<Menu> Menus { get; set; }
    
    public DbSet<MenuItems> MenuItems { get; set; }
    
    //This is for the refresh token
    public DbSet<RefreshTokenInfo> RefreshTokenInfos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PlanCuenta>()
            .Property(p => p.CodCuenta)
            .ValueGeneratedNever();
        modelBuilder.Entity<Banco>()
            .Property(p => p.CodBanco)
            .ValueGeneratedNever();
        modelBuilder.Entity<Documento>()
            .Property(p => p.NroDoc)
            .ValueGeneratedNever();
        modelBuilder.Entity<DetalleDocumento>()
            .Property(p => p.NroDetalleDoc)
            .ValueGeneratedNever();
    }
}