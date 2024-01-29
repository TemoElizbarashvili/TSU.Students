using System.ComponentModel.DataAnnotations;

namespace TSUS.Domain.Entities;

public class Faculty
{
    public int FacultyId { get; set; }
    [StringLength(50)]
    public string Name { get; set; } = default!;
}