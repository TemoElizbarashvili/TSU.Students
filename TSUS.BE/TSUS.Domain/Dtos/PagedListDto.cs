using System.ComponentModel.DataAnnotations;

namespace TSUS.Domain.Dtos;

public class PagedListDto<T>
{
    public List<T> List { get; set; } = default!;
    public bool IsLastPage { get; set; }
    public int? LastEntityId { get; set; }
}

