using System.ComponentModel.DataAnnotations;

namespace TSUS.Domain.Dtos;

public record LoginDto
{
    [Required] public string Email { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
}