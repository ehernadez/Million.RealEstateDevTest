using AutoMapper;
using Million.RealEstate.Application.DTOs;
using Million.RealEstate.Domain.Interfaces;

namespace Million.RealEstate.Application.UseCases.Properties.Implementations
{
    public class GetPropertyByIdUseCase : IGetPropertyByIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPropertyByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PropertyDto?> ExecuteAsync(int id)
        {
            var property = await _unitOfWork.Properties.GetByIdAsync(id);
            return _mapper.Map<PropertyDto>(property);
        }
    }
}