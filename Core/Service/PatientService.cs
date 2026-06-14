using AutoMapper;
using DomainLayer;
using DomainLayer.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
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
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientService(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }
        public async Task CreatePatient(CreatePatientDto patient)
        {
            var result = _mapper.Map<CreatePatientDto, Patient>(patient);
            await _patientRepository.Add(patient.DoctorId,result);
        }



        public async Task DeletePatient(int patientId, int doctorId)
        {
            
           await _patientRepository.Delete( patientId,  doctorId);

        }
        public async Task<IEnumerable<PatientDto>?> GetAllPatients(QueryParameterPatient query)
        {
            var Specification = new PantientSpecification(query);
            var patient =await _patientRepository.GetAllPatients(Specification);
            return _mapper.Map<IEnumerable<PatientDto>>(patient);
        }
        public async Task<IEnumerable<Patient_Doctor>?> GetAllPatientsWithDoctors(QueryParameterPatient query)
        {
            var Specification = new PantientSpecification(query);
            var patient =await _patientRepository.GetAllPatients(Specification);
            return _mapper.Map<IEnumerable<Patient_Doctor>>(patient);
        }
        public async Task<Patient_Doctor?> GetPatientWithDoctorById(int id)
        {
            var patient = await _patientRepository.GetPatientWithDoctorById(id);
            if (patient is null)
                throw new KeyNotFoundException($"Patient with id {id} was not found");
            return _mapper.Map<Patient, Patient_Doctor>(patient);

        }
        public async Task<PatientDto?> GetPatientById(int id)
        {
            var patient = await _patientRepository.GetPatientById(id);
            if (patient is null)
                throw new KeyNotFoundException($"Patient with id {id} was not found");
            return _mapper.Map<Patient, PatientDto>(patient);

        }

        public async Task UpdatePatient(int patientId,int doctorId, UpdatePatientDto dto)
        {
            var patient = await _patientRepository.GetPatientById(patientId);
            var appointment =await _patientRepository.GetAppByPatAndDocId(doctorId, patientId);
            if (patient == null)
                throw new KeyNotFoundException();

            if (!string.IsNullOrEmpty(dto.Name))
                patient.Name = dto.Name;
            if (dto.Age != 0)
                patient.Age = dto.Age;
            if (!string.IsNullOrEmpty(dto.PhoneNumber))
                patient.PhoneNumber = dto.PhoneNumber;
            if (doctorId != dto.DoctorId)
            {
                appointment!.DoctorId = dto.DoctorId;
            }
            await _patientRepository.SaveChange();

        }
    }
}
