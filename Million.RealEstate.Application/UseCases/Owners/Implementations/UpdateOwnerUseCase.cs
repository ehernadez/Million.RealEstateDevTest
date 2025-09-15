using AutoMapper;
using Million.RealEstate.Application.DTOs;
using Million.RealEstate.Application.Extensions;
using Million.RealEstate.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Million.RealEstate.Application.UseCases.Owners.Implementations
{
    public class UpdateOwnerUseCase : IUpdateOwnerUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateOwnerUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> ExecuteAsync(int id, UpdateOwnerDto dto)
        {
            var owner = await _unitOfWork.Owners.GetByIdAsync(id)
                ?? throw new ValidationException($"Owner with id {id} not found");

            dto.Name.ValidateValue(nameof(dto.Name));

            _mapper.Map(dto, owner);
            _unitOfWork.Owners.Update(owner);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }
    }
}