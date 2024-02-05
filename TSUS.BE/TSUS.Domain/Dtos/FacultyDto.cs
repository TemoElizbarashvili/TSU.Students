using System.ComponentModel.DataAnnotations;

namespace TSUS.Domain.Dtos;

public record FacultyDto()
{
    [Required] [StringLength(50)] public string Name { get; set; } = default!;
}