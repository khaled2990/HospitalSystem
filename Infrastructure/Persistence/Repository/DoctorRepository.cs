using DomainLayer;
using DomainLayer.Contracts;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Service.Specifications;
namespace Persistence.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HospitalContext _hospitalContext;
        public DoctorRepository(HospitalContext hospitalContext)
        {
            _hospitalContext = hospitalContext;
        }
        public async Task<IEnumerable<Doctor>> GetAllDoctorWithPatient(ISpecification<Doctor> specification)
        {
            var result = _hospitalContext.Set<Doctor>().Include(d => d.Appointments!).ThenInclude(a => a.Patient);
            return await ApplySpecification<Doctor>.GetQuert(result, specification).ToListAsync();



        }
        public async Task<Doctor?> GetByIdDoctor(int id)
        {
            return await _hospitalContext.Set<Doctor>().FindAsync(id);

        }
        public async Task<Doctor?> GetByIdDoctorWithPatient(int id)
        {
            return await _hospitalContext.Set<Doctor>().Include(a => a.Appointments!).ThenInclude(p => p.Patient).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task Add(Doctor entity)
        {
           var result= await _hospitalContext.Set<Doctor>().AddAsync(entity);
            if(result!=null)
               await _hospitalContext.SaveChangesAsync();
            else
                throw new KeyNotFoundException($"Don`t Create Doctor");

        }
        public async Task Delete(int id)
        {
            var doctor = await _hospitalContext.Set<Doctor>().FindAsync(id);
            if (doctor is not null)
            {
                var result = await _hospitalContext.Set<Appointment>().Where(x=>x.DoctorId==id).ToListAsync();
                if (result != null)
                {
                    foreach (Appointment appointment in result)
                    {
                        _hospitalContext.Remove(appointment);
                    }
                }
                _hospitalContext.Remove(doctor);
                await _hospitalContext.SaveChangesAsync();
            }
            else
                throw new KeyNotFoundException($"Doctor with id {id} not found");
        }


        public async Task<IEnumerable<Doctor>> GetAllDoctor(ISpecification<Doctor> specification)
        {
            return await ApplySpecification<Doctor>.GetQuert(_hospitalContext.Set<Doctor>(), specification).ToListAsync();
        }

        public async Task<int> SaveChange()
        {
            return await _hospitalContext.SaveChangesAsync();
        }

    }
}
