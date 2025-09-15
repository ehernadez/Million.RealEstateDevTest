using Million.RealEstate.Application.DTOs;

namespace Million.RealEstate.Application.UseCases.PropertyTraces
{
    public interface IGetPropertyTracesByPropertyUseCase
    {
        Task<IEnumerable<PropertyTraceDto>> ExecuteAsync(int propertyId);
    }
}