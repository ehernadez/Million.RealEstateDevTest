namespace Million.RealEstate.Application.UseCases.PropertyTraces
{
    public interface IDeletePropertyTraceUseCase
    {
        Task<bool> ExecuteAsync(int id);
    }
}