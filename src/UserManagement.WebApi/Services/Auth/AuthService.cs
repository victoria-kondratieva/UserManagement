using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagement.Domain.DTOs.User;

namespace UserManagement.WebApi.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IConfiguration Configuration;

    public AuthService(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public string GenerateJwtToken(LoginRequest model)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration["JWT:SecretKey"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", model.Email) }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = Configuration["JWT:Issuer"],
            Audience = Configuration["JWT:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
