
using System.ComponentModel.DataAnnotations;

namespace WebCarsal.Models
{
    public class CitaFormViewModel
    {
        [Required(ErrorMessage = "Seleccione un paciente.")]
        [Display(Name = "Paciente")]
        public int IdPaciente { get; set; }

        [Required(ErrorMessage = "Seleccione una especialidad.")]
        [Display(Name = "Especialidad")]
        public int IdEspecialidad { get; set; }

        [Required(ErrorMessage = "Seleccione un doctor.")]
        [Display(Name = "Doctor")]
        public int IdDoctor { get; set; }

        [Required(ErrorMessage = "Seleccione la fecha de la cita.")]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Seleccione la hora de la cita.")]
        [Display(Name = "Hora")]
        public string Hora { get; set; } = string.Empty;
    }
}
