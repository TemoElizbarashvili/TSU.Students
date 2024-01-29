using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TSUS.Domain.Entities;

public class Attachment
{
    public int AttachmentId { get; set; }
    [StringLength(30)]
    public string Name { get; set; } = default!;
    [StringLength(50)]
    public string Type { get; set; } = default!;
    public byte[] Data { get; set; } = default!;
    public DateOnly UploadDate { get; set; }
    [StringLength(300)]
    public string Description { get; set; } = default!;

    //Relation
    [ForeignKey("Subject")]
    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = default!;
}