using System.ComponentModel.DataAnnotations;

namespace TSUS.Domain.Entities;

public class User
{
    public int UserId { get; set; }
    [StringLength(50)]
    public string Email { get; set; } = default!;
    [StringLength(50)]
    public string UserName { get; set; } = default!;
    [StringLength(25)]
    public string Password { get; set; } = default!;
    public byte[]? ProfilePicture { get; set; }
    public Role Role { get; set; }

    //Relations
    public int DepartmentId { get; set; }
    public Department Department { get; set; } = default!;

}

public enum Role
{
    User = 0,
    Moderator = 1,
    Admin = 2
}

