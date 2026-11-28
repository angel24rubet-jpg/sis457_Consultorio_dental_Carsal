using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCarsal.Models;

namespace WebCarsal.Controllers
{
    public class PacientesController : Controller
    {
        private readonly CarsalContext _context;

        public PacientesController(CarsalContext context)
        {
            _context = context;
        }

        // LISTAR LOS ULTIMOS 10 PACIENTES REGISTRADOS
        public async Task<IActionResult> Index(string? buscar)
        {
            var consulta = _context.Paciente
                .Where(p => p.Estado);

            if (!string.IsNullOrWhiteSpace(buscar))
            {
                buscar = buscar.Trim();

                consulta = consulta.Where(p =>
                    p.Ci.Contains(buscar) ||
                    p.Nombres.Contains(buscar) ||
                    (p.PrimerApellido != null && p.PrimerApellido.Contains(buscar)) ||
                    (p.SegundoApellido != null && p.SegundoApellido.Contains(buscar)));
            }

            ViewBag.Buscar = buscar;

            var pacientes = await consulta
                .OrderByDescending(p => p.FechaRegistro)
                .ThenByDescending(p => p.Id)
                .Take(10)
                .ToListAsync();

            return View(pacientes);
        }

        // MOSTRAR FORMULARIO DE REGISTRO
        [HttpGet]
        public IActionResult Create()
        {
            return PartialView(
                "~/Views/Pacientes/Create.cshtml",
                new PacienteFormViewModel()
            );
        }

        // REGISTRAR PACIENTE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PacienteFormViewModel modelo)
        {
            modelo.Ci = modelo.Ci?.Trim() ?? "";
            modelo.Nombres = modelo.Nombres?.Trim() ?? "";
            modelo.PrimerApellido = modelo.PrimerApellido?.Trim();
            modelo.SegundoApellido = modelo.SegundoApellido?.Trim();
            modelo.Celular = modelo.Celular?.Trim();
            modelo.Direccion = modelo.Direccion?.Trim();

            if (!string.IsNullOrWhiteSpace(modelo.Ci))
            {
                bool existe = await _context.Paciente
                    .AnyAsync(p => p.Ci == modelo.Ci);

                if (existe)
                {
                    ModelState.AddModelError(
                        nameof(modelo.Ci),
                        "Ya existe un paciente registrado con este CI."
                    );
                }
            }

            if (!ModelState.IsValid)
            {
                return PartialView(
                    "~/Views/Pacientes/Create.cshtml",
                    modelo
                );
            }

            var paciente = new Paciente
            {
                Ci = modelo.Ci,
                Nombres = modelo.Nombres,
                PrimerApellido = modelo.PrimerApellido,
                SegundoApellido = modelo.SegundoApellido,
                FechaNacimiento = modelo.ObtenerFechaNacimiento(),
                Sexo = modelo.Sexo,
                Celular = modelo.Celular,
                Direccion = modelo.Direccion,
                Estado = true,
                FechaRegistro = DateTime.Now
            };

            _context.Paciente.Add(paciente);
            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true,
                mensaje = "Paciente registrado correctamente."
            });
        }

        // MOSTRAR FORMULARIO DE EDICIÓN
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var paciente = await _context.Paciente
                .FirstOrDefaultAsync(p => p.Id == id && p.Estado);

            if (paciente == null)
                return NotFound();

            var modelo = new PacienteFormViewModel
            {
                Id = paciente.Id,
                Ci = paciente.Ci,
                Nombres = paciente.Nombres,
                PrimerApellido = paciente.PrimerApellido,
                SegundoApellido = paciente.SegundoApellido,
                FechaNacimientoTexto = paciente.FechaNacimiento?
                    .ToString("dd/MM/yyyy"),
                Sexo = paciente.Sexo,
                Celular = paciente.Celular,
                Direccion = paciente.Direccion
            };

            return PartialView(
                "~/Views/Pacientes/Edit.cshtml",
                modelo
            );
        }

        // ACTUALIZAR PACIENTE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            PacienteFormViewModel modelo)
        {
            if (id != modelo.Id)
                return NotFound();

            var paciente = await _context.Paciente
                .FirstOrDefaultAsync(p => p.Id == id && p.Estado);

            if (paciente == null)
                return NotFound();

            modelo.Ci = modelo.Ci?.Trim() ?? "";
            modelo.Nombres = modelo.Nombres?.Trim() ?? "";
            modelo.PrimerApellido = modelo.PrimerApellido?.Trim();
            modelo.SegundoApellido = modelo.SegundoApellido?.Trim();
            modelo.Celular = modelo.Celular?.Trim();
            modelo.Direccion = modelo.Direccion?.Trim();

            if (!string.IsNullOrWhiteSpace(modelo.Ci))
            {
                bool existe = await _context.Paciente
                    .AnyAsync(p => p.Ci == modelo.Ci && p.Id != id);

                if (existe)
                {
                    ModelState.AddModelError(
                        nameof(modelo.Ci),
                        "Este CI ya está registrado por otro paciente."
                    );
                }
            }

            if (!ModelState.IsValid)
            {
                return PartialView(
                    "~/Views/Pacientes/Edit.cshtml",
                    modelo
                );
            }

            paciente.Ci = modelo.Ci;
            paciente.Nombres = modelo.Nombres;
            paciente.PrimerApellido = modelo.PrimerApellido;
            paciente.SegundoApellido = modelo.SegundoApellido;
            paciente.FechaNacimiento = modelo.ObtenerFechaNacimiento();
            paciente.Sexo = modelo.Sexo;
            paciente.Celular = modelo.Celular;
            paciente.Direccion = modelo.Direccion;
            paciente.FechaModificacion = DateTime.Now;

            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true,
                mensaje = "Paciente actualizado correctamente."
            });
        }

        // DESACTIVAR PACIENTE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarEstado(int id)
        {
            var paciente = await _context.Paciente
                .FirstOrDefaultAsync(p => p.Id == id && p.Estado);

            if (paciente == null)
                return NotFound();

            paciente.Estado = false;
            paciente.FechaModificacion = DateTime.Now;

            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Paciente desactivado correctamente.";

            return RedirectToAction(nameof(Index));
        }
    }
}