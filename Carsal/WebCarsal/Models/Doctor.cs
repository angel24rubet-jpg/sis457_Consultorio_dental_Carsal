using System;
using System.Collections.Generic;

namespace WebCarsal.Models;

public partial class Doctor
{
    public int Id { get; set; }

    public string Ci { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string? Celular { get; set; }

    public string? Direccion { get; set; }

    public bool Estado { get; set; }

    public int? IdUsuarioRegistro { get; set; }

    public DateTime FechaRegistro { get; set; }

    public int? IdUsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int IdEspecialidad { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual Especialidad IdEspecialidadNavigation { get; set; } = null!;

    public virtual Usuario? IdUsuarioModificacionNavigation { get; set; }

    public virtual Usuario? IdUsuarioRegistroNavigation { get; set; }

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
