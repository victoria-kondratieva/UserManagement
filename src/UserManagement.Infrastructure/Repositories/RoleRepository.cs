using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.DTOs.User;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;
using UserManagement.Infrastructure.Helpers;

namespace UserManagement.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;

        public RoleRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
