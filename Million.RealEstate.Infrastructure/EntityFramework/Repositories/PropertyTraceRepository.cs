using Million.RealEstate.Domain.Entities;
using Million.RealEstate.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Million.RealEstate.Infrastructure.EntityFramework.Data;

namespace Million.RealEstate.Infrastructure.EntityFramework.Repositories
{
    public class PropertyTraceRepository : GenericRepository<PropertyTrace>, IPropertyTraceRepository
    {
        public PropertyTraceRepository(RealEstateDbContext context) : base(context) { }

        public async Task<IEnumerable<PropertyTrace>> GetByPropertyIdAsync(int propertyId)
        {
            return await _dbSet.Where(trace => trace.IdProperty == propertyId).ToListAsync();
        }
    }
}
