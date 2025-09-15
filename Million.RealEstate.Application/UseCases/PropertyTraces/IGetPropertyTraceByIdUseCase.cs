using Million.RealEstate.Application.DTOs;

namespace Million.RealEstate.Application.UseCases.PropertyTraces
{
    public interface IGetPropertyTraceByIdUseCase
    {
        Task<PropertyTraceDto?> ExecuteAsync(int id);
    }
}