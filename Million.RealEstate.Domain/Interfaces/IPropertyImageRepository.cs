using Million.RealEstate.Domain.Entities;

namespace Million.RealEstate.Domain.Interfaces
{
    public interface IPropertyImageRepository : IGenericRepository<PropertyImage>
    {
        Task<IEnumerable<PropertyImage>> GetByPropertyIdAsync(int propertyId);
    }
}