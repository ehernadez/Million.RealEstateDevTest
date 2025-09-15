using Million.RealEstate.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Million.RealEstate.Application.UseCases.Owners.Implementations
{
    public class DeleteOwnerUseCase : IDeleteOwnerUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOwnerUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ExecuteAsync(int id)
        {
            var owner = await _unitOfWork.Owners.GetByIdAsync(id)
                ?? throw new ValidationException($"Owner with id {id} not found");

            _unitOfWork.Owners.Remove(owner);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }
    }
}