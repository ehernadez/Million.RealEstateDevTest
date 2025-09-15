using AutoMapper;
using Million.RealEstate.Application.DTOs;
using Million.RealEstate.Application.UseCases.Properties.Implementations;
using Million.RealEstate.Domain.Entities;
using Million.RealEstate.Domain.Interfaces;
using Moq;
using NUnit.Framework;

namespace Million.RealEstate.Tests.UseCases.Properties
{
    public class CreatePropertyUseCaseTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IMapper> _mapperMock;
        private CreatePropertyUseCase _createPropertyUseCase;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _createPropertyUseCase = new CreatePropertyUseCase(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task ExecuteAsync_ValidProperty_ReturnsPropertyId()
        {
            // Arrange
            var createPropertyDto = new CreatePropertyDto
            {
                Name = "Test Property",
                Address = "123 Test St",
                Price = 100000,
                CodeInternal = "TEST1",
                Year = 2020,
                Bedrooms = 3,
                Bathrooms = 2
            };

            var property = new Property
            {
                IdProperty = 1,
                Name = createPropertyDto.Name,
                Address = createPropertyDto.Address,
                Price = createPropertyDto.Price,
                CodeInternal = createPropertyDto.CodeInternal,
                Year = createPropertyDto.Year
            };

            _mapperMock.Setup(m => m.Map<Property>(createPropertyDto))
                .Returns(property);

            _unitOfWorkMock.Setup(u => u.Properties.AddAsync(It.IsAny<Property>()))
                .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _createPropertyUseCase.ExecuteAsync(createPropertyDto);

            // Assert
            Assert.That(result, Is.EqualTo(1));
            _unitOfWorkMock.Verify(u => u.Properties.AddAsync(It.IsAny<Property>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}