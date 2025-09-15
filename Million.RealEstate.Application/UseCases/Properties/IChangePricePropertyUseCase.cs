namespace Million.RealEstate.Application.UseCases.Properties
{
    public interface IChangePricePropertyUseCase
    {
        Task<bool> ExecuteAsync(int id, decimal newPrice);
    }
}