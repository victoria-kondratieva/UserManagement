using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.WebApi.Helpers;
using UserManagement.WebApi.Services.User;
using UserManagement.Domain.Entities;
using UserManagement.Domain.DTOs.User;

namespace UserManagement.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Outputs a list of Users with their Roles
    /// </summary>
    /// <response code="200">Users displayed</response>
    /// <response code="404">Not found</response>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<User>>> GetAllAsync(
        [FromQuery] PaginationFilter paginationFilter, 
        [FromQuery] Filter filter,
        [FromQuery] string sortOrder = "NameAsc")
    {
        Logger.Info("Requested GET: api/Users");

        var result = await _userService.GetAllAsync(paginationFilter, filter, sortOrder);
        return Ok(result);
    }

    /// <summary>
    /// Outputs the User by ID
    /// </summary>
    /// <response code="200">User displayed</response>
    /// <response code="404">Not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<User>> GetByIdAsync(int id)
    {
        Logger.Info($"Requested GET: api/Users/{id}");

        try
        {
            var result = await _userService.GetByIdAsync(id);
            return Ok(result);
        }
        catch (HttpException ex)
        {
            Logger.Error(ex.Message);

            return StatusCode(ex.StatusCode, ex.Message);
        }
    }

    /// <summary>
    /// Updates User data by ID
    /// </summary>
    /// <response code="200">User data updated</response>
    /// <response code="400">Input data is invalid</response>
    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAsync(UserRequest model, int id)
    {
        Logger.Info($"Requested PUT: api/Users/{id}");

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _userService.UpdateAsync(model, id);

            return Ok("User data updated");
        }
        catch (HttpException ex)
        {
            Logger.Error(ex.Message);

            return StatusCode(ex.StatusCode, ex.Message);
        }
    }

    /// <summary>
    /// Creates a User
    /// </summary>
    /// <response code="201">User created</response>
    /// <response code="400">Input data is invalid</response>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<User>> CreateAsync(UserRequest model)
    {
        Logger.Info("Requested POST: api/Users/");

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _userService.CreateAsync(model);
            return Ok(result);
        }
        catch (HttpException ex)
        {
            Logger.Error(ex.Message);

            return StatusCode(ex.StatusCode, ex.Message);
        }
    }

    /// <summary>
    /// Adds a Role to the User by IDs
    /// </summary>
    /// <response code="200">A role has been added to the User</response>
    /// <response code="400">Input data is invalid</response>
    [Authorize]
    [Route("{userId}/AddRole/{roleId}")]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddRoleAsync(int userId, int roleId)
    {
        Logger.Info($"Requested PUT: api/Users/{userId}/AddRole/{roleId}");

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _userService.AddRoleAsync(userId, roleId);
            return Ok(result);
        }
        catch (HttpException ex)
        {
            Logger.Error(ex.Message);

            return StatusCode(ex.StatusCode, ex.Message);
        }
    }

    /// <summary>
    /// Deletes a User by ID
    /// </summary>
    /// <response code="200">User deleted</response>
    /// <response code="400">Input data is invalid</response>
    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        Logger.Info($"Requested DELETE: api/Users/{id}");

        try
        {
            await _userService.DeleteAsync(id);

            return Ok("User deleted");
        }
        catch (HttpException ex)
        {
            Logger.Error(ex.Message);

            return StatusCode(ex.StatusCode, ex.Message);
        }
    }
}
