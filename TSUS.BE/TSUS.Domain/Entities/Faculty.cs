using System.ComponentModel.DataAnnotations;
using TSUS.Domain.Dtos;

namespace TSUS.Domain.Entities;

public class Faculty
{
    public int FacultyId { get; set; }
    [StringLength(50)]
    public string Name { get; set; } = default!;

    public static Faculty Create(FacultyDto dto)
        => new()
        {
            Name = dto.Name
        };
}