using Million.RealEstate.Application.DTOs;

namespace Million.RealEstate.Application.UseCases.Properties
{
    public interface IGetAllPropertiesUseCase
    {
        Task<PropertyPagedResponseDto> ExecuteAsync(PropertyFilterDto? filter, int pageNumber, int pageSize);
    }
}