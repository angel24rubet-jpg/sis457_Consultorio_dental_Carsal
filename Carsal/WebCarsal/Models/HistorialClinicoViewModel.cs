
using System.ComponentModel.DataAnnotations;

namespace WebCarsal.Models
{
    public class HistorialClinicoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese el CI del paciente.")]
        [Display(Name = "Carnet de identidad")]
        public string Ci { get; set; } = string.Empty;

        public int IdPaciente { get; set; }

        [Display(Name = "Cita")]
        public int? IdCita { get; set; }

        [Required(ErrorMessage = "El diagnóstico es obligatorio.")]
        [Display(Name = "Diagnóstico")]
        public string Diagnostico { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tratamiento es obligatorio.")]
        [Display(Name = "Tratamiento")]
        public string Tratamiento { get; set; } = string.Empty;

        [Display(Name = "Observaciones")]
        public string? Observaciones { get; set; }
    }
}
