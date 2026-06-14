using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllDoctorWithPatient(ISpecification<Doctor> specification);
        Task<IEnumerable<Doctor>> GetAllDoctor(ISpecification<Doctor> specification);
        Task<Doctor?> GetByIdDoctor(int id);
        Task<Doctor?> GetByIdDoctorWithPatient(int id);
        Task Delete(int id);
        Task Add(Doctor entity);
        Task<int> SaveChange();
    }
}
