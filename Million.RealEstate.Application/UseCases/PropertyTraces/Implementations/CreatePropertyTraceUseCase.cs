using AutoMapper;
using Million.RealEstate.Application.DTOs;
using Million.RealEstate.Application.Extensions;
using Million.RealEstate.Domain.Entities;
using Million.RealEstate.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Million.RealEstate.Application.UseCases.PropertyTraces.Implementations
{
    public class CreatePropertyTraceUseCase : ICreatePropertyTraceUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePropertyTraceUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> ExecuteAsync(int propertyId, CreatePropertyTraceDto dto)
        {
            var property = await _unitOfWork.Properties.GetByIdAsync(propertyId)
                ?? throw new ValidationException($"Property with id {propertyId} not found");

            dto.Name.ValidateValue(nameof(dto.Name));
            dto.Value.ValidateValue(nameof(dto.Value));
            dto.Tax.ValidateValue(nameof(dto.Tax));
            dto.DateSale.ValidateValue(nameof(dto.DateSale));

            var propertyTrace = _mapper.Map<PropertyTrace>(dto);
            propertyTrace.IdProperty = propertyId;

            await _unitOfWork.PropertyTraces.AddAsync(propertyTrace);
            await _unitOfWork.SaveChangesAsync();

            return propertyTrace.IdPropertyTrace;
        }
    }
}