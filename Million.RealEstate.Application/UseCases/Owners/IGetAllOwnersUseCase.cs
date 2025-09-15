using Million.RealEstate.Application.DTOs;

namespace Million.RealEstate.Application.UseCases.Owners
{
    public interface IGetAllOwnersUseCase
    {
        Task<OwnerPagedResponseDto> ExecuteAsync(int pageNumber, int pageSize);
    }
}