using System;
using System.Collections.Generic;

namespace WebCarsal.Models;

public partial class DetallePago
{
    public int Id { get; set; }

    public int IdPago { get; set; }

    public int IdServicio { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal Subtotal { get; set; }

    public virtual Pago IdPagoNavigation { get; set; } = null!;

    public virtual Servicio IdServicioNavigation { get; set; } = null!;
}
