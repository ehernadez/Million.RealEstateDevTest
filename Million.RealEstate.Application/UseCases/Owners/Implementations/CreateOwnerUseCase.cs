using AutoMapper;
using Million.RealEstate.Application.DTOs;
using Million.RealEstate.Application.Extensions;
using Million.RealEstate.Domain.Entities;
using Million.RealEstate.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Million.RealEstate.Application.UseCases.Owners.Implementations
{
    public class CreateOwnerUseCase : ICreateOwnerUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOwnerUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> ExecuteAsync(CreateOwnerDto dto)
        {
            dto.Name.ValidateValue(nameof(dto.Name));

            var owner = _mapper.Map<Owner>(dto);
            await _unitOfWork.Owners.AddAsync(owner);
            await _unitOfWork.SaveChangesAsync();

            return owner.IdOwner;
        }
    }
}