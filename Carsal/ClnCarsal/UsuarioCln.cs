
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
    }
}
