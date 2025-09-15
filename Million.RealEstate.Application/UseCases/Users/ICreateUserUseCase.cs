using Million.RealEstate.Application.DTOs.Users;

namespace Million.RealEstate.Application.UseCases.Users
{
    public interface ICreateUserUseCase
    {
        Task<int> ExecuteAsync(CreateUserDto createUserDto);
    }
}
