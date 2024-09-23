using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Udemy.DotNetWebAPI.Models.DTO.Auth;
using Udemy.DotNetWebAPI.Repositories;

namespace Udemy.DotNetWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this._userManager = userManager;
            this._tokenRepository = tokenRepository;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            try
            {
                var identityUser = new IdentityUser
                {
                    UserName = registerRequestDTO.Username,
                    Email = registerRequestDTO.Username
                };
                var result = await _userManager.CreateAsync(identityUser, registerRequestDTO.Password);
                if (result.Succeeded)
                {
                    if (registerRequestDTO.Roles != null)
                    {
                        foreach (var role in registerRequestDTO.Roles)
                        {
                            try
                            {
                                await _userManager.AddToRoleAsync(identityUser, role);
                            }
                            catch (Exception ex)
                            {
                                return BadRequest(ex.Message);
                            }
                        }
                        return Ok("Register Success");

                    }
                }
                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginRequestDTO.Username);
                if (user != null && await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password))
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var jwttoken = _tokenRepository.CreateJwtToken(user, roles.ToList());
                    var tokenResponse = new LoginResponse
                    {
                        token = jwttoken,
                    };
                    return Ok(tokenResponse);

                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
