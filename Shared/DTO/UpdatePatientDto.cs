using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class UpdatePatientDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string? Name { get; set; }

        public int Age { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
