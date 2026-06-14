using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDto>?> GetAllDoctor(QueryParameters query);
        Task<IEnumerable<Doctor_Patient>?> GetAllDoctorWithPatient(QueryParameters query);
        Task<DoctorDto?> GetByIdDoctor(int id);
        Task<Doctor_Patient?> GetByIdDoctorWithPatient(int id);
        Task Delete(int id);
        Task Create(DoctorDto doctor);
        Task Update(int id,DoctorDto dto);
        
    }
}
