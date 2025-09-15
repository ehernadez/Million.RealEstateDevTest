using Million.RealEstate.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Million.RealEstate.Application.UseCases.Properties.Implementations
{
    public class ChangePricePropertyUseCase : IChangePricePropertyUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChangePricePropertyUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ExecuteAsync(int id, decimal newPrice)
        {
            if (newPrice <= 0)
                throw new ValidationException("Price must be greater than zero");

            var property = await _unitOfWork.Properties.GetByIdAsync(id)
                ?? throw new ValidationException($"Property with id {id} not found");

            await _unitOfWork.Properties.ChangePriceAsync(id, newPrice);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }
    }
}