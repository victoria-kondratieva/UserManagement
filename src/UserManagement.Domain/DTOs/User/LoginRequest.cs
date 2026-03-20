using System.ComponentModel.DataAnnotations;

namespace UserManagement.Domain.DTOs.User;

public class LoginRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}