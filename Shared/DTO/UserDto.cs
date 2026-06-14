using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class UserDto
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string Token { get; set; }=null!;
        public string DisPlayName { get; set; } = null!;
    }
}
