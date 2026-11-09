
using System;
using System.Linq;
using CadCarsal;

namespace ClnCarsal
{
    public class CitaCln
    {
        // Listar todas las citas ordenadas por proximidad
        public static object Listar()
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                return contexto.paCitaListar().ToList();
            }
        }

        // Buscar una cita por su identificador
        public static object BuscarPorId(int idCita)
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                return contexto.paCitaBuscarPorId(idCita).ToList();
            }
        }

        // Registrar una nueva cita
        public static decimal? Insertar(
            int idPaciente,
            int idDoctor,
            DateTime fechaHora,
            int idUsuarioRegistro)
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                return contexto.paCitaInsertar(
                    idPaciente,
                    idDoctor,
                    fechaHora,
                    idUsuarioRegistro
                ).FirstOrDefault();
            }
        }

        // Modificar una cita pendiente
        public static void Modificar(
            int idCita,
            int idDoctor,
            DateTime fechaHora,
            int idUsuarioModificacion)
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                contexto.paCitaModificar(
                    idCita,
                    idDoctor,
                    fechaHora,
                    idUsuarioModificacion
                );
            }
        }

        // Cancelar lógicamente una cita
        public static void Cancelar(
            int idCita,
            int idUsuarioModificacion)
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                contexto.paCitaCancelar(
                    idCita,
                    idUsuarioModificacion
                );
            }
        }

        // Consultar horarios ocupados de un doctor
        public static object HorariosOcupados(
            int idDoctor,
            DateTime fecha)
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                return contexto.paCitaHorariosOcupados(
                    idDoctor,
                    fecha
                ).ToList();
            }
        }

        // Listar citas de una fecha específica
        public static object ListarPorFecha(DateTime fecha)
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                return contexto.paCitaListarPorFecha(fecha).ToList();
            }
        }
    }
}
