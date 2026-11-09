using System;
using System.Collections.Generic;
using System.Linq;
using CadCarsal;

namespace ClnCarsal
{
    // Una fila de la lista "citas por atender" de un paciente
    public class CitaPorAtenderItem
    {
        public int IdCita { get; set; }
        public DateTime FechaHora { get; set; }
        public string Doctor { get; set; }
        public string Estado { get; set; }
    }

    // Una fila del historial clínico de un paciente
    public class HistorialItem
    {
        public int IdHistorial { get; set; }
        public int? IdCita { get; set; }
        public DateTime FechaAtencion { get; set; }
        public string Doctor { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }
        public string Observaciones { get; set; }
    }

    public class HistorialCln
    {
        // Citas del paciente que todavía no se atendieron (Pendiente o Confirmada)
        public static List<CitaPorAtenderItem> ListarCitasPorAtender(int idPaciente)
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                return contexto.Cita
                    .Where(c => c.IdPaciente == idPaciente &&
                                (c.Estado == "Pendiente" || c.Estado == "Confirmada"))
                    .OrderBy(c => c.FechaHora)
                    .Select(c => new CitaPorAtenderItem
                    {
                        IdCita = c.IdCita,
                        FechaHora = c.FechaHora,
                        Doctor = c.Doctor.Nombres + " " + c.Doctor.Apellidos,   
                        Estado = c.Estado
                    })
                    .ToList();
            }
        }

        // Todo el historial de un paciente, de lo más reciente a lo más antiguo
        public static List<HistorialItem> ListarPorPaciente(int idPaciente)
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                return contexto.HistorialClinico
                    .Where(h => h.IdPaciente == idPaciente)
                    .OrderByDescending(h => h.FechaAtencion)
                    .Select(h => new HistorialItem
                    {
                        IdHistorial = h.IdHistorial,
                        IdCita = h.IdCita,
                        FechaAtencion = h.FechaAtencion,
                        Doctor = h.Cita.Doctor.Nombres + " " + h.Cita.Doctor.Apellidos,
                        Diagnostico = h.Diagnostico,
                        Tratamiento = h.Tratamiento,
                        Observaciones = h.Observaciones
                    })
                    .ToList();
            }
        }

        // Registra la atención de una cita y la marca como "Atendida"
        public static int Registrar(
            int idCita,
            string diagnostico,
            string tratamiento,
            string observaciones,
            int idUsuario)
        {
            diagnostico = (diagnostico ?? "").Trim();
            tratamiento = (tratamiento ?? "").Trim();
            observaciones = string.IsNullOrWhiteSpace(observaciones) ? null : observaciones.Trim();
            Validar(diagnostico, tratamiento, observaciones);

            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                var cita = contexto.Cita.Find(idCita);
                if (cita == null)
                    throw new Exception("La cita seleccionada no existe.");

                if (cita.Estado != "Pendiente" && cita.Estado != "Confirmada")
                    throw new Exception(
                        "Solo se puede atender una cita Pendiente o Confirmada. " +
                        "Estado actual: " + cita.Estado + ".");

                if (contexto.HistorialClinico.Any(h => h.IdCita == idCita))
                    throw new Exception("Esta cita ya tiene una atención registrada.");

                var historial = new HistorialClinico
                {
                    IdPaciente = cita.IdPaciente,
                    IdCita = cita.IdCita,
                    FechaAtencion = DateTime.Now,
                    Diagnostico = diagnostico,
                    Tratamiento = tratamiento,
                    Observaciones = observaciones,
                    IdUsuarioRegistro = idUsuario
                };
                contexto.HistorialClinico.Add(historial);

                // La cita pasa a "Atendida". Un solo SaveChanges guarda las dos cosas
                // juntas: o se guardan ambas o no se guarda ninguna.
                cita.Estado = "Atendida";
                cita.IdUsuarioModificacion = idUsuario;
                cita.FechaModificacion = DateTime.Now;

                contexto.SaveChanges();
                return historial.IdHistorial;
            }
        }

        // Corrige el diagnóstico, tratamiento u observaciones de un registro existente
        public static void Modificar(
            int idHistorial,
            string diagnostico,
            string tratamiento,
            string observaciones,
            int idUsuario)
        {
            diagnostico = (diagnostico ?? "").Trim();
            tratamiento = (tratamiento ?? "").Trim();
            observaciones = string.IsNullOrWhiteSpace(observaciones) ? null : observaciones.Trim();
            Validar(diagnostico, tratamiento, observaciones);

            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                var historial = contexto.HistorialClinico.Find(idHistorial);
                if (historial == null)
                    throw new Exception("El registro del historial no existe.");

                historial.Diagnostico = diagnostico;
                historial.Tratamiento = tratamiento;
                historial.Observaciones = observaciones;
                historial.IdUsuarioModificacion = idUsuario;
                historial.FechaModificacion = DateTime.Now;

                contexto.SaveChanges();
            }
        }

        private static void Validar(string diagnostico, string tratamiento, string observaciones)
        {
            if (diagnostico.Length == 0)
                throw new Exception("Ingrese el diagnóstico.");
            if (diagnostico.Length > 500)
                throw new Exception("El diagnóstico no puede pasar de 500 caracteres.");

            if (tratamiento.Length == 0)
                throw new Exception("Ingrese el tratamiento realizado.");
            if (tratamiento.Length > 500)
                throw new Exception("El tratamiento no puede pasar de 500 caracteres.");

            if (observaciones != null && observaciones.Length > 1000)
                throw new Exception("Las observaciones no pueden pasar de 1000 caracteres.");
        }
    }
}