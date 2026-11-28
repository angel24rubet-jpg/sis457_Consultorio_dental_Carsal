
using System.ComponentModel.DataAnnotations;

namespace WebCarsal.Models
{
    public class DoctorFormViewModel
    {
        [Required(ErrorMessage = "El CI es obligatorio.")]
        [StringLength(20)]
        [Display(Name = "Carnet de identidad")]
        public string Ci { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los nombres son obligatorios.")]
        [StringLength(100)]
        [Display(Name = "Nombres")]
        public string Nombres { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        [StringLength(150)]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; } = string.Empty;

        [StringLength(20)]
        [Display(Name = "Celular")]
        public string? Celular { get; set; }

        [StringLength(250)]
        [Display(Name = "Dirección")]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "Seleccione una especialidad.")]
        [Display(Name = "Especialidad")]
        public int? IdEspecialidad { get; set; }
    }
}
