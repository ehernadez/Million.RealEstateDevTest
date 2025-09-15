using Million.RealEstate.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Million.RealEstate.Application.UseCases.PropertyImages.Implementations
{
    public class DeletePropertyImageUseCase : IDeletePropertyImageUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorage;

        public DeletePropertyImageUseCase(IUnitOfWork unitOfWork, IFileStorageService fileStorage)
        {
            _unitOfWork = unitOfWork;
            _fileStorage = fileStorage;
        }

        public async Task<bool> ExecuteAsync(int id)
        {
            var image = await _unitOfWork.PropertyImages.GetByIdAsync(id)
                ?? throw new ValidationException($"PropertyImage with id {id} not found");

            // Eliminar el archivo físico
            var fileName = Path.GetFileName(image.File);
            await _fileStorage.DeleteFileAsync(fileName);

            // Eliminar el registro de la base de datos
            _unitOfWork.PropertyImages.Remove(image);
            var result = await _unitOfWork.SaveChangesAsync();
            
            return result > 0;
        }
    }
}