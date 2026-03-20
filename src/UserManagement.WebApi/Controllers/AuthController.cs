using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.WebApi.Helpers;
using UserManagement.WebApi.Services.Auth;
using UserManagement.WebApi.Services.User;
using UserManagement.Domain.DTOs.User;

namespace UserManagement.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration Configuration;
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public AuthController(IConfiguration configuration, IUserService userService, IAuthService authService)
    {
        Configuration = configuration;
        _userService = userService;
        _authService = authService;
    }

    /// <summary>
    /// Authenticates the User
    /// </summary>
    [AllowAnonymous]
    [HttpPost]
    public IActionResult Auth([FromQuery] LoginRequest model)
    {
        Logger.Info("Requested GET: api/Auth");

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var tokenString = _authService.GenerateJwtToken(model);

        return Ok(new { Token = tokenString, Message = "Success" });
    }

    /// <summary>
    /// Сhecks User authenticate
    /// </summary>
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet(nameof(GetResult))]
    public IActionResult GetResult()
    {
        Logger.Info("Requested GET: api/GetResult");

        return Ok("Authorized");
    }
}
