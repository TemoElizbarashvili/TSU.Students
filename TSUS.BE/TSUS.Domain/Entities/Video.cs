using System.ComponentModel.DataAnnotations;

namespace TSUS.Domain.Entities;

public class Video
{
    public int VideoId { get; set; }
    [StringLength(40)]
    public string Name { get; set; } = default!;
    [StringLength(Int32.MaxValue)]
    public string Link { get; set; } = default!;

    //Relations
    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = default!;
}