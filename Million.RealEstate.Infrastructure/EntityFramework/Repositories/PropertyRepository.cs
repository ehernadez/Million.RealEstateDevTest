using Microsoft.EntityFrameworkCore;
using Million.RealEstate.Domain.Entities;
using Million.RealEstate.Domain.Interfaces;
using System.Linq.Expressions;
using Million.RealEstate.Infrastructure.EntityFramework.Data;

namespace Million.RealEstate.Infrastructure.EntityFramework.Repositories
{
    public class PropertyRepository : GenericRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(RealEstateDbContext context) : base(context) { }

        public async Task<(IEnumerable<Property> Items, int TotalCount)> GetAllPaginateWithDetailsAsync(Expression<Func<Property, bool>>? filter, int pageNumber, int pageSize)
        {
            return await GetAllPaginateAsync(
                filter,
                pageNumber,
                pageSize,
                query => query
                    .Include(p => p.Owner)
                    .Include(p => p.Images.Where(i => i.Enabled)));
        }

        public async Task ChangePriceAsync(int propertyId, decimal newPrice)
        {
            var property = await _dbSet.FindAsync(propertyId);
            if (property != null)
            {
                property.Price = newPrice;
                _dbSet.Update(property);
            }
        }
    }
}
