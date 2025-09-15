using AutoMapper;
using Million.RealEstate.Application.DTOs;
using Million.RealEstate.Domain.Interfaces;

namespace Million.RealEstate.Application.UseCases.Owners.Implementations
{
    public class GetOwnerByIdUseCase : IGetOwnerByIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOwnerByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OwnerDto?> ExecuteAsync(int id)
        {
            var owner = await _unitOfWork.Owners.GetByIdAsync(id);
            return owner != null ? _mapper.Map<OwnerDto>(owner) : null;
        }
    }
}