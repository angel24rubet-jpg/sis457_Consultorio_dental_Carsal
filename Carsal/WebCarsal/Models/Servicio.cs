using System;
using System.Collections.Generic;

namespace WebCarsal.Models;

public partial class Servicio
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public bool Estado { get; set; }

    public int? IdUsuarioRegistro { get; set; }

    public DateTime FechaRegistro { get; set; }

    public int? IdUsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual ICollection<DetallePago> DetallePago { get; set; } = new List<DetallePago>();

    public virtual Usuario? IdUsuarioModificacionNavigation { get; set; }

    public virtual Usuario? IdUsuarioRegistroNavigation { get; set; }
}
