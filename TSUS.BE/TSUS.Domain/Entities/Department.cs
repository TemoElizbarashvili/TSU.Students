using System.ComponentModel.DataAnnotations;

namespace TSUS.Domain.Entities;

public class Department
{
    public int DepartmentId { get; set; }
    [StringLength(50)]
    public string Name { get; set; } = default!;

    //Relations
    public int FacultyId { get; set; }
    public Faculty? Faculty { get; set; }
}
