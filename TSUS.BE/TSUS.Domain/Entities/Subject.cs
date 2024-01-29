using System.ComponentModel.DataAnnotations;

namespace TSUS.Domain.Entities;

public class Subject
{
    public int SubjectId { get; set; }
    [StringLength(50)]
    public string Name { get; set; } = default!;
    public int Credit { get; set; }
    public bool IsMandatory { get; set; }
    public int? Semester { get; set; }
    public string? Description { get; set; }

    //Relations
    public List<Lecturer>? Lecturers { get; set; }
    public int DepartmentId { get; set; }
    public Department? Department { get; set; } 
}