using AutoMapper;
using Million.RealEstate.Application.DTOs;
using Million.RealEstate.Domain.Interfaces;

namespace Million.RealEstate.Application.UseCases.PropertyTraces.Implementations
{
    public class GetPropertyTraceByIdUseCase : IGetPropertyTraceByIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPropertyTraceByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PropertyTraceDto?> ExecuteAsync(int id)
        {
            var propertyTrace = await _unitOfWork.PropertyTraces.GetByIdAsync(id);
            return _mapper.Map<PropertyTraceDto>(propertyTrace);
        }
    }
}