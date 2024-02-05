using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TSUS.Domain.Entities;

public class Video
{
    public int VideoId { get; set; }
    [StringLength(40)]
    public string Name { get; set; } = default!;
    [StringLength(Int32.MaxValue)]
    public string Link { get; set; } = default!;

    //Relations
    [ForeignKey("Subject")]
    public int SubjectId { get; set; }
    public Subject? Subject { get; set; }
}