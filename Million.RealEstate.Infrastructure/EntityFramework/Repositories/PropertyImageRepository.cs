using Million.RealEstate.Domain.Entities;
using Million.RealEstate.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Million.RealEstate.Infrastructure.EntityFramework.Data;

namespace Million.RealEstate.Infrastructure.EntityFramework.Repositories
{
    public class PropertyImageRepository : GenericRepository<PropertyImage>, IPropertyImageRepository
    {
        public PropertyImageRepository(RealEstateDbContext context) : base(context) { }

        public async Task<IEnumerable<PropertyImage>> GetByPropertyIdAsync(int propertyId)
        {
            return await _dbSet.Where(img => img.IdProperty == propertyId).ToListAsync();
        }
    }
}
