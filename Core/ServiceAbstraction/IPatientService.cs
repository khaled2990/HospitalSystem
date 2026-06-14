using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IPatientService
    {
        public Task<IEnumerable<PatientDto>?> GetAllPatients(QueryParameterPatient query);
        public Task<IEnumerable<Patient_Doctor>?> GetAllPatientsWithDoctors(QueryParameterPatient query);
        public Task<Patient_Doctor?> GetPatientWithDoctorById(int id);
        public Task<PatientDto?> GetPatientById(int id);
        public Task CreatePatient(CreatePatientDto patient);
        public Task UpdatePatient(int patientId, int doctorId, UpdatePatientDto patient);
        public Task DeletePatient(int patientId, int doctorId);
    }
}
