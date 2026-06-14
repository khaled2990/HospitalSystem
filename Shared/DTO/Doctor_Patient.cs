using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class Doctor_Patient
    {
        public int Id { get; set; }

        public string NameDoctor { get; set; } = null!;

        public string Specialization { get; set; } = null!;
        public List<string> NamePatient { get; set; } = null!;

    }
}
