using Million.RealEstate.Application.DTOs;

namespace Million.RealEstate.Application.UseCases.Owners
{
    public interface IUpdateOwnerUseCase
    {
        Task<bool> ExecuteAsync(int id, UpdateOwnerDto dto);
    }
}