using UserManagement.Domain.DTOs.User;

namespace UserManagement.WebApi.Services.Auth;

public interface IAuthService
{
    public string GenerateJwtToken(LoginRequest model);
}