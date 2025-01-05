using Microsoft.AspNetCore.Mvc;
using FisioScan.Business;
using FisioScan.Models;
using Microsoft.AspNetCore.Authorization;


namespace FisioScan.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{
    private readonly ILogger<PatientController> _logger;

    private readonly IPatientService _patientService;

    public PatientController(ILogger<PatientController> logger, IPatientService patientService)
    {
        _logger = logger;
        _patientService = patientService;
    }

    [HttpGet(Name = "GetAllPatients")]
    public ActionResult<IEnumerable<Patient>> SearchPatient(string? dni)
    {
        var patients = _patientService.GetPatients(dni);

        if (!patients.Any())
        {
            return NotFound();
        }

        var transformedPatients = patients.Select(
            p => new
            {
                p.PatientId,
                p.Name
            }
        );

        return Ok(transformedPatients);
    }
}