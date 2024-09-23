using Udemy.DotNetWebAPI.Models.Domain;
using Udemy.DotNetWebAPI.Models.DTO;

namespace Udemy.DotNetWebAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<bool> CreateUser(CreateUserDTO createUserDto);
        Task<User> GetUserById(long id);

    }
}
