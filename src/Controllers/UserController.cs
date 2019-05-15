using Couchbase.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoAppLite.Common.Auth;
using TodoAppLite.Models;
using TodoAppLite.Services;

namespace TodoAppLiteControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly IUserService userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            this.logger = logger;
            this.userService = userService;
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterUser model)
        {
            userService.Register(model);
            return Ok();
        }

        [HttpPost("Login")]
        public JsonWebToken Login([FromBody] LoginModel model)
        {
           return userService.Login(model);
        }
    }
}