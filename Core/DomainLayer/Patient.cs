using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class Patient
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Age { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public ICollection<Appointment> Appointments { get; set; }=new List<Appointment>();
    }
}
