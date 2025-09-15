using Million.RealEstate.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Million.RealEstate.Application.UseCases.PropertyTraces.Implementations
{
    public class DeletePropertyTraceUseCase : IDeletePropertyTraceUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePropertyTraceUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ExecuteAsync(int id)
        {
            var propertyTrace = await _unitOfWork.PropertyTraces.GetByIdAsync(id)
                ?? throw new ValidationException($"PropertyTrace with id {id} not found");

            _unitOfWork.PropertyTraces.Remove(propertyTrace);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0;
        }
    }
}