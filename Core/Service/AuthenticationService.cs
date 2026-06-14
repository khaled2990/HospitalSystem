using DomainLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationService(UserManager<ApplicationUser> userManager ,IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<bool> CheckEmail(string email)
        {
           var result=await _userManager.FindByEmailAsync(email);
            return result is not null;
        }
        public async Task<UserDto> GetCurrentUser(string email)
        {
           
            var result = await _userManager.FindByEmailAsync(email);
            if (result is not null)
                return new UserDto()
                {
                    Email = email,
                    DisPlayName = result.DisPlayName,
                    Token =await CreateToken(result)
                };

            else
                throw new NotImplementedException();
        }

        public async Task<UserDto> Login(LoginDto loginDto)
        {
            var user=await _userManager.FindByEmailAsync(loginDto.Email);
            if(user is null)
                 throw new NotImplementedException();
            var pass=_userManager.CheckPasswordAsync(user, loginDto.Password);
            if(pass is null)
                throw new NotImplementedException();

            return new UserDto()
            {
                DisPlayName = user.DisPlayName,
                Email = user.Email!,
                Token =await CreateToken(user)

            };
        }

       

        public async Task<UserDto> Register(RegisterDto registerDto)
        {
            var user = new ApplicationUser()
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                DisPlayName = registerDto.DisPlayName,
                PhoneNumber = registerDto.PhoneNumber
            };
            var result=await _userManager.CreateAsync(user,registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ",
                    result.Errors.Select(e => e.Description));

                throw new Exception(errors);
            }

            return new UserDto()
            {
                DisPlayName = user.DisPlayName,
                Email = user.Email!,
                Token =await CreateToken(user)
            };

        }

        private async Task<string> CreateToken(ApplicationUser user)
        {
            var claim = new List<Claim>()
            {
                new(JwtRegisteredClaimNames.Email,user.Email!),
                new(ClaimTypes.NameIdentifier,user.Id),
                new(JwtRegisteredClaimNames.Name,user.UserName!),
               new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),

            };
            var Roles=await _userManager.GetRolesAsync(user);
            foreach (var item in Roles)
            {
                claim.Add(new Claim( ClaimTypes.Role, item));
            }
            var SecretKey =_configuration["JWT:SecretKey"];
            var key= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey!));
            var credent=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:issuer"],
                audience: _configuration["JWT:audience"],
                expires:DateTime.Now.AddHours(1),
                claims: claim,
                signingCredentials: credent
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
