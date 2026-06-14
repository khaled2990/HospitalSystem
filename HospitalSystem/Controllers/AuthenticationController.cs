using DomainLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service;
using ServiceAbstraction;
using Shared.DTO;
using System.Security.Claims;

namespace HospitalSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController:ControllerBase
    {
        private readonly IServiceMamager _serviceManager;

        public AuthenticationController(IServiceMamager serviceManager ) {
            _serviceManager = serviceManager;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            return Ok(await _serviceManager.authenticationService.Login(loginDto));
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            return Ok(await _serviceManager.authenticationService.Register(registerDto));
        }

        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            return await _serviceManager.authenticationService.CheckEmail(email);
        }
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email=User.FindFirstValue(ClaimTypes.Email);
            return await _serviceManager.authenticationService.GetCurrentUser(email!);
        }




    }
}
