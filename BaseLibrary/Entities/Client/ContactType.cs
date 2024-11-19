namespace BaseLibrary.Entities.Client;

public class ContactType : BaseEntity
{
    //One-to-Many Relationships
    public List<Contact>? Contacts { get; set; }
}