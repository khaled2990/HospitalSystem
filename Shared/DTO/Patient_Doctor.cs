using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class Patient_Doctor
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Age { get; set; }
        public string PhoneNumber { get; set; } = null!;

        public List<DoctorDto> doctors { get; set; } = new();

    }
}
