namespace BaseLibrary.Entities.Client;

public class DocumentType : BaseEntity
{
    //One-to-Many Relationships
    public List<Document>? Documents { get; set; }
}