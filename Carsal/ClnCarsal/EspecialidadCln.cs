using System.Linq;
using CadCarsal;

namespace ClnCarsal
{
    public class EspecialidadCln
    {
        // Especialidades activas, ordenadas por nombre (para el desplegable)
        public static object Listar()
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                return contexto.Especialidad
                    .Where(e => e.Estado == true)
                    .OrderBy(e => e.Nombre)
                    .Select(e => new
                    {
                        e.IdEspecialidad,
                        e.Nombre
                    })
                    .ToList();
            }
        }
    }
}