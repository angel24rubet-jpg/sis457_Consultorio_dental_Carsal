
using System;
using System.Security.Cryptography;

namespace ClnCarsal
{
    public class SeguridadCln
    {
        public static string GenerarHash(string contraseña)
        {
            byte[] salt = new byte[16];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(
                contraseña, salt, 100000))
            {
                byte[] hash = pbkdf2.GetBytes(32);

                return Convert.ToBase64String(salt) + ":" +
                       Convert.ToBase64String(hash);
            }
        }

        public static bool VerificarContraseña(
            string contraseña, string hashGuardado)
        {
            if (string.IsNullOrWhiteSpace(contraseña) ||
                string.IsNullOrWhiteSpace(hashGuardado))
                return false;

            string[] partes = hashGuardado.Split(':');

            if (partes.Length != 2)
                return false;

            try
            {
                byte[] salt = Convert.FromBase64String(partes[0]);
                byte[] hashOriginal =
                    Convert.FromBase64String(partes[1]);

                using (var pbkdf2 = new Rfc2898DeriveBytes(
                    contraseña, salt, 100000))
                {
                    byte[] hashCalculado = pbkdf2.GetBytes(32);

                    int diferencia = hashOriginal.Length ^ hashCalculado.Length;

                    for (int i = 0; i < hashOriginal.Length &&
                                    i < hashCalculado.Length; i++)
                    {
                        diferencia |= hashOriginal[i] ^ hashCalculado[i];
                    }

                    return diferencia == 0;
                }
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
