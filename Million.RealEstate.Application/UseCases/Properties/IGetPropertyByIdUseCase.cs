using Million.RealEstate.Application.DTOs;

namespace Million.RealEstate.Application.UseCases.Properties
{
    public interface IGetPropertyByIdUseCase
    {
        Task<PropertyDto?> ExecuteAsync(int id);
    }
}