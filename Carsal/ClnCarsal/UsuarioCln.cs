
using System;
using System.Linq;
using CadCarsal;

namespace ClnCarsal
{
    public class UsuarioCln
    {
        public static int Registrar(
            string nombreUsuario,
            string contraseña,
            string rol,
            int? idDoctor)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new Exception("Ingrese el nombre de usuario.");

            if (string.IsNullOrWhiteSpace(contraseña) ||
                contraseña.Length < 8)
                throw new Exception(
                    "La contraseña debe tener al menos 8 caracteres.");

            if (rol != "Administrador" && rol != "Doctor")
                throw new Exception("El rol no es válido.");

            string hash = SeguridadCln.GenerarHash(contraseña);

            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                bool existe = contexto.Usuario.Any(
                    u => u.NombreUsuario == nombreUsuario);

                if (existe)
                    throw new Exception(
                        "Ese nombre de usuario ya está registrado.");

                var usuario = new Usuario
                {
                    NombreUsuario = nombreUsuario,
                    ClaveHash = hash,
                    Rol = rol,
                    IdDoctor = idDoctor,
                    Estado = true
                };

                contexto.Usuario.Add(usuario);
                contexto.SaveChanges();

                return usuario.IdUsuario;
            }
        }

        public static int? ObtenerIdUsuarioActivo()
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                return contexto.Usuario
                    .Where(u => u.Estado == true)
                    .OrderBy(u => u.IdUsuario)
                    .Select(u => (int?)u.IdUsuario)
                    .FirstOrDefault();
            }
        }

        public static bool ValidarAcceso(
            string nombreUsuario, string contraseña)
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                var usuario = contexto.Usuario.FirstOrDefault(
                    u => u.NombreUsuario == nombreUsuario &&
                         u.Estado == true);

                if (usuario == null)
                    return false;

                return SeguridadCln.VerificarContraseña(
                    contraseña, usuario.ClaveHash);
            }
        }
        public static Usuario ObtenerPorNombre(string nombreUsuario)
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                return contexto.Usuario.FirstOrDefault(
                    u => u.NombreUsuario == nombreUsuario &&
                         u.Estado == true);
            }
        }


        public static object Listar()
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                return (
                    from u in contexto.Usuario
                    join d in contexto.Doctor
                        on u.IdDoctor equals (int?)d.IdDoctor
                        into doctores
                    from d in doctores.DefaultIfEmpty()
                    select new
                    {
                        u.IdUsuario,
                        u.NombreUsuario,
                        u.Rol,
                        u.IdDoctor,
                        Doctor = d == null
                            ? ""
                            : d.Nombres + " " + d.Apellidos,
                        u.Estado
                    }
                ).ToList();
            }
        }
        public static void Modificar(
            int idUsuario,
            string nombreUsuario,
            string contraseña,
            string rol,
            int? idDoctor)
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                var usuario = contexto.Usuario
                    .FirstOrDefault(u => u.IdUsuario == idUsuario);

                if (usuario == null)
                    throw new Exception("No se encontró el usuario.");

                bool existe = contexto.Usuario.Any(
                    u => u.NombreUsuario == nombreUsuario &&
                         u.IdUsuario != idUsuario);

                if (existe)
                    throw new Exception("Ese nombre de usuario ya está registrado.");

                if (string.IsNullOrWhiteSpace(nombreUsuario))
                    throw new Exception("Ingrese el nombre de usuario.");

                if (rol != "Administrador" && rol != "Doctor")
                    throw new Exception("El rol no es válido.");

                usuario.NombreUsuario = nombreUsuario;
                usuario.Rol = rol;
                usuario.IdDoctor = idDoctor;

                // Solo cambiar la contraseña si se escribió una nueva
                if (!string.IsNullOrWhiteSpace(contraseña))
                {
                    if (contraseña.Length < 8)
                        throw new Exception(
                            "La contraseña debe tener al menos 8 caracteres.");

                    usuario.ClaveHash = SeguridadCln.GenerarHash(contraseña);
                }

                contexto.SaveChanges();
            }
        }

        public static void Eliminar(int idUsuario)
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                var usuario = contexto.Usuario
                    .FirstOrDefault(u => u.IdUsuario == idUsuario);

                if (usuario == null)
                    throw new Exception("No se encontró el usuario.");

                usuario.Estado = false;

                contexto.SaveChanges();
            }
        }

    }
}
