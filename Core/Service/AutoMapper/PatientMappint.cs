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
    public class PatientMappint : Profile
    {
        public PatientMappint()
        {
            CreateMap<Patient, Patient_Doctor>().ForMember(
                x => x.doctors, option => option.MapFrom(x=>x.Appointments.Select(y=>y.Doctor))).ReverseMap();
            CreateMap<Doctor, DoctorDto>().ReverseMap();
            CreateMap<Patient, PatientDto>().ReverseMap();
            CreateMap<Patient, CreatePatientDto>().ReverseMap();
        }
    }
}
