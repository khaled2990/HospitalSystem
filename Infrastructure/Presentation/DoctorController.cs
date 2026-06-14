using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTO;
namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class Doctor01Controller : ControllerBase
    {
        private readonly IServiceMamager _serviceMamager;

        public Doctor01Controller(IServiceMamager serviceMamager)
        {
            _serviceMamager = serviceMamager;
        }

        [HttpGet("GetAllPatientsWihDoctors")]
        public async Task<ActionResult<IEnumerable<Doctor_Patient>>> GetAllPatientsWihDoctors([FromQuery] QueryParameters query)
        {
            var PatientsWihDoctor = await _serviceMamager.doctorService.GetAllDoctorWithPatient(query);
            return Ok(PatientsWihDoctor);
        }
        [HttpGet("PatientsWihDoctor/{id}")]
        public async Task<ActionResult<Doctor_Patient>> GetByIdPatientsWihDoctor(int id)
        {
            var doctor =await _serviceMamager.doctorService.GetByIdDoctorWithPatient(id);
            return Ok(doctor);
        }
        [HttpGet("GetByIdDoctor/{id}")]
        public async Task<ActionResult<DoctorDto>> GetByIdDoctor(int id)
        {
            var doctor = await _serviceMamager.doctorService.GetByIdDoctor(id);
            return Ok(doctor);
        }
        [HttpGet("GetAllDoctors")]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetAllDoctors([FromQuery] QueryParameters query)
        {
            var PatientsWihDoctor = await _serviceMamager.doctorService.GetAllDoctor(query);
            return Ok(PatientsWihDoctor);
        }
    }
}
