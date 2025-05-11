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

    public class GeneralAssessmentController : ControllerBase
    {
        private readonly ILogger<GeneralAssessmentController> _logger;
        private readonly IGeneralAssessmentService _generalAssessmentService;
        private readonly IAuthService _authService;

        public GeneralAssessmentController(ILogger<GeneralAssessmentController> logger, IGeneralAssessmentService generalAssessmentService, IAuthService authService)
        {
            _logger = logger;
            _generalAssessmentService = generalAssessmentService;
            _authService = authService;
        }

        [Authorize]
        [HttpGet(Name = "GetAllGeneralAssessments")]
        public ActionResult<IEnumerable<GeneralAssessment>> SearchGeneralAssessment(int? generalAssessmentId, int? createdBy, int? treatmentId, string? usualPhysicalActivity, string? height, string? weight, string? occupation, string? medicalHistory)
        {
            if (_authService.HasAccessToResource(User, out int? rolePhysioId))
            {
                if (rolePhysioId == null)
                {
                    var generalAssessments = _generalAssessmentService.GetGeneralAssessments(generalAssessmentId, createdBy, treatmentId, usualPhysicalActivity, height, weight, occupation, medicalHistory);

                    if (generalAssessments == null || !generalAssessments.Any())
                    {
                        return NotFound("No se encontraron valoraciones generales con los parámetros proporcionados.");
                    }

                    return Ok(generalAssessments);
                }

                if (rolePhysioId.HasValue)
                {
                    createdBy = rolePhysioId.Value;
                    var generalAssessments = _generalAssessmentService.GetGeneralAssessments(generalAssessmentId, createdBy, treatmentId, usualPhysicalActivity, height, weight, occupation, medicalHistory);

                    if (generalAssessments == null || !generalAssessments.Any())
                    {
                        return NotFound("No se encontraron valoraciones generales con los parámetros proporcionados.");
                    }

                    var transformedGeneralAssessments = generalAssessments.Select(p => new
                    {
                        p.GeneralAssessmentId,
                        p.CreatedBy,
                        p.TreatmentId,
                        p.UsualPhysicalActivity,
                        p.Height,
                        p.Weight,
                        p.Occupation,
                        p.MedicalHistory
                    }).ToList();

                    return Ok(transformedGeneralAssessments);
                }
            }

            return Unauthorized("Acceso denegado");
        }
    }
}