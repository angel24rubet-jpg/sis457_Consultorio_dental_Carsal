
using System.ComponentModel.DataAnnotations;

namespace WebCarsal.Models
{
    public class PagoFormViewModel
    {
        [Required(ErrorMessage = "Ingrese el CI del paciente.")]
        [Display(Name = "Carnet de identidad")]
        public string Ci { get; set; } = string.Empty;

        public int IdPaciente { get; set; }

        [Required(ErrorMessage = "Seleccione una cita atendida.")]
        [Display(Name = "Cita")]
        public int? IdCita { get; set; }

        [Required(ErrorMessage = "Seleccione un método de pago.")]
        [Display(Name = "Método de pago")]
        public string MetodoPago { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        [Display(Name = "Efectivo recibido")]
        public decimal EfectivoRecibido { get; set; }

        public List<DetallePagoFormViewModel> Detalles { get; set; } = new();

        public decimal Total => Detalles.Sum(d => d.Subtotal);

        public decimal Cambio => Math.Max(0, EfectivoRecibido - Total);
    }

    public class DetallePagoFormViewModel
    {
        public int IdServicio { get; set; }

        public string NombreServicio { get; set; } = string.Empty;

        public decimal PrecioUnitario { get; set; }

        [Range(1, 100, ErrorMessage = "La cantidad debe ser mayor a cero.")]
        public int Cantidad { get; set; } = 1;

        public decimal Subtotal => PrecioUnitario * Cantidad;
    }
}
