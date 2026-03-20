using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.DTOs.User;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;
using UserManagement.Domain.Constants;
using UserManagement.Infrastructure.Helpers;

namespace UserManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> GetAllAsync(PaginationFilter paginationFilter, Filter filter, string sortOrder)
        {
            var users = _context.Users.Include(u => u.Roles).AsQueryable();

            if (!string.IsNullOrEmpty(filter.RoleName))
                users = users.Where(u => u.Roles.Any(r => r.Name.ToLower().Contains(filter.RoleName.ToLower())));
            

            if (!string.IsNullOrEmpty(filter.Name))
                users = users.Where(u => u.Name.ToLower().Contains(filter.Name.ToLower()));
            
            if (!string.IsNullOrEmpty(filter.Email))
                users = users.Where(u => u.Email.ToLower().Contains(filter.Email.ToLower()));
            
            if (filter.MinAge > 0)
                users = users.Where(u => u.Age >= filter.MinAge);
            
            if (filter.MaxAge > 0)
                users = users.Where(u => u.Age <= filter.MaxAge);

            switch (sortOrder)
            {
                case Constants.NameAsc:
                    users = users.OrderBy(u => u.Name);
                    break;
                case Constants.NameDesc:
                    users = users.OrderByDescending(u => u.Name);
                    break;
                case Constants.AgeAsc:
                    users = users.OrderBy(u => u.Age);
                    break;
                case Constants.AgeDesc:
                    users = users.OrderByDescending(u => u.Age);
                    break;
                case Constants.EmailAsc:
                    users = users.OrderBy(u => u.Email);
                    break;
                case Constants.EmailDesc:
                    users = users.OrderByDescending(u => u.Email);
                    break;
            }

            var validPaginationFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);

            return await users
                   .Skip((validPaginationFilter.PageNumber - 1) * validPaginationFilter.PageSize)
                   .Take(validPaginationFilter.PageSize)
                   .ToListAsync();
        }

        public async Task<User> CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return user;
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
        }

        public async Task<User> AddRoleAsync(User user, Role role)
        {
            user.Roles.Add(role);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> ExistsEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }
    }
}
