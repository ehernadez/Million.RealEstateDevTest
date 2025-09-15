using AutoMapper;
using Million.RealEstate.Application.DTOs;
using Million.RealEstate.Application.Extensions;
using Million.RealEstate.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Million.RealEstate.Application.UseCases.PropertyTraces.Implementations
{
    public class UpdatePropertyTraceUseCase : IUpdatePropertyTraceUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePropertyTraceUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> ExecuteAsync(int id, UpdatePropertyTraceDto dto)
        {
            var propertyTrace = await _unitOfWork.PropertyTraces.GetByIdAsync(id)
                ?? throw new ValidationException($"PropertyTrace with id {id} not found");

            dto.Name.ValidateValue(nameof(dto.Name));
            dto.Value.ValidateValue(nameof(dto.Value));
            dto.Tax.ValidateValue(nameof(dto.Tax));
            dto.DateSale.ValidateValue(nameof(dto.DateSale));

            _mapper.Map(dto, propertyTrace);
            _unitOfWork.PropertyTraces.Update(propertyTrace);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0;
        }
    }
}