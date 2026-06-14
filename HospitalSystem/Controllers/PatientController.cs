using DomainLayer;
using Microsoft.AspNetCore.Mvc;
using Service;
using ServiceAbstraction;
using Shared.DTO;
using System.Numerics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HospitalSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IServiceMamager _serviceMamager;

        public PatientController(IServiceMamager serviceMamager)
        {
            _serviceMamager = serviceMamager;
        }

        [HttpGet("GetPatientById/{id}")]
        public async Task<ActionResult<Patient_Doctor>> GetPatientById(int id)
        {

            try
            {
                var result = await _serviceMamager.patientService.GetPatientById(id);
                if (result == null) return NotFound(new { message = "Not Found Doctor" });

                return Ok(result);
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
        [HttpGet("GetPatientWithDoctorById/{id}")]
        public async Task<ActionResult<Patient_Doctor>> GetPatientWithDoctorById(int id)
        {

            try
            {
                var result = await _serviceMamager.patientService.GetPatientWithDoctorById(id);
                if (result == null) return NotFound(new { message = "Not Found Doctor" });
                return Ok(result);
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
        [HttpGet("GetAllPatients")]
        public async Task<ActionResult<IEnumerable<Patient_Doctor>>> GetAllPatients([FromQuery] QueryParameterPatient query)
        {
            try
            {
                var result = await _serviceMamager.patientService.GetAllPatients(query);
                if(result == null)return NotFound(new {massage= "Not Found Doctor" });
                return Ok(result);
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

        [HttpGet("GetAllPatientsWithDoctors")]
        public async Task<ActionResult<IEnumerable<Patient_Doctor>>> GetAllPatientsWithDoctors([FromQuery] QueryParameterPatient query)
        {
            try
            {
                var result = await _serviceMamager.patientService.GetAllPatientsWithDoctors(query);
                if (result == null) if (result == null) return NotFound(new { message = "Not Found Doctor" });
                return Ok(result);
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
        public async Task<ActionResult> CreatePatient(CreatePatientDto patient)
        {
            try
            {
                await _serviceMamager.patientService.CreatePatient(patient);
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
        [HttpPut("{doctorId}")]
        public async Task<ActionResult> UpdatePatient(int patientId, int doctorId, UpdatePatientDto patient)
        {

            try
            {
                await _serviceMamager.patientService.UpdatePatient( patientId,  doctorId, patient);
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
        public async Task<ActionResult> DeletePatient(int patientId, int doctorId)
        {
            try
            {
                await _serviceMamager.patientService.DeletePatient( patientId,  doctorId);
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
