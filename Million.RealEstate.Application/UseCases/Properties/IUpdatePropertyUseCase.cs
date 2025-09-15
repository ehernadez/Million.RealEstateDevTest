using Million.RealEstate.Application.DTOs;

namespace Million.RealEstate.Application.UseCases.Properties
{
    public interface IUpdatePropertyUseCase
    {
        Task<bool> ExecuteAsync(int id, UpdatePropertyDto dto);
    }
}