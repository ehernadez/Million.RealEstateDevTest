using Million.RealEstate.Application.DTOs.Auth;

namespace Million.RealEstate.Application.UseCases.Auth
{
    public interface ILoginUseCase
    {
        Task<LoginResponseDto> ExecuteAsync(LoginRequestDto request);
    }
}
