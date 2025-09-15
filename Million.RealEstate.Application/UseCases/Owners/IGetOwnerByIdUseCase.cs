using Million.RealEstate.Application.DTOs;

namespace Million.RealEstate.Application.UseCases.Owners
{
    public interface IGetOwnerByIdUseCase
    {
        Task<OwnerDto?> ExecuteAsync(int id);
    }
}