using System;
using System.Collections.Generic;

namespace WebCarsal.Models;

public partial class Cita
{
    public int Id { get; set; }

    public int IdPaciente { get; set; }

    public int IdDoctor { get; set; }

    public DateTime FechaHora { get; set; }

    public string Estado { get; set; } = null!;

    public int? IdUsuarioRegistro { get; set; }

    public DateTime FechaRegistro { get; set; }

    public int? IdUsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual ICollection<HistorialClinico> HistorialClinico { get; set; } = new List<HistorialClinico>();

    public virtual Doctor IdDoctorNavigation { get; set; } = null!;

    public virtual Paciente IdPacienteNavigation { get; set; } = null!;

    public virtual Usuario? IdUsuarioModificacionNavigation { get; set; }

    public virtual Usuario? IdUsuarioRegistroNavigation { get; set; }

    public virtual ICollection<Pago> Pago { get; set; } = new List<Pago>();
}
