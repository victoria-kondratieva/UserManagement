using UserManagement.Domain.DTOs.User;
using E = UserManagement.Domain.Entities;

namespace UserManagement.WebApi.Services.User;

public interface IUserService
{
    Task<E.User> GetByIdAsync(int id);
    Task<IEnumerable<E.User>> GetAllAsync(PaginationFilter paginationFilter, Filter filter, string sortOrder);
    Task<E.User> UpdateAsync(UserRequest request, int id);
    Task<E.User> CreateAsync(UserRequest request);
    Task<E.User> AddRoleAsync(int userId, int roleId);
    Task DeleteAsync(int id);
}
