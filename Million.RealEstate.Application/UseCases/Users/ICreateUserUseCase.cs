using Million.RealEstate.Application.DTOs;

namespace Million.RealEstate.Application.UseCases.Users
{
    public interface ICreateUserUseCase
    {
        Task<int> ExecuteAsync(CreateUserDto createUserDto);
    }
}
