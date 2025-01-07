using System.ComponentModel.DataAnnotations;

namespace FisioScan.Models;

public class UpdatePhysioDTO
{
    [Required]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "La contraseña debe tener entre 4 y 100 caracteres")]
    public string? Password { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Formato de correo electrónico no válido")]
    [StringLength(100, ErrorMessage = "El correo debe tener menos de 100 caracteres")]
    public string Email { get; set; } = string.Empty;
}