﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TSUS.Domain.Dtos;

namespace TSUS.Domain.Entities;

public class User
{
    public int UserId { get; set; }
    [StringLength(50)]
    //TODO: Uncomment when Testing ends
    //[RegularExpression(@"^[a-zA-Z0-9._-]+@([a-zA-Z0-9-]+\.)?tsu\.edu\.ge$")]
    public string Email { get; set; } = default!;
    [StringLength(50)]
    public string UserName { get; set; } = default!;
    [StringLength(250)]
    [MinLength(8)]
    public string Password { get; set; } = default!;
    public Role Role { get; set; }
    public bool IsVerified { get; set; }

    //Relations
    [ForeignKey("Department")]
    public int DepartmentId { get; set; }
    public Department? Department { get; set; }

    public static User Create(RegistrationDto model)
        => new User()
        {
            DepartmentId = model.DepartmentId,
            Email = model.Email,
            UserName = model.UserName,
            Password = model.Password,
            IsVerified = false,
            Role = Entities.Role.User
        };
}

public enum Role
{
    User = 0,
    Moderator = 1,
    Admin = 2
}