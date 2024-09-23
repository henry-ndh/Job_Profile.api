using AutoMapper;
using Udemy.DotNetWebAPI.Models.Domain;
using Udemy.DotNetWebAPI.Models.DTO;
using Udemy.DotNetWebAPI.Repositories.Interfaces;
using Udemy.DotNetWebAPI.Services.Interfaces;

namespace Udemy.DotNetWebAPI.Services.Implements
{
    public class UserBizLogic : IUserBizLogic
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper _mapper;


        public UserBizLogic(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this._mapper = mapper;
        }

        public async Task<bool> CreateUpdateUser(CreateUserDTO userDTO)
        {
            return await userRepository.CreateUser(userDTO);
        }

        public async Task<UserDTO> GetUserById(long id)
        {
            var data = await userRepository.GetUserById(id);
            var usersDTO = _mapper.Map<UserDTO>(data);
            return usersDTO;

        }
    }
}
