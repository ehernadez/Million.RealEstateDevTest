using AutoMapper;
using Million.RealEstate.Application.DTOs;
using Million.RealEstate.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Million.RealEstate.Application.UseCases.PropertyTraces.Implementations
{
    public class GetPropertyTracesByPropertyUseCase : IGetPropertyTracesByPropertyUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPropertyTracesByPropertyUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PropertyTraceDto>> ExecuteAsync(int propertyId)
        {
            var property = await _unitOfWork.Properties.GetByIdAsync(propertyId)
                ?? throw new ValidationException($"Property with id {propertyId} not found");

            var traces = await _unitOfWork.PropertyTraces.GetByPropertyIdAsync(propertyId);
            return _mapper.Map<IEnumerable<PropertyTraceDto>>(traces);
        }
    }
}