using Microsoft.EntityFrameworkCore;
using Udemy.DotNetWebAPI.Data;
using Udemy.DotNetWebAPI.Models.Domain;
using Udemy.DotNetWebAPI.Models.DTO;
using Udemy.DotNetWebAPI.Repositories.Interfaces;

namespace Udemy.DotNetWebAPI.Repositories.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly DotNetDbContext _dbContext;

        public UserRepository(DotNetDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateUser(CreateUserDTO userDTO)
        {
            var any = await _dbContext.User.AnyAsync(x => x.Id == userDTO.Id);
            if (any)
            {
                var user = await _dbContext.User.FirstOrDefaultAsync(x => x.Id == userDTO.Id);
                user.FirstName = userDTO.FirstName;
                user.MiddleName = userDTO.MiddleName;
                user.Mobile = userDTO.Mobile;
                user.LastName = userDTO.LastName;
                user.Email = userDTO.Email;
                user.PasswordHash = userDTO.PasswordHash;
                user.Intro = userDTO.Intro;
                user.Profile = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/User_icon_2.svg/640px-User_icon_2.svg.png";
                _dbContext.User.Update(user);
            }
            else
            {
                var user = new User
                {
                    FirstName = userDTO.FirstName,
                    MiddleName = userDTO.MiddleName,
                    Mobile = userDTO.Mobile,
                    LastName = userDTO.LastName,
                    Email = userDTO.Email,
                    PasswordHash = userDTO.PasswordHash,
                    RegisterAt = DateTime.Now,
                    LastLogin = DateTime.Now,
                    Intro = userDTO.Intro,
                    Profile = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/User_icon_2.svg/640px-User_icon_2.svg.png"
                };
                await _dbContext.User.AddAsync(user);
            }
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<User>> GetAll()
        {
            return await _dbContext.User.ToListAsync();
        }

        public async Task<User> GetUserById(long id)
        {
            return await _dbContext.User.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
