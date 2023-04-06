using ExcelAtUni.Data;
using ExcelAtUni.Dtos;
using ExcelAtUni.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExcelAtUni.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly DataContext _dataContext = new DataContext(new ConfigurationBuilder());

        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            try
            {
                userRegisterDto.Username.ToLower();

                if (await _repo.UserExists(userRegisterDto.Username))
                {
                    return BadRequest("User already exists");
                }

                var userCreated = new User()
                {
                    Email = userRegisterDto.Username
                };

                await _repo.Register(userCreated, userRegisterDto.Password);

                return Ok(userCreated);
            }
            catch (Exception e)
            {
                throw new Exception("User not successfully registered" + e);
            }

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            try
            {
                var userFromRepo = await _repo.Login(userLoginDto.Email, userLoginDto.Password);

                if (userFromRepo == null)
                    return Unauthorized();

                var claims = new[]
                {
                    new Claim(ClaimTypes.Email, userFromRepo.Id.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_dataContext.GetToken()));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new
                {
                    token = tokenHandler.WriteToken(token)
                });
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
