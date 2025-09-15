using Million.RealEstate.Application.DTOs.Auth;
using Million.RealEstate.Domain.Interfaces;

namespace Million.RealEstate.Application.UseCases.Auth.Implementations
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LoginResponseDto> ExecuteAsync(LoginRequestDto request)
        {
            var result = await _unitOfWork.AuthenticationService.AuthenticateAsync(request.Email, request.Password);
            
            return new LoginResponseDto
            {
                Token = result.Token,
                Email = result.Email,
                ExpiresAt = result.ExpiresAt
            };
        }
    }
}