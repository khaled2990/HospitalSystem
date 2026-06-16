using DomainLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Service;
using ServiceAbstraction;
using Shared.DTO;
using System.Security.Claims;
using System.Text;

namespace HospitalSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController:ControllerBase
    {
        private readonly IServiceMamager _serviceManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationController(IServiceMamager serviceManager,UserManager<ApplicationUser> userManager ) {
            _serviceManager = serviceManager;
            _userManager = userManager;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            
            return Ok(await _serviceManager.authenticationService.Login(loginDto));
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";

            var result = await _serviceManager
                .authenticationService
                .Register(registerDto, baseUrl);

            return Ok(result);
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

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return BadRequest("User not found");

            try
            {
                var decodedTokenBytes = WebEncoders.Base64UrlDecode(token);
                var originalToken = Encoding.UTF8.GetString(decodedTokenBytes);
                var result = await _userManager.ConfirmEmailAsync(user, originalToken);

                if (!result.Succeeded)
                    return BadRequest("Invalid token");

                return Ok("Email confirmed successfully");
            }
            catch (Exception)
            {
                return BadRequest("Error processing the token");
            }
        }

        [HttpGet("test-email")]
        public async Task<ActionResult> TestEmail()
        {
            await _serviceManager.emailService.SendEmailAsync(
                "khadad819@gmail.com",
                "Test Email",
                "https://google.com");

            return Ok("Email sent");
        }

    }
}
