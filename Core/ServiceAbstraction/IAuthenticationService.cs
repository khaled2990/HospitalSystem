using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        public Task<UserDto> Login(LoginDto loginDto);
        public Task<UserDto> Register(RegisterDto registerDto);
        public Task<bool> CheckEmail(string email);
        public Task<UserDto> GetCurrentUser(string email);
    }
}
