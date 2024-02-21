namespace TSUS.Domain.ReadModels;

public record DepartmentRm
{   
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Department { get; set; }
}