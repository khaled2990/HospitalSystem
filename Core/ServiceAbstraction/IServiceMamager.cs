using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IServiceMamager
    {
        public IDoctorService doctorService { get; }
        public IAuthenticationService authenticationService { get; }
        public IPatientService patientService { get; }
    }
}
