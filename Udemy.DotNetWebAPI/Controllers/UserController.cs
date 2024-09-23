using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Udemy.DotNetWebAPI.Models.DTO;
using Udemy.DotNetWebAPI.Repositories;
using Udemy.DotNetWebAPI.Repositories.Interfaces;
using Udemy.DotNetWebAPI.Services.Interfaces;

namespace Udemy.DotNetWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserBizLogic _userBizLogic;
        public UserController(IUserBizLogic userBizLogic, IMapper mapper, ILogger<UserController> logger)
        {
            this._userBizLogic = userBizLogic;
            this._logger = logger;
        }
        //[HttpGet("get-all-user")]
        //public async Task<IActionResult> GetAll()
        //{
        //    _logger.LogInformation("Getting all users");
        //    var users = await userRepository.GetAll();
        //    return Ok(usersDTO);
        //}

        [HttpPost("create-update-user")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO dto)
        {
            try
            {
                var result = await _userBizLogic.CreateUpdateUser(dto);
                if (result)
                {
                    return Ok("Tạo thành công");
                }
                return BadRequest("Tạo thất bại");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-user-by-id/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userBizLogic.GetUserById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}
