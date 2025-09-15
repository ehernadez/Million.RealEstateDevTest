using AutoMapper;
using Million.RealEstate.Application.DTOs;
using Million.RealEstate.Application.UseCases.Users.Implementations;
using Million.RealEstate.Domain.Entities;
using Million.RealEstate.Domain.Interfaces;
using Moq;
using NUnit.Framework;

namespace Million.RealEstate.Tests.UseCases.Users
{
    public class CreateUserUseCaseTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IMapper> _mapperMock;
        private CreateUserUseCase _createUserUseCase;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _createUserUseCase = new CreateUserUseCase(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task ExecuteAsync_ValidUser_ReturnsUserId()
        {
            // Arrange
            var createUserDto = new CreateUserDto
            {
                FirstName = "Test",
                LastName = "User",
                Email = "test@test.com",
                Password = "password123"
            };

            var hashedPassword = "hashedPassword123";

            _unitOfWorkMock.Setup(u => u.Users.EmailExistsAsync(createUserDto.Email))
                .ReturnsAsync(false);

            _unitOfWorkMock.Setup(u => u.PasswordHasher.HashPassword(createUserDto.Password))
                .Returns(hashedPassword);

            _unitOfWorkMock.Setup(u => u.Users.AddAsync(It.IsAny<User>()))
                .Callback<User>(user => user.Id = 1)
                .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _createUserUseCase.ExecuteAsync(createUserDto);

            // Assert
            Assert.That(result, Is.EqualTo(1));
            _unitOfWorkMock.Verify(u => u.Users.EmailExistsAsync(createUserDto.Email), Times.Once);
            _unitOfWorkMock.Verify(u => u.PasswordHasher.HashPassword(createUserDto.Password), Times.Once);
            _unitOfWorkMock.Verify(u => u.Users.AddAsync(It.Is<User>(u => 
                u.Email == createUserDto.Email && 
                u.PasswordHash == hashedPassword &&
                u.FirstName == createUserDto.FirstName &&
                u.LastName == createUserDto.LastName)), 
                Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void ExecuteAsync_ExistingEmail_ThrowsInvalidOperationException()
        {
            // Arrange
            var createUserDto = new CreateUserDto
            {
                FirstName = "Test",
                LastName = "User",
                Email = "existing@test.com",
                Password = "password123"
            };

            _unitOfWorkMock.Setup(u => u.Users.EmailExistsAsync(createUserDto.Email))
                .ReturnsAsync(true);

            // Act & Assert
            var ex = Assert.ThrowsAsync<InvalidOperationException>(
                () => _createUserUseCase.ExecuteAsync(createUserDto));
            
            Assert.That(ex.Message, Is.EqualTo("Email already exists"));
            
            _unitOfWorkMock.Verify(u => u.Users.EmailExistsAsync(createUserDto.Email), Times.Once);
            _unitOfWorkMock.Verify(u => u.PasswordHasher.HashPassword(It.IsAny<string>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.Users.AddAsync(It.IsAny<User>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }
    }
}