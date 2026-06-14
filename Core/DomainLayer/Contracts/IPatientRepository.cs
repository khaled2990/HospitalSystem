using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IPatientRepository
    {
       public Task<IEnumerable<Patient>?> GetAllPatients(ISpecification<Patient> specification);
       public Task<IEnumerable<Patient>?> GetAllPatientsWithDoctors(ISpecification<Patient> specification);
       public Task<Patient?> GetPatientWithDoctorById(int id);
       public Task<Patient?> GetPatientById(int id);
       public Task<Appointment?> GetAppByPatAndDocId(int DoctorId,int PatientId);
        Task Delete(int patientId, int doctorId);
        Task Add(int idDoctor, Patient entity);
        Task<int> SaveChange();
    }
}
