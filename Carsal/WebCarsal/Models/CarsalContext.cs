using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebCarsal.Models;

public partial class CarsalContext : DbContext
{
    public CarsalContext()
    {
    }

    public CarsalContext(DbContextOptions<CarsalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cita> Cita { get; set; }

    public virtual DbSet<DetallePago> DetallePago { get; set; }

    public virtual DbSet<Doctor> Doctor { get; set; }

    public virtual DbSet<Especialidad> Especialidad { get; set; }

    public virtual DbSet<HistorialClinico> HistorialClinico { get; set; }

    public virtual DbSet<Paciente> Paciente { get; set; }

    public virtual DbSet<Pago> Pago { get; set; }

    public virtual DbSet<Servicio> Servicio { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=LabConsultorioDental_Carsal;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cita__394B0202CE1F59A3");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Pendiente")
                .HasColumnName("estado");
            entity.Property(e => e.FechaHora)
                .HasColumnType("datetime")
                .HasColumnName("fechaHora");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdDoctor).HasColumnName("idDoctor");
            entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");
            entity.Property(e => e.IdUsuarioModificacion).HasColumnName("idUsuarioModificacion");
            entity.Property(e => e.IdUsuarioRegistro).HasColumnName("idUsuarioRegistro");

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Cita_Doctor");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Cita_Paciente");

            entity.HasOne(d => d.IdUsuarioModificacionNavigation).WithMany(p => p.CitaIdUsuarioModificacionNavigation)
                .HasForeignKey(d => d.IdUsuarioModificacion)
                .HasConstraintName("fk_Cita_UsuarioModificacion");

            entity.HasOne(d => d.IdUsuarioRegistroNavigation).WithMany(p => p.CitaIdUsuarioRegistroNavigation)
                .HasForeignKey(d => d.IdUsuarioRegistro)
                .HasConstraintName("fk_Cita_UsuarioRegistro");
        });

        modelBuilder.Entity<DetallePago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DetalleP__9FD1381C68A7EEA1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdPago).HasColumnName("idPago");
            entity.Property(e => e.IdServicio).HasColumnName("idServicio");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precioUnitario");
            entity.Property(e => e.Subtotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("subtotal");

            entity.HasOne(d => d.IdPagoNavigation).WithMany(p => p.DetallePago)
                .HasForeignKey(d => d.IdPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_DetallePago_Pago");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.DetallePago)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_DetallePago_Servicio");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Doctor__F838DB3E987117F7");

            entity.HasIndex(e => e.Ci, "UQ__Doctor__32149A5A3DBEA9F3").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("apellidos");
            entity.Property(e => e.Celular)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("celular");
            entity.Property(e => e.Ci)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ci");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdEspecialidad).HasColumnName("idEspecialidad");
            entity.Property(e => e.IdUsuarioModificacion).HasColumnName("idUsuarioModificacion");
            entity.Property(e => e.IdUsuarioRegistro).HasColumnName("idUsuarioRegistro");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombres");

            entity.HasOne(d => d.IdEspecialidadNavigation).WithMany(p => p.Doctor)
                .HasForeignKey(d => d.IdEspecialidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Doctor_Especialidad");

            entity.HasOne(d => d.IdUsuarioModificacionNavigation).WithMany(p => p.DoctorIdUsuarioModificacionNavigation)
                .HasForeignKey(d => d.IdUsuarioModificacion)
                .HasConstraintName("fk_Doctor_UsuarioModificacion");

            entity.HasOne(d => d.IdUsuarioRegistroNavigation).WithMany(p => p.DoctorIdUsuarioRegistroNavigation)
                .HasForeignKey(d => d.IdUsuarioRegistro)
                .HasConstraintName("fk_Doctor_UsuarioRegistro");
        });

        modelBuilder.Entity<Especialidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Especial__693FA0AF598C538A");

            entity.HasIndex(e => e.Nombre, "UQ__Especial__75E3EFCF20716B85").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdUsuarioModificacion).HasColumnName("idUsuarioModificacion");
            entity.Property(e => e.IdUsuarioRegistro).HasColumnName("idUsuarioRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdUsuarioModificacionNavigation).WithMany(p => p.EspecialidadIdUsuarioModificacionNavigation)
                .HasForeignKey(d => d.IdUsuarioModificacion)
                .HasConstraintName("fk_Especialidad_UsuarioModificacion");

            entity.HasOne(d => d.IdUsuarioRegistroNavigation).WithMany(p => p.EspecialidadIdUsuarioRegistroNavigation)
                .HasForeignKey(d => d.IdUsuarioRegistro)
                .HasConstraintName("fk_Especialidad_UsuarioRegistro");
        });

        modelBuilder.Entity<HistorialClinico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Historia__9CC7DBB4B701BC8A");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Diagnostico)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("diagnostico");
            entity.Property(e => e.FechaAtencion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaAtencion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.IdCita).HasColumnName("idCita");
            entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");
            entity.Property(e => e.IdUsuarioModificacion).HasColumnName("idUsuarioModificacion");
            entity.Property(e => e.IdUsuarioRegistro).HasColumnName("idUsuarioRegistro");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("observaciones");
            entity.Property(e => e.Tratamiento)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("tratamiento");

            entity.HasOne(d => d.IdCitaNavigation).WithMany(p => p.HistorialClinico)
                .HasForeignKey(d => d.IdCita)
                .HasConstraintName("fk_HistorialClinico_Cita");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.HistorialClinico)
                .HasForeignKey(d => d.IdPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_HistorialClinico_Paciente");

            entity.HasOne(d => d.IdUsuarioModificacionNavigation).WithMany(p => p.HistorialClinicoIdUsuarioModificacionNavigation)
                .HasForeignKey(d => d.IdUsuarioModificacion)
                .HasConstraintName("fk_HistorialClinico_UsuarioModificacion");

            entity.HasOne(d => d.IdUsuarioRegistroNavigation).WithMany(p => p.HistorialClinicoIdUsuarioRegistroNavigation)
                .HasForeignKey(d => d.IdUsuarioRegistro)
                .HasConstraintName("fk_HistorialClinico_UsuarioRegistro");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Paciente__C93DB49B0DCDA1C5");

            entity.HasIndex(e => e.Ci, "UQ__Paciente__32149A5A958D83DA").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Celular)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("celular");
            entity.Property(e => e.Ci)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ci");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fechaNacimiento");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdUsuarioModificacion).HasColumnName("idUsuarioModificacion");
            entity.Property(e => e.IdUsuarioRegistro).HasColumnName("idUsuarioRegistro");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombres");
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("primerApellido");
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("segundoApellido");
            entity.Property(e => e.Sexo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("sexo");

            entity.HasOne(d => d.IdUsuarioModificacionNavigation).WithMany(p => p.PacienteIdUsuarioModificacionNavigation)
                .HasForeignKey(d => d.IdUsuarioModificacion)
                .HasConstraintName("fk_Paciente_UsuarioModificacion");

            entity.HasOne(d => d.IdUsuarioRegistroNavigation).WithMany(p => p.PacienteIdUsuarioRegistroNavigation)
                .HasForeignKey(d => d.IdUsuarioRegistro)
                .HasConstraintName("fk_Paciente_UsuarioRegistro");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pago__FC851A3A3A42E32A");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Concepto)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("concepto");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Pendiente")
                .HasColumnName("estado");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.FechaPago)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaPago");
            entity.Property(e => e.IdCita).HasColumnName("idCita");
            entity.Property(e => e.IdUsuarioModificacion).HasColumnName("idUsuarioModificacion");
            entity.Property(e => e.IdUsuarioRegistro).HasColumnName("idUsuarioRegistro");
            entity.Property(e => e.MetodoPago)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("metodoPago");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto");

            entity.HasOne(d => d.IdCitaNavigation).WithMany(p => p.Pago)
                .HasForeignKey(d => d.IdCita)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Pago_Cita");

            entity.HasOne(d => d.IdUsuarioModificacionNavigation).WithMany(p => p.PagoIdUsuarioModificacionNavigation)
                .HasForeignKey(d => d.IdUsuarioModificacion)
                .HasConstraintName("fk_Pago_UsuarioModificacion");

            entity.HasOne(d => d.IdUsuarioRegistroNavigation).WithMany(p => p.PagoIdUsuarioRegistroNavigation)
                .HasForeignKey(d => d.IdUsuarioRegistro)
                .HasConstraintName("fk_Pago_UsuarioRegistro");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Servicio__2DCCF9A207AEE0DE");

            entity.HasIndex(e => e.Nombre, "UQ__Servicio__75E3EFCFFE2FBA9C").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdUsuarioModificacion).HasColumnName("idUsuarioModificacion");
            entity.Property(e => e.IdUsuarioRegistro).HasColumnName("idUsuarioRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");

            entity.HasOne(d => d.IdUsuarioModificacionNavigation).WithMany(p => p.ServicioIdUsuarioModificacionNavigation)
                .HasForeignKey(d => d.IdUsuarioModificacion)
                .HasConstraintName("fk_Servicio_UsuarioModificacion");

            entity.HasOne(d => d.IdUsuarioRegistroNavigation).WithMany(p => p.ServicioIdUsuarioRegistroNavigation)
                .HasForeignKey(d => d.IdUsuarioRegistro)
                .HasConstraintName("fk_Servicio_UsuarioRegistro");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__5B65BF975059F2E0");

            entity.HasIndex(e => e.NombreUsuario, "UQ__Usuario__6B0F5AE0CAC90DA0").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClaveHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("claveHash");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.IdDoctor).HasColumnName("idDoctor");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreUsuario");
            entity.Property(e => e.Rol)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("rol");

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.IdDoctor)
                .HasConstraintName("fk_Usuario_Doctor");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
