using Million.RealEstate.Domain.Entities;
using System.Linq.Expressions;

namespace Million.RealEstate.Domain.Interfaces
{
    public interface IPropertyRepository : IGenericRepository<Property>
    {
        Task<(IEnumerable<Property> Items, int TotalCount)> GetAllPaginateWithDetailsAsync(Expression<Func<Property, bool>>? filter, int pageNumber, int pageSize);
        Task ChangePriceAsync(int propertyId, decimal newPrice);
    }
}