namespace BaseLibrary.Entities.Client;

public class Contact : BaseEntity
{
    public string? Relationship { get; set; }
    public string? Number { get; set; }
    
    //Many-to-One Relationships
    public Client? Client { get; set; }
    public int ClientId { get; set; }
    
    public ContactType? ContactType { get; set; }
    public int ContactTypeId { get; set; }
}