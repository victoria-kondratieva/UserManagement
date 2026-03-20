namespace UserManagement.Domain.DTOs.User;

public class Filter
{
    public string? Email { get; set; }
    public string? Name { get; set; }
    public int MinAge { get; set; }
    public int MaxAge { get; set; }
    public string? RoleName { get; set; }
}