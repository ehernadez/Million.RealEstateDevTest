using Million.RealEstate.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Million.RealEstate.Application.UseCases.PropertyImages.Implementations
{
    public class UpdatePropertyImageEnableUseCase : IUpdatePropertyImageEnableUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePropertyImageEnableUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ExecuteAsync(int id, bool enabled)
        {
            var image = await _unitOfWork.PropertyImages.GetByIdAsync(id)
                ?? throw new ValidationException($"PropertyImage with id {id} not found");

            image.Enabled = enabled;
            _unitOfWork.PropertyImages.Update(image);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0;
        }
    }
}