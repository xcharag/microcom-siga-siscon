namespace BaseLibrary.Entities.Client;

public class City : BaseEntity
{
    //One-to-Many Relationships
    public List<Address>? Addresses { get; set; }
}