using AutoMapper;
using Million.RealEstate.Application.DTOs;
using Million.RealEstate.Application.Extensions;
using Million.RealEstate.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Million.RealEstate.Application.UseCases.Properties.Implementations
{
    public class UpdatePropertyUseCase : IUpdatePropertyUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePropertyUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> ExecuteAsync(int id, UpdatePropertyDto dto)
        {
            var property = await _unitOfWork.Properties.GetByIdAsync(id) 
                ?? throw new ValidationException($"Property with id {id} not found");

            if (dto.IdOwner.HasValue)
            {
                var ownerExists = await _unitOfWork.Owners.GetByIdAsync(dto.IdOwner.Value)
                    ?? throw new ValidationException($"Owner with id {dto.IdOwner.Value} not found");
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

            _mapper.Map(dto, property);
            _unitOfWork.Properties.Update(property);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }
    }
}