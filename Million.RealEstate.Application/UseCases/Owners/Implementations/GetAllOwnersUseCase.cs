using AutoMapper;
using Million.RealEstate.Application.DTOs;
using Million.RealEstate.Domain.Interfaces;

namespace Million.RealEstate.Application.UseCases.Owners.Implementations
{
    public class GetAllOwnersUseCase : IGetAllOwnersUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllOwnersUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OwnerPagedResponseDto> ExecuteAsync(int pageNumber, int pageSize)
        {
            var result = await _unitOfWork.Owners.GetAllPaginateAsync(null, pageNumber, pageSize);
            var response = _mapper.Map<OwnerPagedResponseDto>(result);
            response.PageNumber = pageNumber;
            response.PageSize = pageSize;

            return response;
        }
    }
}