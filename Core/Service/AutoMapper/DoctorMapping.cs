using AutoMapper;
using DomainLayer;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AutoMapper
{
    public class DoctorMapping:Profile
    {
      public DoctorMapping() {
            CreateMap<Doctor, DoctorDto>();

            CreateMap<Doctor, Doctor_Patient>()
                .ForMember(x => x.NameDoctor, option => option.MapFrom(D => D.Name))
                .ForMember(x => x.NamePatient,
                option => option.MapFrom(d => d.Appointments
               .Select(a => a.Patient.Name)
               .ToList()));

            CreateMap<Patient, Patient_Doctor>().ReverseMap();

        }
    }
}
