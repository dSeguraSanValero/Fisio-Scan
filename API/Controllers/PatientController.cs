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
        public ActionResult<IEnumerable<object>> SearchPatient(string? dni, int? createdBy, string? name, string? firstSurname, string? secondSurname)
        {
            if (_authService.HasAccessToResource(User, out int? physioId))
            {
                if (physioId == null)
                {
                    var patients = _patientService.GetPatients(dni, null, name, firstSurname, secondSurname);
                    
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
                        p.Dni
                    }).ToList();

                    return Ok(transformedPatients);
                }

                if (physioId.HasValue)
                {
                    createdBy = physioId.Value;
                    var patients = _patientService.GetPatients(dni, createdBy, name, firstSurname, secondSurname);

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
                        p.Dni
                    }).ToList();

                    return Ok(transformedPatients);
                }
            }
            
            return Unauthorized("Acceso denegado");
        }
    }
}

