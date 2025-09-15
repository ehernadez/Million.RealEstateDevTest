using Million.RealEstate.Domain.Models.Auth;

namespace Million.RealEstate.Domain.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> AuthenticateAsync(string email, string password);
        string GenerateJwtToken(string email);
    }
}