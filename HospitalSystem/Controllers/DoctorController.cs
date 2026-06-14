using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTO;
namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IServiceMamager _serviceMamager;

        public DoctorController(IServiceMamager serviceMamager)
        {
            _serviceMamager = serviceMamager;
        }
        [HttpGet("GetByIdDoctor/{id}")]
        public async Task<ActionResult<DoctorDto>> GetByIdDoctor(int id)
        {
            try
            {
                var doctor = await _serviceMamager.doctorService.GetByIdDoctor(id);

                if (doctor == null)
                    return NotFound(new { message = "Not Found Doctor" });

                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error",
                    error = ex.Message
                });
            }
        }
        [Authorize(Roles = "SuperAdmin")]

        [HttpGet("GetByIdDoctorWihPatients/{id}")]
        public async Task<ActionResult<Doctor_Patient>> GetByIdDoctorWihPatients(int id)
        {
            try
            {
                var doctor = await _serviceMamager.doctorService.GetByIdDoctorWithPatient(id);

                if (doctor == null)
                    return NotFound(new { message = "Not Found Doctor" });

                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error",
                    error = ex.Message
                });
            }
        }
        [HttpGet("GetAllDoctors")]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetAllDoctors([FromQuery] QueryParameters query)
        {

            try
            {
                var PatientsWihDoctor = await _serviceMamager.doctorService.GetAllDoctor(query);

                if (PatientsWihDoctor == null)
                    return NotFound(new { message = "Not Found Doctor" });

                return Ok(PatientsWihDoctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error",
                    error = ex.Message
                });
            }
        }
        [HttpGet("GetAllDoctorsWithPatients")]
        public async Task<ActionResult<IEnumerable<Doctor_Patient>>> GetAllDoctorsWithPatients([FromQuery] QueryParameters query)
        {

            try
            {
                var PatientsWihDoctor = await _serviceMamager.doctorService.GetAllDoctorWithPatient(query);

                if (PatientsWihDoctor == null)
                    return NotFound(new { message = "Not Found Doctor" });

                return Ok(PatientsWihDoctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error",
                    error = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateDoctor(DoctorDto doctor)
        {
            
            try
            {
                await _serviceMamager.doctorService.Create(doctor);
                return Ok(new
                {
                    message = "Done",

                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Don`t Create",
                    error = ex.Message
                });
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, DoctorDto doctor)
        {          
            try
            {
                await _serviceMamager.doctorService.Update(id, doctor);
                return Ok(new
                {
                    message = "Done",

                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Don`t Update",
                    error = ex.Message
                });
            }
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteByIdDoctor(int id)
        {
            try
            {
                await _serviceMamager.doctorService.Delete(id);
                return Ok(new
                {
                    message = "Done",

                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Don`t Delete",
                    error = ex.Message
                });
            }

        }
    }
}
