using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace WebCarsal.Models
{
    public class PacienteFormViewModel : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El CI es obligatorio.")]
        [StringLength(20)]
        [Display(Name = "Carnet de identidad")]
        public string Ci { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los nombres son obligatorios.")]
        [StringLength(100)]
        [Display(Name = "Nombres")]
        public string Nombres { get; set; } = string.Empty;

        [StringLength(100)]
        [Display(Name = "Primer apellido")]
        public string? PrimerApellido { get; set; }

        [StringLength(100)]
        [Display(Name = "Segundo apellido")]
        public string? SegundoApellido { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        public string? FechaNacimientoTexto { get; set; }

        [StringLength(20)]
        [Display(Name = "Sexo")]
        public string? Sexo { get; set; }

        [StringLength(20)]
        [Display(Name = "Celular de referencia")]
        public string? Celular { get; set; }

        [StringLength(250)]
        [Display(Name = "Dirección")]
        public string? Direccion { get; set; }

        public IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(PrimerApellido) &&
                string.IsNullOrWhiteSpace(SegundoApellido))
            {
                yield return new ValidationResult(
                    "Debe ingresar al menos un apellido.",
                    new[] { nameof(PrimerApellido), nameof(SegundoApellido) });
            }

            if (!string.IsNullOrWhiteSpace(FechaNacimientoTexto))
            {
                bool fechaValida = DateOnly.TryParseExact(
                    FechaNacimientoTexto,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateOnly fecha
                );

                if (!fechaValida)
                {
                    yield return new ValidationResult(
                        "Ingrese una fecha válida en formato día/mes/año.",
                        new[] { nameof(FechaNacimientoTexto) });
                }
                else
                {
                    DateOnly hoy = DateOnly.FromDateTime(DateTime.Today);
                    DateOnly fechaLimite = DateOnly.FromDateTime(
                        DateTime.Today.AddYears(-5));

                    if (fecha > hoy)
                    {
                        yield return new ValidationResult(
                            "La fecha de nacimiento no puede ser futura.",
                            new[] { nameof(FechaNacimientoTexto) });
                    }
                    else if (fecha > fechaLimite)
                    {
                        yield return new ValidationResult(
                            "El paciente debe tener como mínimo 5 años cumplidos.",
                            new[] { nameof(FechaNacimientoTexto) });
                    }
                }
            }
        }

        public DateOnly? ObtenerFechaNacimiento()
        {
            if (string.IsNullOrWhiteSpace(FechaNacimientoTexto))
                return null;

            if (DateOnly.TryParseExact(
                FechaNacimientoTexto,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateOnly fecha))
            {
                return fecha;
            }

            return null;
        }
    }
}