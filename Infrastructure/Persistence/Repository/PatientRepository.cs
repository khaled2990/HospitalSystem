using DomainLayer;
using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Service.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly HospitalContext _hospitalContext;

        public PatientRepository(HospitalContext hospitalContext)
        {
            _hospitalContext = hospitalContext;
        }
        public async Task Add(int DoctorId, Patient entity)
        {
            var result = await _hospitalContext.Set<Patient>().AddAsync(entity);
            await _hospitalContext.SaveChangesAsync();

            if (result != null)
            {
                var doctor = await _hospitalContext.Set<Doctor>().FindAsync(DoctorId);
                if (doctor != null)
                {
                    Appointment appointment = new Appointment
                    {
                        DoctorId = DoctorId,
                        PatientId = entity.Id
                    };
                    var falg = await _hospitalContext.Set<Appointment>().AddAsync(appointment);
                    if (falg != null)
                        await _hospitalContext.SaveChangesAsync();
                    else
                        throw new KeyNotFoundException($"Don`t Create Patient");

                }
                else
                    throw new KeyNotFoundException($"Don`t Create Patient");
            }
            else
                throw new KeyNotFoundException($"Don`t Create Patient");


        }


        public async Task Delete(int patientId, int doctorId)
        {
            var patient = await _hospitalContext.Set<Patient>().FindAsync(patientId);
            if (patient == null)
                throw new KeyNotFoundException($"Patient with id {patientId} not found");
            else
            {
                var result = await _hospitalContext.Set<Appointment>().FirstOrDefaultAsync(x => x.PatientId == patientId && x.DoctorId == doctorId);
                if (result != null)
                {
                    _hospitalContext.Set<Patient>().Remove(patient);
                    _hospitalContext.Set<Appointment>().Remove(result);
                    await _hospitalContext.SaveChangesAsync();
                }
                else throw new KeyNotFoundException($"Appointment not found");
            }


        }

        public async Task<IEnumerable<Patient>?> GetAllPatientsWithDoctors(ISpecification<Patient> specification)
        {
            var patient = _hospitalContext.Set<Patient>().Include(x => x.Appointments).ThenInclude(x => x.Doctor);
            return await ApplySpecification<Patient>.GetQuert(patient, specification).ToListAsync();
        }
        public async Task<IEnumerable<Patient>?> GetAllPatients(ISpecification<Patient> specification)
        {
            var patient = _hospitalContext.Set<Patient>();
            return await ApplySpecification<Patient>.GetQuert(patient, specification).ToListAsync();
        }

        public async Task<Patient?> GetPatientWithDoctorById(int id)
        {
            return await _hospitalContext.Set<Patient>().Include(x => x.Appointments).ThenInclude(x => x.Doctor).FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<Patient?> GetPatientById(int id)
        {
            return await _hospitalContext.Set<Patient>().FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<int> SaveChange()
        {
            return await _hospitalContext.SaveChangesAsync();
        }

        public async Task<Appointment?> GetAppByPatAndDocId(int DoctorId, int PatientId)
        {
            var result = await _hospitalContext.Set<Appointment>().FirstOrDefaultAsync(x => x.PatientId == PatientId && x.DoctorId == DoctorId);
            if (result != null)
                return result;
            else
                throw new KeyNotFoundException($"Not Found Appointment");


        }
    }
}
