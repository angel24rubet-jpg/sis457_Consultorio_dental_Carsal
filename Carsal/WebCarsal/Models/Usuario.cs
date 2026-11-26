using System;
using System.Collections.Generic;

namespace WebCarsal.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string ClaveHash { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public int? IdDoctor { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<Cita> CitaIdUsuarioModificacionNavigation { get; set; } = new List<Cita>();

    public virtual ICollection<Cita> CitaIdUsuarioRegistroNavigation { get; set; } = new List<Cita>();

    public virtual ICollection<Doctor> DoctorIdUsuarioModificacionNavigation { get; set; } = new List<Doctor>();

    public virtual ICollection<Doctor> DoctorIdUsuarioRegistroNavigation { get; set; } = new List<Doctor>();

    public virtual ICollection<Especialidad> EspecialidadIdUsuarioModificacionNavigation { get; set; } = new List<Especialidad>();

    public virtual ICollection<Especialidad> EspecialidadIdUsuarioRegistroNavigation { get; set; } = new List<Especialidad>();

    public virtual ICollection<HistorialClinico> HistorialClinicoIdUsuarioModificacionNavigation { get; set; } = new List<HistorialClinico>();

    public virtual ICollection<HistorialClinico> HistorialClinicoIdUsuarioRegistroNavigation { get; set; } = new List<HistorialClinico>();

    public virtual Doctor? IdDoctorNavigation { get; set; }

    public virtual ICollection<Paciente> PacienteIdUsuarioModificacionNavigation { get; set; } = new List<Paciente>();

    public virtual ICollection<Paciente> PacienteIdUsuarioRegistroNavigation { get; set; } = new List<Paciente>();

    public virtual ICollection<Pago> PagoIdUsuarioModificacionNavigation { get; set; } = new List<Pago>();

    public virtual ICollection<Pago> PagoIdUsuarioRegistroNavigation { get; set; } = new List<Pago>();

    public virtual ICollection<Servicio> ServicioIdUsuarioModificacionNavigation { get; set; } = new List<Servicio>();

    public virtual ICollection<Servicio> ServicioIdUsuarioRegistroNavigation { get; set; } = new List<Servicio>();
}
