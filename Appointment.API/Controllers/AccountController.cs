using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VUPayRoll.Service;
using VUPayRoll.ViewModel.Account;

namespace VUPayRoll.API.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly AccountService _service;
        public AccountController(AccountService service, IConfiguration config)
        {
            _service = service;
            _config = config;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpViewModel model)
        {
            model.Username = model.Username.ToLower();

            if (await _service.UserExist(model.Username))
                return BadRequest("User already exist");
            await _service.SignUp(model);
            return Ok("Successfully registered");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var userRepo = await _service.Login(model.Username.ToLower(), model.Password.ToLower());

            if (userRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userRepo.ID.ToString()),
                new Claim(ClaimTypes.Name, userRepo.Username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config
                     .GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var usertoken = tokenHandler.WriteToken(token);
            return Ok(new { userRepo, usertoken });

        }

        [HttpPost("Userexist")]
        public async Task<IActionResult> UserExist([FromBody] string username)
        {
            if (await _service.UserExist(username))
                return BadRequest("Username is already exist");

            return Ok();
        }
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _service.GetAll();
            return Ok(users);
        }

        [HttpGet("Approved/{id}")]
        public async Task<IActionResult> Approved(int id)
        {
            var user = await _service.Approved(id);
            return Ok(user);
        }

        //Custom
        [HttpGet("GetAlls")]
        public async Task<IActionResult> GetAllsAsync()
        {
            var users = await _service.GetAlls();
            return Ok(users);
        }
    }
}
