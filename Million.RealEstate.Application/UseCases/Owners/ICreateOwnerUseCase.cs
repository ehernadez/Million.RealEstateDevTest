using Million.RealEstate.Application.DTOs;

namespace Million.RealEstate.Application.UseCases.Owners
{
    public interface ICreateOwnerUseCase
    {
        Task<int> ExecuteAsync(CreateOwnerDto dto);
    }
}