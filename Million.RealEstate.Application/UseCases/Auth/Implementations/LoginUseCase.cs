using AutoMapper;
using Million.RealEstate.Application.DTOs;
using Million.RealEstate.Domain.Interfaces;

namespace Million.RealEstate.Application.UseCases.Auth.Implementations
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LoginUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LoginResponseDto> ExecuteAsync(LoginRequestDto request)
        {
            var result = await _unitOfWork.AuthenticationService.AuthenticateAsync(request.Email, request.Password);
            return _mapper.Map<LoginResponseDto>(result);
        }
    }
}