
using System.ComponentModel.DataAnnotations;

namespace WebCarsal.Models
{
    public class PacienteFormViewModel
    {
        [Required(ErrorMessage = "El CI es obligatorio.")]
        [StringLength(20, ErrorMessage = "El CI no puede superar los 20 caracteres.")]
        [Display(Name = "Carnet de identidad")]
        public string Ci { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los nombres son obligatorios.")]
        [StringLength(100)]
        [Display(Name = "Nombres")]
        public string Nombres { get; set; } = string.Empty;

        [StringLength(100)]
        [Display(Name = "Primer apellido")]
        public string? PrimerApellido { get; set; }

        [StringLength(100)]
        [Display(Name = "Segundo apellido")]
        public string? SegundoApellido { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        public DateOnly? FechaNacimiento { get; set; }

        [Display(Name = "Sexo")]
        public string? Sexo { get; set; }

        [StringLength(20)]
        [Display(Name = "Celular")]
        public string? Celular { get; set; }

        [StringLength(250)]
        [Display(Name = "Dirección")]
        public string? Direccion { get; set; }
    }
}
