namespace Million.RealEstate.Application.UseCases.Owners
{
    public interface IDeleteOwnerUseCase
    {
        Task<bool> ExecuteAsync(int id);
    }
}