using Million.RealEstate.Application.DTOs;

namespace Million.RealEstate.Application.UseCases.Properties
{
    public interface ICreatePropertyUseCase
    {
        Task<int> ExecuteAsync(CreatePropertyDto dto);
    }
}
