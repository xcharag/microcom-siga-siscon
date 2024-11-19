namespace BaseLibrary.Entities.Client;

public class ClientType : BaseEntity
{
    public decimal? Discount { get; set; }
    
    //One-to-Many Relationships
    public List<Client>? Clients { get; set; }
}