namespace BaseLibrary.Entities.Client;

public class Document
{
    public int? Id { get; set; }
    public string? Number { get; set; }
    public string? Complement { get; set; }
    
    //Many-to-One Relationships
    public Client? Client { get; set; }
    public int ClientId { get; set; }
    
    public DocumentType? DocumentType { get; set; }
    public int DocumentTypeId { get; set; }
}