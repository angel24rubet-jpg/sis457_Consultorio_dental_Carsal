using System;
using System.Collections.Generic;

namespace WebCarsal.Models;

public partial class HistorialClinico
{
    public int Id { get; set; }

    public int IdPaciente { get; set; }

    public int? IdCita { get; set; }

    public DateTime FechaAtencion { get; set; }

    public string Diagnostico { get; set; } = null!;

    public string Tratamiento { get; set; } = null!;

    public string? Observaciones { get; set; }

    public int? IdUsuarioRegistro { get; set; }

    public int? IdUsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual Cita? IdCitaNavigation { get; set; }

    public virtual Paciente IdPacienteNavigation { get; set; } = null!;

    public virtual Usuario? IdUsuarioModificacionNavigation { get; set; }

    public virtual Usuario? IdUsuarioRegistroNavigation { get; set; }
}
