using AutoMapper;
using DomainLayer;
using DomainLayer.Contracts;
using Service.Specifications;
using ServiceAbstraction;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;

        public DoctorService(IDoctorRepository doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task Create(DoctorDto doctor)
        {
            var result=_mapper.Map<DoctorDto,Doctor>(doctor);
            await _doctorRepository.Add(result);
        }

        public async Task Delete(int id)
        {

           await _doctorRepository.Delete(id);
     
        }

        public async Task<IEnumerable<DoctorDto>?> GetAllDoctor(QueryParameters query)
        {
            var doctorSpec = new DoctorSpecification(query);
            var doctor =await _doctorRepository.GetAllDoctor(doctorSpec);
            return _mapper.Map<IEnumerable<Doctor>, IEnumerable<DoctorDto>>(doctor);
        }

        public async Task<IEnumerable<Doctor_Patient>?> GetAllDoctorWithPatient(QueryParameters query)
        {
            var doctorSpec = new DoctorSpecification(query);
            var doctor =await _doctorRepository.GetAllDoctorWithPatient(doctorSpec);
            return _mapper.Map<IEnumerable<Doctor>, IEnumerable<Doctor_Patient>>(doctor);
        }

        public async Task<DoctorDto?> GetByIdDoctor(int id)
        {
            var doctor = await _doctorRepository.GetByIdDoctor(id);
            if(doctor == null)return null;
            return _mapper.Map<Doctor,DoctorDto>(doctor);
        }

        public async Task<Doctor_Patient?> GetByIdDoctorWithPatient(int id)
        {
            var doctor = await _doctorRepository.GetByIdDoctorWithPatient(id);
            if (doctor == null) return null;
            return _mapper.Map<Doctor, Doctor_Patient>(doctor);
        }


     

        public async Task Update(int id, DoctorDto dto)
        {
            var doctor =await _doctorRepository.GetByIdDoctor(id);
            if (doctor == null)
            {
                throw new KeyNotFoundException($"Patient with id {id} not found");
            }
            dto.Id= id;
            if (!string.IsNullOrWhiteSpace(dto.Name))
            {
                doctor.Name= dto.Name;
            }
            if (!string.IsNullOrWhiteSpace(dto.Specialization))
            {
                doctor.Specialization = dto.Specialization;
            }
            if (!string.IsNullOrWhiteSpace(dto.PhoneNumber))
            {
                doctor.PhoneNumber = dto.PhoneNumber;
            }
            if (dto.Price >0)
            {
                doctor.Price = dto.Price;
            }
           await _doctorRepository.SaveChange();

        }
    }
}
