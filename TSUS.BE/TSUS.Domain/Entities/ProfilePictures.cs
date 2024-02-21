using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TSUS.Domain.Entities;

public class ProfilePictures
{
    public int ProfilePictureId { get; set; }
    [StringLength(50)]
    public string Type { get; set; } = default!;
    public byte[] Data { get; set; } = default!;
    public DateOnly UploadDate { get; set; }

    //Relation
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; } = default!;
}

