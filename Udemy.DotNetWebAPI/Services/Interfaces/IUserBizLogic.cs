using Udemy.DotNetWebAPI.Models.DTO;

namespace Udemy.DotNetWebAPI.Services.Interfaces
{
    public interface IUserBizLogic
    {
        Task<bool> CreateUpdateUser(CreateUserDTO userDTO);
        Task<UserDTO> GetUserById(long id);
    }
}
