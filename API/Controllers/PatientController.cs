using Microsoft.AspNetCore.Mvc;
using FisioScan.Models;
using FisioScan.Business;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace FisioScan.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly ILogger<PatientController> _logger;
        private readonly IPatientService _patientService;
        private readonly IAuthService _authService;

        public PatientController(ILogger<PatientController> logger, IPatientService patientService, IAuthService authService)
        {
            _logger = logger;
            _patientService = patientService;
            _authService = authService;
        }

        [Authorize]
        [HttpGet(Name = "GetAllPatients")]
        public ActionResult<IEnumerable<Patient>> SearchPatient(string? dni, int? createdBy, string? name, string? firstSurname, string? secondSurname, DateTime birthDate)
        {
            if (_authService.HasAccessToResource(User, out int? rolePhysioId))
            {
                if (rolePhysioId == null)
                {
                    var patients = _patientService.GetPatients(dni, createdBy, name, firstSurname, secondSurname, birthDate);
                    
                    if (patients == null || !patients.Any())
                    {
                        return NotFound("No se encontraron pacientes con los parámetros proporcionados.");
                    }

                    var transformedPatients = patients.Select(p => new
                    {
                        p.PatientId,
                        p.CreatedBy,
                        p.Name,
                        p.FirstSurname,
                        p.SecondSurname,
                        p.Dni,
                        BirthDate = p.BirthDate.ToString("yyyy-MM-dd")
                    }).ToList();

                    return Ok(transformedPatients);
                }

                if (rolePhysioId.HasValue)
                {
                    createdBy = rolePhysioId.Value;
                    var patients = _patientService.GetPatients(dni, createdBy, name, firstSurname, secondSurname, birthDate);

                    if (patients == null || !patients.Any())
                    {
                        return NotFound("No se encontraron pacientes con los parámetros proporcionados.");
                    }

                    var transformedPatients = patients.Select(p => new
                    {
                        p.PatientId,
                        p.CreatedBy,
                        p.Name,
                        p.FirstSurname,
                        p.SecondSurname,
                        p.Dni,
                        BirthDate = p.BirthDate.ToString("yyyy-MM-dd")
                    }).ToList();

                    return Ok(transformedPatients);
                }
            }
            
            return Unauthorized("Acceso denegado");
        }

        [Authorize]
        [HttpPost]
        public IActionResult RegisterPatient([FromBody] RegisterPatientDTO patientDTO)
        {
            if (_authService.HasAccessToResource(User, out int? rolePhysioId))
            {
                if (rolePhysioId == null)
                {
                    try
                    {
                        _patientService.RegisterPatient(
                            name: patientDTO.Name,
                            dni: patientDTO.Dni,
                            firstSurname: patientDTO.FirstSurname,
                            secondSurname: patientDTO.SecondSurname,
                            createdBy: 1,
                            birthDate: patientDTO.BirthDate
                        );

                        return CreatedAtAction(nameof(SearchPatient), new { dni = patientDTO.Dni }, patientDTO);
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.Message);
                    }
                }

                if (rolePhysioId.HasValue)
                {
                    try
                    {
                        _patientService.RegisterPatient(
                            name: patientDTO.Name,
                            dni: patientDTO.Dni,
                            firstSurname: patientDTO.FirstSurname,
                            secondSurname: patientDTO.SecondSurname,
                            createdBy: rolePhysioId.Value,
                            birthDate: patientDTO.BirthDate
                        );

                        return CreatedAtAction(nameof(SearchPatient), new { dni = patientDTO.Dni }, patientDTO);
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.Message);
                    }
                }
                
            }

        return Unauthorized("Acceso denegado");
        }
    }
}

