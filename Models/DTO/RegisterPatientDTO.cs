using System.ComponentModel.DataAnnotations;

namespace FisioScan.Models
{
    public class RegisterPatientDTO
    {
        [Required]
        [StringLength(50, ErrorMessage = "El nombre debe tener menos de 50 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "El apellido debe tener menos de 50 caracteres")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "El apellido debe tener menos de 50 caracteres")]
        public string SecondName { get; set; } = string.Empty;

        [Required]
        [StringLength(10, MinimumLength = 8, ErrorMessage = "El DNI debe tener entre 8 y 10 dígitos")]
        public string? Dni { get; set; }
    }
}