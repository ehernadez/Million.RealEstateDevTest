using Microsoft.EntityFrameworkCore;
using Million.RealEstate.Domain.Entities;
using Million.RealEstate.Domain.Interfaces;
using Million.RealEstate.Infrastructure.EntityFramework.Data;

namespace Million.RealEstate.Infrastructure.EntityFramework.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(RealEstateDbContext context) : base(context) { }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
    }
}