using System.ComponentModel.DataAnnotations;

namespace TSUS.Domain.Entities;

public class Lecturer
{
    public int LecturerId { get; set; }
    [StringLength(10)]
    public string Name { get; set; } = default!;
    [StringLength(20)]
    public string LastName { get; set; } = default!;
    [StringLength(30)]
    public string? PhoneNumber { get; set; }
    [StringLength(300)]
    public string? Description { get; set; } 
    public double? Rating { get; set; }

    //Relations
    public List<Subject>? Subjects { get; set; }
}
