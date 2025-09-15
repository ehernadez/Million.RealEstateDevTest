using AutoMapper;
using Million.RealEstate.Application.DTOs;
using Million.RealEstate.Domain.Entities;
using Million.RealEstate.Domain.Interfaces;

namespace Million.RealEstate.Application.UseCases.Users.Implementations
{
    public class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUserUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> ExecuteAsync(CreateUserDto createUserDto)
        {
            if (await _unitOfWork.Users.EmailExistsAsync(createUserDto.Email))
            {
                throw new InvalidOperationException("Email already exists");
            }

            var user = _mapper.Map<User>(createUserDto);
            user.PasswordHash = _unitOfWork.PasswordHasher.HashPassword(createUserDto.Password);
            user.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return user.Id;
        }
    }
}