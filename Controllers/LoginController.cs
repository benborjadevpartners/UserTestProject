using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using UserTestProject.Models;
using UserTestProject.Params;
using UserTestProject.Services;
using System.Text;

namespace UserTestProject.Controllers
{    
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private static string _AdminUsername = "admin";
        private static string _AdminPassword = "ilovedavao";
        private IConfiguration _config;
        private readonly ILogger<UsersController> _logger;
        private readonly IUserAppService _userAppService;

        public LoginController(ILogger<UsersController> logger,
            IUserAppService userAppService,
            IConfiguration config)
        {
            _logger = logger;
            _userAppService = userAppService;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]        
        public async Task<IActionResult> Login([FromBody] LoginParam loginParam)
        {
            User user;
            IActionResult response = Unauthorized();

            if (loginParam.Username.Equals(_AdminUsername) &&
                loginParam.Password.Equals(_AdminPassword))
            {
                user = new User
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                };
            }
            else
            {
                user = await _userAppService.ValidateLogin(loginParam.Username, loginParam.Password);
            }
            

            if ( user != null )
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }
            
            return response;

        }

        private string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet]
        [Route("get-user/{id:int}")]
        public async Task<User> Get(int id)
        {
            
            return await _userAppService.GetUser(id);
            
        }

       

    }
}
