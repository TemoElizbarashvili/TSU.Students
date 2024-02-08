using System.ComponentModel.DataAnnotations;

namespace TSUS.Domain.Entities;

public class VerifyCodes
{
    [Required]
    public int VerifyCode { get; set; }
    [Required]
    [StringLength(50)]
    //TODO: Uncomment when Testing ends
    //[RegularExpression(@"^[a-zA-Z0-9._-]+@([a-zA-Z0-9-]+\.)?tsu\.edu\.ge$")]
    public string Email { get; set; } = default!;
    public int Attempt { get; set; }
}

