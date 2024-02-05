using System.ComponentModel.DataAnnotations;

namespace TSUS.Domain.Dtos;

public record DepartmentDto()
{
    [Required][StringLength(50)] public string Name { get; set; } = default!;
    [Required] public int FacultyId { get; set; }
}