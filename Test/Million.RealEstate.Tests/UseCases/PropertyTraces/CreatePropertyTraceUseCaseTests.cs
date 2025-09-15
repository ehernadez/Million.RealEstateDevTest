using AutoMapper;
using Million.RealEstate.Application.DTOs;
using Million.RealEstate.Application.UseCases.PropertyTraces.Implementations;
using Million.RealEstate.Domain.Entities;
using Million.RealEstate.Domain.Interfaces;
using Moq;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace Million.RealEstate.Tests.UseCases.PropertyTraces
{
    public class CreatePropertyTraceUseCaseTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IMapper> _mapperMock;
        private CreatePropertyTraceUseCase _createPropertyTraceUseCase;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _createPropertyTraceUseCase = new CreatePropertyTraceUseCase(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task ExecuteAsync_ValidTrace_ReturnsTraceId()
        {
            // Arrange
            var propertyId = 1;
            var createPropertyTraceDto = new CreatePropertyTraceDto
            {
                Name = "Test",
                Value = 100000,
                Tax = 5000,
                DateSale = DateTime.UtcNow
            };

            var property = new Property { IdProperty = propertyId };
            var propertyTrace = new PropertyTrace
            {
                IdPropertyTrace = 1,
                IdProperty = propertyId,
                Name = createPropertyTraceDto.Name,
                Value = createPropertyTraceDto.Value,
                Tax = createPropertyTraceDto.Tax,
                DateSale = createPropertyTraceDto.DateSale
            };

            _unitOfWorkMock.Setup(u => u.Properties.GetByIdAsync(propertyId))
                .ReturnsAsync(property);

            _mapperMock.Setup(m => m.Map<PropertyTrace>(createPropertyTraceDto))
                .Returns(propertyTrace);

            _unitOfWorkMock.Setup(u => u.PropertyTraces.AddAsync(It.IsAny<PropertyTrace>()))
                .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _createPropertyTraceUseCase.ExecuteAsync(propertyId, createPropertyTraceDto);

            // Assert
            Assert.That(result, Is.EqualTo(1));
            _unitOfWorkMock.Verify(u => u.Properties.GetByIdAsync(propertyId), Times.Once);
            _unitOfWorkMock.Verify(u => u.PropertyTraces.AddAsync(It.IsAny<PropertyTrace>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void ExecuteAsync_InvalidPropertyId_ThrowsValidationException()
        {
            // Arrange
            var propertyId = 999;
            var createPropertyTraceDto = new CreatePropertyTraceDto
            {
                Name = "Test",
                Value = 100000,
                Tax = 5000,
                DateSale = DateTime.UtcNow
            };

            _unitOfWorkMock.Setup(u => u.Properties.GetByIdAsync(propertyId))
                .ReturnsAsync((Property?)null);

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(() => 
                _createPropertyTraceUseCase.ExecuteAsync(propertyId, createPropertyTraceDto));
            
            _unitOfWorkMock.Verify(u => u.Properties.GetByIdAsync(propertyId), Times.Once);
            _unitOfWorkMock.Verify(u => u.PropertyTraces.AddAsync(It.IsAny<PropertyTrace>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Test]
        public void ExecuteAsync_InvalidData_ThrowsValidationException()
        {
            // Arrange
            var propertyId = 1;
            var createPropertyTraceDto = new CreatePropertyTraceDto
            {
                Name = string.Empty,
                Value = -100,
                Tax = 5000,
                DateSale = DateTime.Now
            };

            var property = new Property { IdProperty = propertyId };

            _unitOfWorkMock.Setup(u => u.Properties.GetByIdAsync(propertyId))
                .ReturnsAsync(property);

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(() => 
                _createPropertyTraceUseCase.ExecuteAsync(propertyId, createPropertyTraceDto));
            
            _unitOfWorkMock.Verify(u => u.Properties.GetByIdAsync(propertyId), Times.Once);
            _unitOfWorkMock.Verify(u => u.PropertyTraces.AddAsync(It.IsAny<PropertyTrace>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }
    }
}