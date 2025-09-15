namespace Million.RealEstate.Application.UseCases.PropertyImages
{
    public interface IDeletePropertyImageUseCase
    {
        Task<bool> ExecuteAsync(int id);
    }
}