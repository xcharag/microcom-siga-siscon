using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities;

public class DocumentoBanco
{
    [Required]
    [Key]
    public int CodDocumentoBanco { get; set; }
    
    //Many-to-One Relationship
    public Documento? Documento { get; set; }
    public int CodDoc { get; set; }
    
    public Banco? Banco { get; set; }
    public int CodBanco { get; set; }
}