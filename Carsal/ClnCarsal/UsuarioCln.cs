
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
                    u => u.nombreUsuario == nombreUsuario);

                if (existe)
                    throw new Exception(
                        "Ese nombre de usuario ya está registrado.");

                var usuario = new Usuario
                {
                    nombreUsuario = nombreUsuario,
                    claveHash = hash,
                    rol = rol,
                    idDoctor = idDoctor,
                    estado = true
                };

                contexto.Usuario.Add(usuario);
                contexto.SaveChanges();

                return usuario.id;
            }
        }

        public static int? ObtenerIdUsuarioActivo()
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                return contexto.Usuario
                    .Where(u => u.estado == true)
                    .OrderBy(u => u.id)
                    .Select(u => (int?)u.id)
                    .FirstOrDefault();
            }
        }

        public static bool ValidarAcceso(
            string nombreUsuario, string contraseña)
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                var usuario = contexto.Usuario.FirstOrDefault(
                    u => u.nombreUsuario == nombreUsuario &&
                         u.estado == true);

                if (usuario == null)
                    return false;

                return SeguridadCln.VerificarContraseña(
                    contraseña, usuario.claveHash);
            }
        }
        public static Usuario ObtenerPorNombre(string nombreUsuario)
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                return contexto.Usuario.FirstOrDefault(
                    u => u.nombreUsuario == nombreUsuario &&
                         u.estado == true);
            }
        }


        public static object Listar()
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                return (
                    from u in contexto.Usuario
                    join d in contexto.Doctor
                        on u.idDoctor equals (int?)d.id
                        into doctores
                    from d in doctores.DefaultIfEmpty()
                    select new
                    {
                        IdUsuario = u.id,
                        NombreUsuario = u.nombreUsuario,
                        Rol = u.rol,
                        IdDoctor = u.idDoctor,
                        Doctor = d == null
                            ? ""
                            : d.nombres + " " + d.apellidos,
                        Estado = u.estado
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
                    .FirstOrDefault(u => u.id == idUsuario);

                if (usuario == null)
                    throw new Exception("No se encontró el usuario.");

                bool existe = contexto.Usuario.Any(
                    u => u.nombreUsuario == nombreUsuario &&
                         u.id != idUsuario);

                if (existe)
                    throw new Exception("Ese nombre de usuario ya está registrado.");

                if (string.IsNullOrWhiteSpace(nombreUsuario))
                    throw new Exception("Ingrese el nombre de usuario.");

                if (rol != "Administrador" && rol != "Doctor")
                    throw new Exception("El rol no es válido.");

                usuario.nombreUsuario = nombreUsuario;
                usuario.rol = rol;
                usuario.idDoctor = idDoctor;

                // Solo cambiar la contraseña si se escribió una nueva
                if (!string.IsNullOrWhiteSpace(contraseña))
                {
                    if (contraseña.Length < 8)
                        throw new Exception(
                            "La contraseña debe tener al menos 8 caracteres.");

                    usuario.claveHash = SeguridadCln.GenerarHash(contraseña);
                }

                contexto.SaveChanges();
            }
        }

        public static void Eliminar(int idUsuario)
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                var usuario = contexto.Usuario
                    .FirstOrDefault(u => u.id == idUsuario);

                if (usuario == null)
                    throw new Exception("No se encontró el usuario.");

                usuario.estado = false;

                contexto.SaveChanges();
            }
        }

    }
}