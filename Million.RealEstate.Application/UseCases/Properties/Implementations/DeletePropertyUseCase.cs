using Million.RealEstate.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Million.RealEstate.Application.UseCases.Properties.Implementations
{
    public class DeletePropertyUseCase : IDeletePropertyUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePropertyUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ExecuteAsync(int id)
        {
            var property = await _unitOfWork.Properties.GetByIdAsync(id)
                ?? throw new ValidationException($"Property with id {id} not found");

            _unitOfWork.Properties.Remove(property);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }
    }
}