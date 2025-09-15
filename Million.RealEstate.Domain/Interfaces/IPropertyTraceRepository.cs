using Million.RealEstate.Domain.Entities;

namespace Million.RealEstate.Domain.Interfaces
{
    public interface IPropertyTraceRepository : IGenericRepository<PropertyTrace>
    {
        Task<IEnumerable<PropertyTrace>> GetByPropertyIdAsync(int propertyId);
    }
}