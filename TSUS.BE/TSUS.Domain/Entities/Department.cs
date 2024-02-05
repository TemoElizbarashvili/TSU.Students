using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TSUS.Domain.Dtos;

namespace TSUS.Domain.Entities;

public class Department
{
    public int DepartmentId { get; set; }
    [StringLength(50)]
    public string Name { get; set; } = default!;

    //Relations
    [ForeignKey("Faculty")]
    public int FacultyId { get; set; }
    public Faculty? Faculty { get; set; }

    public static Department Create(DepartmentDto dto)
        => new Department()
        {
            Name = dto.Name,
            FacultyId = dto.FacultyId
        };
}
