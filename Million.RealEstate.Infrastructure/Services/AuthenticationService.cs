using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Million.RealEstate.Domain.Interfaces;
using Million.RealEstate.Domain.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Million.RealEstate.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticationService(
            IConfiguration configuration,
            IUserRepository userRepository,
            IPasswordHasher passwordHasher)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<LoginResponse> AuthenticateAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null || !_passwordHasher.VerifyPassword(password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            var token = GenerateJwtToken(user.Email);
            var expirationTime = DateTime.UtcNow.AddHours(1);

            return new LoginResponse
            {
                Token = token,
                Email = email,
                ExpiresAt = expirationTime
            };
        }

        public string GenerateJwtToken(string email)
        {
            var secretKey = _configuration["JwtSettings:SecretKey"];
            var key = Encoding.ASCII.GetBytes(secretKey ?? throw new InvalidOperationException("JWT secret key not configured"));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}