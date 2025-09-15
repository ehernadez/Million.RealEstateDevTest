namespace Million.RealEstate.Application.UseCases.Properties
{
    public interface IDeletePropertyUseCase
    {
        Task<bool> ExecuteAsync(int id);
    }
}