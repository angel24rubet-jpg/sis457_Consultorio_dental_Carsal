using System;
using System.Collections.Generic;

namespace WebCarsal.Models;

public partial class Pago
{
    public int Id { get; set; }

    public int IdCita { get; set; }

    public DateTime FechaPago { get; set; }

    public decimal Monto { get; set; }

    public string MetodoPago { get; set; } = null!;

    public string Concepto { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public int? IdUsuarioRegistro { get; set; }

    public int? IdUsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual ICollection<DetallePago> DetallePago { get; set; } = new List<DetallePago>();

    public virtual Cita IdCitaNavigation { get; set; } = null!;

    public virtual Usuario? IdUsuarioModificacionNavigation { get; set; }

    public virtual Usuario? IdUsuarioRegistroNavigation { get; set; }
}
