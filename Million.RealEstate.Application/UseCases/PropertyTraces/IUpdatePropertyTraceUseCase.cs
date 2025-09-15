using Million.RealEstate.Application.DTOs;

namespace Million.RealEstate.Application.UseCases.PropertyTraces
{
    public interface IUpdatePropertyTraceUseCase
    {
        Task<bool> ExecuteAsync(int id, UpdatePropertyTraceDto dto);
    }
}