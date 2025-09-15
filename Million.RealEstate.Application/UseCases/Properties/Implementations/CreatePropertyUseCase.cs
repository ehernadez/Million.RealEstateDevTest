using AutoMapper;
using Million.RealEstate.Application.DTOs;
using Million.RealEstate.Application.Extensions;
using Million.RealEstate.Domain.Entities;
using Million.RealEstate.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Million.RealEstate.Application.UseCases.Properties.Implementations
{
    public class CreatePropertyUseCase : ICreatePropertyUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreatePropertyUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> ExecuteAsync(CreatePropertyDto dto)
        {
            if (dto.IdOwner.HasValue)
            {
                var ownerExists = await _unitOfWork.Owners.GetByIdAsync(dto.IdOwner.Value) 
                    ?? throw new ValidationException($"The OwnerId {dto.IdOwner.Value} not found.");
            }

            dto.Name.ValidateValue(nameof(dto.Name));
            dto.Address.ValidateValue(nameof(dto.Address));
            dto.Price.ValidateValue(nameof(dto.Price));
            dto.CodeInternal.ValidateValue(nameof(dto.CodeInternal));
            dto.Year.ValidateValue(nameof(dto.Year));
            dto.Bedrooms.ValidateValue(nameof(dto.Bedrooms));
            dto.Bathrooms.ValidateValue(nameof(dto.Bathrooms));

            if (dto.Year < 1800 || dto.Year > DateTime.UtcNow.Year + 1)
                throw new ValidationException("The Year field must be a valid 4-digit year (YYYY) and not greater than the current year");

            var property = _mapper.Map<Property>(dto);
            await _unitOfWork.Properties.AddAsync(property);
            await _unitOfWork.SaveChangesAsync();

            return property.IdProperty;
        }
    }
}
