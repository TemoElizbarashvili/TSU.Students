using System.ComponentModel.DataAnnotations;

namespace TSUS.Domain.Entities;

public class Review
{
    public int ReviewId { get; set; }
    [Required]
    [StringLength(50)]
    public string Title { get; set; } = default!;
    [Required] 
    public string? DetailedReview { get; set; }
}

