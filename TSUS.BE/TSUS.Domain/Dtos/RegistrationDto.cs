using System.ComponentModel.DataAnnotations;

namespace TSUS.Domain.Dtos;

public record RegistrationDto()
{
    [Required] [StringLength(50)]
    public string Email { get; set; } = string.Empty;

    [Required] [StringLength(50)] 
    public string UserName { get; set; } = string.Empty;

    [Required] [StringLength(25)] 
    public string Password { get; set; } = string.Empty;

    public int DepartmentId { get; set; }
}
