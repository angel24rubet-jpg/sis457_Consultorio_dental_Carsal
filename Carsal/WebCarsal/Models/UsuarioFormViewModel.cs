using System.ComponentModel.DataAnnotations;

namespace WebCarsal.Models
{
    public class UsuarioFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre de usuario.")]
        [StringLength(50)]
        [Display(Name = "Nombre de usuario")]
        public string NombreUsuario { get; set; } = string.Empty;

        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string? Clave { get; set; }

        [Required(ErrorMessage = "Seleccione un rol.")]
        [Display(Name = "Rol")]
        public string Rol { get; set; } = string.Empty;

        [Display(Name = "Doctor asociado")]
        public int? IdDoctor { get; set; }
    }
}