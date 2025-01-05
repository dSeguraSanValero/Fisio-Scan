using Microsoft.AspNetCore.Mvc;
using FisioScan.Models;
using FisioScan.Business;
using FisioSolution.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FisioScan.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhysioController : ControllerBase
    {
        private readonly IPhysioService _physioService;

        public PhysioController(IPhysioService physioService)
        {
            _physioService = physioService;
        }

        [HttpGet(Name = "GetAllPhysios")]
        public ActionResult<IEnumerable<Physio>> SearchPhysio()
        {
            var physios = _physioService.GetPhysios();

            if (!physios.Any())
            {
                return NotFound();
            }

            return Ok(physios);
        }

        [HttpPost("register")]
        public IActionResult RegisterPhysio([FromBody] RegisterPhysioDTO physioDto)
        {
            if (physioDto == null)
            {
                return BadRequest("Datos del fisioterapeuta son requeridos.");
            }

            _physioService.RegisterPhysio(
                physioDto.Name,
                physioDto.LastName,
                physioDto.Email,
                physioDto.RegistrationNumber,
                physioDto.Password
            );

            return CreatedAtRoute("GetAllPhysios", new { registrationNumber = physioDto.RegistrationNumber }, physioDto);
        }
    }
}

