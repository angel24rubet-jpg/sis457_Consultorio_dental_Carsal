
using System.Linq;
using CadCarsal;

namespace ClnCarsal
{
    public class DoctorCln
    {
        public static object Listar()
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                return contexto.Doctor
                    .Where(d => d.Estado == true)
                    .Select(d => new
                    {
                        d.IdDoctor,
                        d.Ci,
                        d.Nombres,
                        d.Apellidos,
                        d.Celular,
                        d.Especialidad,
                        d.Direccion
                    })
                    .ToList();
            }
        }

        public static void Insertar(
            string ci,
            string nombres,
            string primerApellido,
            string segundoApellido,
            string celular,
            string especialidad,
            string direccion,
            int idUsuarioRegistro)
        {
            string apellidos = (
                (primerApellido ?? "").Trim() + " " +
                (segundoApellido ?? "").Trim()
            ).Trim();

            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                contexto.paDoctorInsertar(
                    ci,
                    nombres,
                    apellidos,
                    celular,
                    especialidad,
                    direccion,
                    idUsuarioRegistro
                );
            }
        }


        public static void Modificar(
            int idDoctor,
            string ci,
            string nombres,
            string primerApellido,
            string segundoApellido,
            string celular,
            string especialidad,
            string direccion,
            int idUsuarioModificacion)
        {
            string apellidos = (
                (primerApellido ?? "").Trim() + " " +
                (segundoApellido ?? "").Trim()
            ).Trim();

            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                contexto.paDoctorModificar(
                    idDoctor,
                    ci,
                    nombres,
                    apellidos,
                    celular,
                    especialidad,
                    direccion,
                    idUsuarioModificacion
                );
            }
        }


        public static void Eliminar(
            int idDoctor,
            int idUsuarioModificacion)
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                contexto.paDoctorEliminar(
                    idDoctor,
                    idUsuarioModificacion
                );
            }
        }

        public static object BuscarPorCi(string ci)
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                return contexto.paDoctorBuscarPorCi(ci).ToList();
            }
        }


    }
}
