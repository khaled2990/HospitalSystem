using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class DoctorDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Specialization { get; set; }

        public string? PhoneNumber { get; set; }

        public decimal Price { get; set; }
    }
}
