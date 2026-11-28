
using System.ComponentModel.DataAnnotations;

namespace WebCarsal.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Ingrese su usuario.")]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingrese su contraseña.")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; } = string.Empty;
    }
}
