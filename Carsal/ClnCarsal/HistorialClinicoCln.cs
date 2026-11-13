using System;
using System.Collections.Generic;
using System.Linq;
using CadCarsal;

namespace ClnCarsal
{
    public class HistorialClinicoCln
    {
        // Listar todas las atenciones de un paciente
        public static List<paHistorialListarPorPaciente_Result> ListarPorPaciente(int idPaciente)
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                return contexto.paHistorialListarPorPaciente(idPaciente).ToList();
            }
        }

        // Citas del paciente que todavía se pueden marcar como atendidas
        public static List<paHistorialCitasDisponibles_Result> CitasDisponibles(int idPaciente)
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                return contexto.paHistorialCitasDisponibles(idPaciente).ToList();
            }
        }

        // Registrar una nueva atención en el historial
        public static decimal? Insertar(
            int idPaciente,
            int? idCita,
            string diagnostico,
            string tratamiento,
            string observaciones,
            int idUsuarioRegistro)
        {
            Validar(diagnostico, tratamiento, observaciones);

            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                return contexto.paHistorialInsertar(
                    idPaciente,
                    idCita,
                    diagnostico.Trim(),
                    tratamiento.Trim(),
                    string.IsNullOrWhiteSpace(observaciones) ? null : observaciones.Trim(),
                    idUsuarioRegistro
                ).FirstOrDefault();
            }
        }

        // Corregir una atención ya registrada
        public static void Modificar(
            int idHistorial,
            string diagnostico,
            string tratamiento,
            string observaciones,
            int idUsuarioModificacion)
        {
            Validar(diagnostico, tratamiento, observaciones);

            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                contexto.paHistorialModificar(
                    idHistorial,
                    diagnostico.Trim(),
                    tratamiento.Trim(),
                    string.IsNullOrWhiteSpace(observaciones) ? null : observaciones.Trim(),
                    idUsuarioModificacion
                );
            }
        }

        // Reglas de negocio comunes a registrar y modificar
        private static void Validar(string diagnostico, string tratamiento, string observaciones)
        {
            if (string.IsNullOrWhiteSpace(diagnostico))
                throw new Exception("Ingrese el diagnóstico.");

            if (string.IsNullOrWhiteSpace(tratamiento))
                throw new Exception("Ingrese el tratamiento realizado.");

            if (diagnostico.Trim().Length > 500)
                throw new Exception("El diagnóstico no puede superar los 500 caracteres.");

            if (tratamiento.Trim().Length > 500)
                throw new Exception("El tratamiento no puede superar los 500 caracteres.");

            if (!string.IsNullOrWhiteSpace(observaciones) && observaciones.Trim().Length > 1000)
                throw new Exception("Las observaciones no pueden superar los 1000 caracteres.");
        }
    }
}