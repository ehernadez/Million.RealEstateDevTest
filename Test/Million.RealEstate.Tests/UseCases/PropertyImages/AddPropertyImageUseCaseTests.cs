using Million.RealEstate.Application.DTOs;
using Million.RealEstate.Application.UseCases.PropertyImages.Implementations;
using Million.RealEstate.Domain.Entities;
using Million.RealEstate.Domain.Interfaces;
using Moq;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace Million.RealEstate.Tests.UseCases.PropertyImages
{
    public class AddPropertyImageUseCaseTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private AddPropertyImageUseCase _addPropertyImageUseCase;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _addPropertyImageUseCase = new AddPropertyImageUseCase(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task ExecuteAsync_ValidImage_ReturnsPropertyImageResponse()
        {
            // Arrange
            var propertyId = 1;
            var fileName = "test.jpg";
            var contentType = "image/jpeg";
            var fileLength = 1024L; // 1KB
            var memoryStream = new MemoryStream(new byte[fileLength]);
            var fileUrl = "http://localhost/images/properties/test.jpg";

            var property = new Property { IdProperty = propertyId };
            var savedFilePath = "images/properties/test.jpg";
            var propertyImage = new PropertyImage
            {
                IdPropertyImage = 1,
                IdProperty = propertyId,
                File = savedFilePath,
                Enabled = true
            };

            var addPropertyImageDto = new AddPropertyImageDto 
            { 
                ImageStream = memoryStream,
                FileName = fileName,
                ContentType = contentType,
                Length = fileLength,
                Enabled = true
            };

            _unitOfWorkMock.Setup(u => u.Properties.GetByIdAsync(propertyId))
                .ReturnsAsync(property);

            _unitOfWorkMock.Setup(u => u.FileStorageService.SaveFileAsync(It.IsAny<Stream>(), It.IsAny<string>()))
                .ReturnsAsync(savedFilePath);

            _unitOfWorkMock.Setup(u => u.FileStorageService.GetFileUrl(savedFilePath))
                .Returns(fileUrl);

            _unitOfWorkMock.Setup(u => u.PropertyImages.AddAsync(It.IsAny<PropertyImage>()))
                .Callback<PropertyImage>(pi => 
                {
                    pi.IdPropertyImage = propertyImage.IdPropertyImage;
                    pi.File = savedFilePath;
                })
                .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _addPropertyImageUseCase.ExecuteAsync(propertyId, addPropertyImageDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.IdPropertyImage, Is.EqualTo(propertyImage.IdPropertyImage));
                Assert.That(result.FileUrl, Is.EqualTo(fileUrl));
            });
            _unitOfWorkMock.Verify(u => u.Properties.GetByIdAsync(propertyId), Times.Once);
            _unitOfWorkMock.Verify(u => u.FileStorageService.SaveFileAsync(It.IsAny<Stream>(), It.IsAny<string>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.FileStorageService.GetFileUrl(savedFilePath), Times.Once);
            _unitOfWorkMock.Verify(u => u.PropertyImages.AddAsync(It.IsAny<PropertyImage>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void ExecuteAsync_InvalidPropertyId_ThrowsValidationException()
        {
            // Arrange
            var propertyId = 999;
            var memoryStream = new MemoryStream(new byte[1024]);
            var addPropertyImageDto = new AddPropertyImageDto 
            { 
                ImageStream = memoryStream,
                FileName = "test.jpg",
                ContentType = "image/jpeg",
                Length = 1024,
                Enabled = true
            };

            _unitOfWorkMock.Setup(u => u.Properties.GetByIdAsync(propertyId))
                .ReturnsAsync((Property?)null);

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(() => 
                _addPropertyImageUseCase.ExecuteAsync(propertyId, addPropertyImageDto));
            
            _unitOfWorkMock.Verify(u => u.Properties.GetByIdAsync(propertyId), Times.Once);
            _unitOfWorkMock.Verify(u => u.FileStorageService.SaveFileAsync(It.IsAny<Stream>(), It.IsAny<string>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.FileStorageService.GetFileUrl(It.IsAny<string>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.PropertyImages.AddAsync(It.IsAny<PropertyImage>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Test]
        public void ExecuteAsync_FileTooLarge_ThrowsValidationException()
        {
            // Arrange
            var propertyId = 1;
            var fileLength = 6L * 1024 * 1024;
            var memoryStream = new MemoryStream(new byte[1024]);
            var addPropertyImageDto = new AddPropertyImageDto 
            { 
                ImageStream = memoryStream,
                FileName = "test.jpg",
                ContentType = "image/jpeg",
                Length = fileLength,
                Enabled = true
            };

            var property = new Property { IdProperty = propertyId };

            _unitOfWorkMock.Setup(u => u.Properties.GetByIdAsync(propertyId))
                .ReturnsAsync(property);

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(() => 
                _addPropertyImageUseCase.ExecuteAsync(propertyId, addPropertyImageDto));
            
            _unitOfWorkMock.Verify(u => u.Properties.GetByIdAsync(propertyId), Times.Once);
            _unitOfWorkMock.Verify(u => u.FileStorageService.SaveFileAsync(It.IsAny<Stream>(), It.IsAny<string>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.FileStorageService.GetFileUrl(It.IsAny<string>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.PropertyImages.AddAsync(It.IsAny<PropertyImage>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }
    }
}