using Million.RealEstate.Application.DTOs;
using Million.RealEstate.Domain.Entities;
using Million.RealEstate.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Million.RealEstate.Application.UseCases.PropertyImages.Implementations
{
    public class AddPropertyImageUseCase : IAddPropertyImageUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddPropertyImageUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

                var image = new PropertyImage
                {
                    IdProperty = propertyId,
                    File = savedFileName,
                    Enabled = dto.Enabled
                };

                await _unitOfWork.PropertyImages.AddAsync(image);
                await _unitOfWork.SaveChangesAsync();

                // Mapear respuesta
                return new AddPropertyImageResponseDto
                {
                    IdPropertyImage = image.IdPropertyImage,
                    FileUrl = fileUrl
                };
            }
            catch (Exception ex)
            {
                throw new ValidationException($"Error processing image: {ex.Message}");
            }
        }
    }
}