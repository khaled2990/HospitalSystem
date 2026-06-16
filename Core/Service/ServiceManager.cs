using AutoMapper;
using DomainLayer;
using DomainLayer.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager(IDoctorRepository _doctorRepository, IMapper _mapper, UserManager<ApplicationUser> userManager,IConfiguration configuration, IPatientRepository patientRepository,IEmailService emailService) : IServiceMamager
    {

        private readonly Lazy<IDoctorService> _LazydoctorService = new Lazy<IDoctorService>(() => new DoctorService(_doctorRepository, _mapper));
        private readonly Lazy<IAuthenticationService> _LazyauthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, configuration, emailService));
        private readonly Lazy<IPatientService> _LazyPatientService = new Lazy<IPatientService>(() => new PatientService(patientRepository, _mapper));
        private readonly Lazy<IEmailService> _LazyEmailService = new Lazy<IEmailService>(() => new EmailService(configuration));
        public IDoctorService doctorService => _LazydoctorService.Value;
        public IAuthenticationService authenticationService => _LazyauthenticationService.Value;

        public IPatientService patientService =>_LazyPatientService.Value;
        public IEmailService emailService => _LazyEmailService.Value;
    }
}
