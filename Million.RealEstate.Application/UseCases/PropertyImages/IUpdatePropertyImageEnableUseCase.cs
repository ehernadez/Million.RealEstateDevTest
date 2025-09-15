namespace Million.RealEstate.Application.UseCases.PropertyImages
{
    public interface IUpdatePropertyImageEnableUseCase
    {
        Task<bool> ExecuteAsync(int id, bool enabled);
    }
}