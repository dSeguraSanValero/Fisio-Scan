using System.ComponentModel.DataAnnotations;

namespace FisioSolution.Models
{
    public class RegisterPhysioDTO
    {
        [Required]
        [StringLength(100, ErrorMessage = "El nombre debe tener menos de 100 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "El apellido debe tener menos de 100 caracteres")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "Formato de correo electrónico no válido")]
        [StringLength(100, ErrorMessage = "El correo debe tener menos de 100 caracteres")]
        public string Email { get; set; } = string.Empty;

        [Required]
        public int RegistrationNumber { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "La contraseña debe tener entre 5 y 100 caracteres")]
        public string Password { get; set; } = string.Empty;
    }
}
