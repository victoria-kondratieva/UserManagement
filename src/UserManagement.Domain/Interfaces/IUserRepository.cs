using UserManagement.Domain.DTOs.User;
using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Interfaces;

public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
    Task<IEnumerable<User>> GetAllAsync(PaginationFilter paginationFilter, Filter filter, string sortOrder);
    Task<User> CreateAsync(User user);
    Task<User> UpdateAsync(User user);
    Task DeleteAsync(User user);
    Task<User> AddRoleAsync(User user, Role role);
    Task<bool> ExistsEmailAsync(string email);
    Task<bool> ExistsByIdAsync(int id);
}
