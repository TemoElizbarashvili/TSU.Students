namespace TSUS.Domain.ReadModels;

public record UserInfoRm
{
    public int Id { get; set; }
    public string Mail { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public bool IsVerified { get; set; }
}