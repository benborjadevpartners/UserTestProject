using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using UserTestProject.Models;
using UserTestProject.Services;

namespace UserTestProject.Controllers
{
    
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
       
        private readonly ILogger<UsersController> _logger;
        private readonly IUserAppService _userAppService;

        public UsersController(ILogger<UsersController> logger,
            IUserAppService userAppService)
        {
            _logger = logger;
            _userAppService = userAppService;
        }

        [Authorize]
        [HttpGet]
        [Route("get-all")]
        public async Task<IEnumerable<User>> Get()
        {            
            return await _userAppService.GetUsers();
        }

        [Authorize]
        [HttpGet]
        [Route("get-user/{id:int}")]
        public async Task<User> Get(int id)
        {
            
            return await _userAppService.GetUser(id);
            
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("add-user")]
        public async Task<User> Post(User user)
        {
            return await _userAppService.AddUser(user);
        }

        [Authorize]
        [HttpPut]
        [Route("edit-user/{id:int}")]
        public async Task<User> Put(int id, User user)
        {
            return await _userAppService.EditUser(id,user);
        }

        [Authorize]
        [HttpDelete]
        [Route("delete-user/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userAppService.DeleteUser(id);
            return Ok(result);

        }

    }
}
