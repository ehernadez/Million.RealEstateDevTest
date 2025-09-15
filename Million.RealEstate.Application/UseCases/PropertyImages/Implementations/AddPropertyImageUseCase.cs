using AutoMapper;
using Million.RealEstate.Application.DTOs;
using Million.RealEstate.Domain.Entities;
using Million.RealEstate.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Million.RealEstate.Application.UseCases.PropertyImages.Implementations
{
    public class AddPropertyImageUseCase : IAddPropertyImageUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddPropertyImageUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AddPropertyImageResponseDto> ExecuteAsync(int propertyId, AddPropertyImageDto dto)
        {
            var property = await _unitOfWork.Properties.GetByIdAsync(propertyId)
                ?? throw new ValidationException($"Property with id {propertyId} not found");

            if (dto.Length > 5 * 1024 * 1024)
                throw new ValidationException("File size exceeds maximum limit of 5MB");

            try
            {
                var savedFileName = await _unitOfWork.FileStorageService.SaveFileAsync(dto.ImageStream, dto.FileName);
                var fileUrl = _unitOfWork.FileStorageService.GetFileUrl(savedFileName);

                var image = _mapper.Map<PropertyImage>(dto);
                image.IdProperty = propertyId;
                image.File = savedFileName;

                await _unitOfWork.PropertyImages.AddAsync(image);
                await _unitOfWork.SaveChangesAsync();

                var response = _mapper.Map<AddPropertyImageResponseDto>(image);
                response.FileUrl = fileUrl;

                return response;
            }
            catch (Exception ex)
            {
                throw new ValidationException($"Error processing image: {ex.Message}");
            }
        }
    }
}