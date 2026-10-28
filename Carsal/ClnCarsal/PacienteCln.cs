using System.Collections.Generic;
using System.Linq;
using CadCarsal;

namespace ClnCarsal
{
    public class PacienteCln
    {
        // listar
        public static List<paPacienteListar_Result> Listar()
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                return contexto.paPacienteListar_Consultar().ToList();
            }
        }
        // buscar
        public static List<paPacienteBuscarPorCi_Result> BuscarPorCi(string ci)
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                return contexto.paPacienteBuscarPorCi_Consultar(ci).ToList();
            }
        }
        // Registrar paciente
        public static void Insertar(
            string ci,
            string nombres,
            string primerApellido,
            string segundoApellido,
            System.DateTime? fechaNacimiento,
            string sexo,
            string celular,
            string direccion,
            int idUsuarioRegistro)
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                contexto.paPacienteInsertar(
                    ci,
                    nombres,
                    primerApellido,
                    segundoApellido,
                    fechaNacimiento,
                    sexo,
                    celular,
                    direccion,
                    idUsuarioRegistro
                );
            }
        }
        // Modificar paciente
        public static void Modificar(
            int idPaciente,
            string ci,
            string nombres,
            string primerApellido,
            string segundoApellido,
            System.DateTime? fechaNacimiento,
            string sexo,
            string celular,
            string direccion,
            int idUsuarioModificacion)
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                contexto.paPacienteModificar(
                    idPaciente,
                    ci,
                    nombres,
                    primerApellido,
                    segundoApellido,
                    fechaNacimiento,
                    sexo,
                    celular,
                    direccion,
                    idUsuarioModificacion
                );
            }
        }
        // Eliminar paciente 
        public static void Eliminar(int idPaciente, int idUsuarioModificacion)
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                contexto.paPacienteEliminar(
                    idPaciente,
                    idUsuarioModificacion
                );
            }
        }
    }
}