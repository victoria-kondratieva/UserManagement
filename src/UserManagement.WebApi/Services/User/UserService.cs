using AutoMapper;
using UserManagement.Domain.DTOs.User;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;
using UserManagement.Domain.Constants;
using UserManagement.WebApi.Helpers;
using E = UserManagement.Domain.Entities;

namespace UserManagement.WebApi.Services.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<E.User> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        return user ?? throw new HttpException(StatusCodes.Status404NotFound, $"User with id:{id} not found.");
    }

    public async Task<IEnumerable<E.User>> GetAllAsync(PaginationFilter paginationFilter, Filter filter, string sortOrder)
    {
        if (paginationFilter.PageNumber < 1) paginationFilter.PageNumber = 1;

        return await _userRepository.GetAllAsync(paginationFilter, filter, sortOrder);
    }

    public async Task<E.User> UpdateAsync(UserRequest request, int id)
    {
        if (request.Id != id)
            throw new HttpException(StatusCodes.Status400BadRequest, "IDs don't match.");

        if (!await _userRepository.ExistsByIdAsync(id))
            throw new HttpException(StatusCodes.Status404NotFound, $"User with id:{id} not found.");

        if (await _userRepository.ExistsEmailAsync(request.Email))
            throw new HttpException(StatusCodes.Status409Conflict, $"Email {request.Email} is already taken.");

        var user = _mapper.Map<E.User>(request);

        return await _userRepository.UpdateAsync(user);
    }

    public async Task<E.User> CreateAsync(UserRequest request)
    {
        if (await _userRepository.ExistsByIdAsync(request.Id))
            throw new HttpException(StatusCodes.Status409Conflict, "User with this ID already exists.");

        if (await _userRepository.ExistsEmailAsync(request.Email))
            throw new HttpException(StatusCodes.Status409Conflict, "User with this email already exists.");

        var user = _mapper.Map<E.User>(request);

        var userRole = await _roleRepository.GetByIdAsync(Constants.UserRoleId)
            ?? throw new HttpException(StatusCodes.Status500InternalServerError, "Default role not found.");

        user.Roles = new List<Role> { userRole };

        return await _userRepository.CreateAsync(user);
    }

    public async Task<E.User> AddRoleAsync(int userId, int roleId)
    {
        var user = await _userRepository.GetByIdAsync(userId)
             ?? throw new HttpException(StatusCodes.Status404NotFound, $"User {userId} not found.");

        var role = await _roleRepository.GetByIdAsync(roleId)
            ?? throw new HttpException(StatusCodes.Status404NotFound, $"Role {roleId} not found.");

        if (user.Roles.Any(r => r.Id == roleId))
        {
            throw new HttpException(StatusCodes.Status400BadRequest, "Role already assigned.");
        }

        return await _userRepository.AddRoleAsync(user, role);
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id)
           ?? throw new HttpException(StatusCodes.Status404NotFound, $"User with id:{id} not found.");
      
        await _userRepository.DeleteAsync(user);
    }
}
