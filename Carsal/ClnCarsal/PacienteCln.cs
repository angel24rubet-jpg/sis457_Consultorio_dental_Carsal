using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        // Buscar por CI, nombres o apellidos (búsqueda parcial, sin importar tildes ni mayúsculas)
        public static List<paPacienteListar_Result> Buscar(string texto)
        {
            var pacientes = Listar();

            if (string.IsNullOrWhiteSpace(texto))
                return pacientes;

            string buscado = Normalizar(texto);

            return pacientes.Where(p =>
                Normalizar(p.Ci).Contains(buscado) ||
                Normalizar(p.Nombres + " " + p.PrimerApellido + " " + p.SegundoApellido).Contains(buscado)
            ).ToList();
        }

        // Pasa a minúsculas y quita tildes: "María López" -> "maria lopez"
        private static string Normalizar(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return "";

            string descompuesto = texto.Trim().ToLower().Normalize(NormalizationForm.FormD);
            var resultado = new StringBuilder();
            foreach (char c in descompuesto)
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    resultado.Append(c);
            return resultado.ToString();
        }

        // Calcular la edad a partir de la fecha de nacimiento
        public static int? CalcularEdad(DateTime? fechaNacimiento)
        {
            if (!fechaNacimiento.HasValue)
                return null;

            DateTime hoy = DateTime.Today;
            int edad = hoy.Year - fechaNacimiento.Value.Year;
            if (fechaNacimiento.Value.Date > hoy.AddYears(-edad))
                edad--;
            return edad;
        }

        // ================= REGLAS DE NEGOCIO =================
        private static readonly Regex SoloLetras = new Regex(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$");

        private static void Validar(
            string ci,
            string nombres,
            string primerApellido,
            string segundoApellido,
            DateTime? fechaNacimiento,
            string sexo,
            string celular)
        {
            if (string.IsNullOrWhiteSpace(ci) || ci.Trim().Length < 5 || ci.Trim().Length > 15)
                throw new Exception("El CI debe tener entre 5 y 15 caracteres.");

            if (!char.IsDigit(ci.Trim()[0]))
                throw new Exception("El CI debe comenzar con un número.");

            if (string.IsNullOrWhiteSpace(nombres) || !SoloLetras.IsMatch(nombres.Trim()))
                throw new Exception("Los nombres solo pueden contener letras.");

            // Debe ingresar al menos uno de los dos apellidos
            if (string.IsNullOrWhiteSpace(primerApellido) &&
                string.IsNullOrWhiteSpace(segundoApellido))
                throw new Exception("Debe ingresar al menos un apellido.");

            // Validar primer apellido solo si fue ingresado
            if (!string.IsNullOrWhiteSpace(primerApellido) &&
                !SoloLetras.IsMatch(primerApellido.Trim()))
                throw new Exception("El primer apellido solo puede contener letras.");

            // Validar segundo apellido solo si fue ingresado
            if (!string.IsNullOrWhiteSpace(segundoApellido) &&
                !SoloLetras.IsMatch(segundoApellido.Trim()))
                throw new Exception("El segundo apellido solo puede contener letras.");

            if (sexo != "Masculino" && sexo != "Femenino")
                throw new Exception("Seleccione un sexo válido.");

            if (fechaNacimiento.HasValue)
            {
                if (fechaNacimiento.Value.Date > DateTime.Today)
                    throw new Exception("La fecha de nacimiento no puede ser futura.");

                if (CalcularEdad(fechaNacimiento) > 120)
                    throw new Exception("La fecha de nacimiento no es válida (más de 120 años).");

                if (CalcularEdad(fechaNacimiento) < 5)
                    throw new Exception("Solo se pueden registrar pacientes de 5 años o más.");
            }

            if (!string.IsNullOrWhiteSpace(celular))
            {
                string cel = celular.Trim();
                if (!cel.All(char.IsDigit) || cel.Length < 7 || cel.Length > 8)
                    throw new Exception("El celular debe tener 7 u 8 dígitos.");
            }
        }

        // Registrar paciente

        public static void Insertar(
            string ci,
            string nombres,
            string primerApellido,
            string segundoApellido,
            DateTime? fechaNacimiento,
            string sexo,
            string celular,
            string direccion,
            int idUsuarioRegistro)
        {
            primerApellido = string.IsNullOrWhiteSpace(primerApellido)
                ? null : primerApellido.Trim();

            segundoApellido = string.IsNullOrWhiteSpace(segundoApellido)
                ? null : segundoApellido.Trim();

            Validar(ci, nombres, primerApellido, segundoApellido,
                fechaNacimiento, sexo, celular);

            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                contexto.paPacienteInsertar(
                    ci.Trim(),
                    nombres.Trim(),
                    primerApellido,
                    segundoApellido,
                    fechaNacimiento,
                    sexo,
                    string.IsNullOrWhiteSpace(celular) ? null : celular.Trim(),
                    string.IsNullOrWhiteSpace(direccion) ? null : direccion.Trim(),
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
    DateTime? fechaNacimiento,
    string sexo,
    string celular,
    string direccion,
    int idUsuarioModificacion)
        {
            if (idPaciente <= 0)
                throw new Exception("Seleccione un paciente válido.");

            // Normalizar apellidos
            primerApellido = string.IsNullOrWhiteSpace(primerApellido)
                ? null : primerApellido.Trim();

            segundoApellido = string.IsNullOrWhiteSpace(segundoApellido)
                ? null : segundoApellido.Trim();

            // Validar datos
            Validar(ci, nombres, primerApellido, segundoApellido,
                fechaNacimiento, sexo, celular);

            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                contexto.paPacienteModificar(
                    idPaciente,
                    ci.Trim(),
                    nombres.Trim(),
                    primerApellido,
                    segundoApellido,
                    fechaNacimiento,
                    sexo,
                    string.IsNullOrWhiteSpace(celular) ? null : celular.Trim(),
                    string.IsNullOrWhiteSpace(direccion) ? null : direccion.Trim(),
                    idUsuarioModificacion
                );
            }
        }

        // Eliminar paciente (lógico)
        public static void Eliminar(int idPaciente, int idUsuarioModificacion)
        {
            if (idPaciente <= 0)
                throw new Exception("Seleccione un paciente válido.");

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