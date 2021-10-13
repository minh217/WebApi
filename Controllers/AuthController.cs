using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;
using WebApi.Dtos;
using WebApi.Models;
using WebApi.Helpers;
namespace WebApi.Controllers 
{
    [Route("api")]
    [ApiController]
    public class AuthController: Controller
    {
        private readonly IUserRepository _repository;
        private readonly JwtService _jwtService;
        public AuthController(IUserRepository repository,JwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {

            User user = new User(){
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };
            return Created("Success", _repository.Create(user));
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto){
            var user = _repository.GetUserByEmail(dto.Email);
            if(user == null || !BCrypt.Net.BCrypt.Verify(dto.Password,user.Password))
            {
                return BadRequest("Invalid Credential"); 
            }

            var jwt = _jwtService.Generate(user.Id);

            Response.Cookies.Append("jwt",jwt,new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new {
                message = "Success"
            });
        }

        [HttpGet("user")]
        public IActionResult User()
        {
            try{    
                var jwt =  Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);
                int userId = int.Parse(token.Issuer);
                var user = _repository.GetUserById(userId);

                return Ok(user);
            }catch(Exception _){
                return Unauthorized();
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new {
                message  = "Success"
            });
        }
    }
}