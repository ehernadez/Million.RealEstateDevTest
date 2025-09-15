using Million.RealEstate.Application.DTOs;

namespace Million.RealEstate.Application.UseCases.PropertyTraces
{
    public interface ICreatePropertyTraceUseCase
    {
        Task<int> ExecuteAsync(int propertyId, CreatePropertyTraceDto dto);
    }
}