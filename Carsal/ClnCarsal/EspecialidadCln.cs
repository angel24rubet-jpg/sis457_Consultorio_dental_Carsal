
using System;
using System.Linq;
using CadCarsal;

namespace ClnCarsal
{
    public class EspecialidadCln
    {
        // Listar especialidades activas para el formulario de doctores
        public static object Listar()
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                return contexto.Especialidad
                    .Where(e => e.estado == true)
                    .OrderBy(e => e.nombre)
                    .Select(e => new
                    {
                        IdEspecialidad = e.id,
                        Nombre = e.nombre
                    })
                    .ToList();
            }
        }

        // Listar todas las especialidades para gestion

        public static object ListarTodos()
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                return contexto.paEspecialidadListar()
                    .ToList()
                    .Select(e => new
                    {
                        e.IdEspecialidad,
                        e.Nombre,
                        e.Descripcion,
                        Estado = e.Estado ? "Activo" : "Inactivo",
                        e.FechaRegistro
                    })
                    .ToList();
            }
        }


        // Registrar especialidad
        public static void Insertar(
            string nombre,
            string descripcion,
            int idUsuarioRegistro)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("El nombre de la especialidad es obligatorio.");

            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                contexto.paEspecialidadInsertar(
                    nombre.Trim(),
                    string.IsNullOrWhiteSpace(descripcion)
                        ? null : descripcion.Trim(),
                    idUsuarioRegistro
                );
            }
        }

        // Modificar especialidad
        public static void Modificar(
            int idEspecialidad,
            string nombre,
            string descripcion,
            int idUsuarioModificacion)
        {
            if (idEspecialidad <= 0)
                throw new Exception("Seleccione una especialidad válida.");

            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("El nombre de la especialidad es obligatorio.");

            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                contexto.paEspecialidadModificar(
                    idEspecialidad,
                    nombre.Trim(),
                    string.IsNullOrWhiteSpace(descripcion)
                        ? null : descripcion.Trim(),
                    idUsuarioModificacion
                );
            }
        }

        // Desactivar especialidad
        public static void Eliminar(
            int idEspecialidad,
            int idUsuarioModificacion)
        {
            if (idEspecialidad <= 0)
                throw new Exception("Seleccione una especialidad válida.");

            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                contexto.paEspecialidadEliminar(
                    idEspecialidad,
                    idUsuarioModificacion
                );
            }
        }
    }
}
