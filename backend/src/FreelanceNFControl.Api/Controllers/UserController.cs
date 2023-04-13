using FreelanceNFControl.Domain.Entities;
using FreelanceNFControl.Domain.Interfaces;
using FreelanceNFControl.Infra.Core.Requests.User;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceNFControl.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(
            IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserRequest request)
        {
            if (!request.IsValid())
            {
                return BadRequest(request.GetErrors());
            }

            var user = new User(request.FirstName, request.LastName, request.UserName, request.Email);

            await _userService.Add(user, request.Password);

            var loginResponse = await _userService.AuthenticateUser(user, request.Password);

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser(LoginRequest request)
        {
            if (!request.IsValid())
            {
                return BadRequest(request.GetErrors());
            }

            var loginResponse = await _userService.AuthenticateUser(request);

            return Ok(loginResponse);
        }
    }
}
