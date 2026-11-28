using System;
using System.Collections.Generic;

namespace WebCarsal.Models;

public partial class Paciente
{
    public int Id { get; set; }

    public string Ci { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string? PrimerApellido { get; set; }

    public string? SegundoApellido { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public string? Sexo { get; set; }

    public string? Celular { get; set; }

    public string? Direccion { get; set; }

    public bool Estado { get; set; }

    public DateTime FechaRegistro { get; set; }

    public int? IdUsuarioRegistro { get; set; }

    public int? IdUsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual ICollection<HistorialClinico> HistorialClinico { get; set; } = new List<HistorialClinico>();

    public virtual Usuario? IdUsuarioModificacionNavigation { get; set; }

    public virtual Usuario? IdUsuarioRegistroNavigation { get; set; }
}
