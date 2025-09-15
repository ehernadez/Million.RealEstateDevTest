using AutoMapper;
using Million.RealEstate.Application.DTOs;
using Million.RealEstate.Application.Helpers;
using Million.RealEstate.Domain.Interfaces;

namespace Million.RealEstate.Application.UseCases.Properties.Implementations
{
    public class GetAllPropertiesUseCase : IGetAllPropertiesUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllPropertiesUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PropertyPagedResponseDto> ExecuteAsync(PropertyFilterDto? filter, int pageNumber, int pageSize)
        {
            var propertyFilter = PropertyFilterHelper.BuildPropertyFilter(filter);
            var result = await _unitOfWork.Properties.GetAllPaginateWithDetailsAsync(propertyFilter, pageNumber, pageSize);
            
            var response = _mapper.Map<PropertyPagedResponseDto>(result);
            response.PageNumber = pageNumber;
            response.PageSize = pageSize;

            return response;
        }
    }
}