using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Interfaces;

public interface IRoleRepository
{
    Task<Role> GetByIdAsync(int id);
}
