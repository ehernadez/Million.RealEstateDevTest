using Million.RealEstate.Application.DTOs;

namespace Million.RealEstate.Application.UseCases.PropertyImages
{
    public interface IAddPropertyImageUseCase
    {
        Task<AddPropertyImageResponseDto> ExecuteAsync(int propertyId, AddPropertyImageDto dto);
    }
}