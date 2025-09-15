using Million.RealEstate.Application.DTOs.Auth;
using Million.RealEstate.Application.UseCases.Auth.Implementations;
using Million.RealEstate.Domain.Interfaces;
using Million.RealEstate.Domain.Models.Auth;
using Moq;
using NUnit.Framework;

namespace Million.RealEstate.Tests.UseCases.Auth
{
    public class LoginUseCaseTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private LoginUseCase _loginUseCase;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _loginUseCase = new LoginUseCase(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task ExecuteAsync_ValidCredentials_ReturnsLoginResponse()
        {
            // Arrange
            var loginRequest = new LoginRequestDto
            {
                Email = "test@test.com",
                Password = "password123"
            };

            var loginResponse = new LoginResponse
            {
                Token = "jwt_token",
                Email = loginRequest.Email,
                ExpiresAt = DateTime.UtcNow.AddHours(1)
            };

            _unitOfWorkMock.Setup(u => u.AuthenticationService.AuthenticateAsync(
                loginRequest.Email, loginRequest.Password))
                .ReturnsAsync(loginResponse);

            // Act
            var result = await _loginUseCase.ExecuteAsync(loginRequest);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Token, Is.EqualTo(loginResponse.Token));
                Assert.That(result.Email, Is.EqualTo(loginResponse.Email));
                Assert.That(result.ExpiresAt, Is.EqualTo(loginResponse.ExpiresAt));
            });
            _unitOfWorkMock.Verify(u => u.AuthenticationService.AuthenticateAsync(
                loginRequest.Email, loginRequest.Password), Times.Once);
        }

        [Test]
        public void ExecuteAsync_InvalidCredentials_ThrowsException()
        {
            // Arrange
            var loginRequest = new LoginRequestDto
            {
                Email = "test@test.com",
                Password = "password"
            };

            _unitOfWorkMock.Setup(u => u.AuthenticationService.AuthenticateAsync(
                loginRequest.Email, loginRequest.Password))
                .ThrowsAsync(new UnauthorizedAccessException("Invalid credentials"));

            // Act & Assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(() => 
                _loginUseCase.ExecuteAsync(loginRequest));

            _unitOfWorkMock.Verify(u => u.AuthenticationService.AuthenticateAsync(
                loginRequest.Email, loginRequest.Password), Times.Once);
        }
    }
}