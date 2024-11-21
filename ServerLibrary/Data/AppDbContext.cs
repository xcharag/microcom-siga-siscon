using BaseLibrary.Entities;
using BaseLibrary.Entities.Client;
using Microsoft.EntityFrameworkCore;

namespace ServerLibrary.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<ClientType> ClientTypes { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<ContactType> ContactTypes { get; set; }
    public DbSet<DocumentType> DocumentTypes { get; set; }
    public DbSet<City> Cities { get; set; }
}