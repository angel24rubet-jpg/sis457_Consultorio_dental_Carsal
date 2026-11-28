
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCarsal.Models;

namespace WebCarsal.Controllers
{
    public class HistorialClinicoController : Controller
    {
        private readonly CarsalContext _context;

        public HistorialClinicoController(CarsalContext context)
        {
            _context = context;
        }

        // LISTADO DE HISTORIALES
        public async Task<IActionResult> Index()
        {
            var historiales = await _context.HistorialClinico
                .Include(h => h.IdPacienteNavigation)
                .Include(h => h.IdCitaNavigation)
                    .ThenInclude(c => c!.IdDoctorNavigation)
                .OrderByDescending(h => h.FechaAtencion)
                .ToListAsync();

            return View(historiales);
        }

        // DETALLES
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var historial = await _context.HistorialClinico
                .Include(h => h.IdPacienteNavigation)
                .Include(h => h.IdCitaNavigation)
                    .ThenInclude(c => c!.IdDoctorNavigation)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (historial == null)
                return NotFound();

            return View(historial);
        }

        // FORMULARIO DE REGISTRO
        [HttpGet]
        public IActionResult Create()
        {
            return View(new HistorialClinicoViewModel());
        }

        // BUSCAR PACIENTE Y SUS CITAS PENDIENTES POR CI
        [HttpGet]
        public async Task<IActionResult> BuscarPaciente(string ci)
        {
            if (string.IsNullOrWhiteSpace(ci))
            {
                return Json(new
                {
                    encontrado = false,
                    mensaje = "Ingrese el carnet de identidad del paciente."
                });
            }

            ci = ci.Trim();

            var paciente = await _context.Paciente
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Ci == ci && p.Estado);

            if (paciente == null)
            {
                return Json(new
                {
                    encontrado = false,
                    mensaje = "No se encontró un paciente activo con ese CI."
                });
            }

            var citas = await _context.Cita
                .AsNoTracking()
                .Where(c =>
                    c.IdPaciente == paciente.Id &&
                    c.Estado == "Pendiente" &&
                    !_context.HistorialClinico.Any(h => h.IdCita == c.Id))
                .Include(c => c.IdDoctorNavigation)
                    .ThenInclude(d => d.IdEspecialidadNavigation)
                .OrderBy(c => c.FechaHora)
                .Select(c => new
                {
                    id = c.Id,
                    fecha = c.FechaHora.ToString("dd/MM/yyyy"),
                    hora = c.FechaHora.ToString("HH:mm"),
                    doctor = c.IdDoctorNavigation.Nombres + " " +
                             c.IdDoctorNavigation.Apellidos,
                    especialidad = c.IdDoctorNavigation
                        .IdEspecialidadNavigation.Nombre
                })
                .ToListAsync();

            return Json(new
            {
                encontrado = true,
                paciente = new
                {
                    id = paciente.Id,
                    ci = paciente.Ci,
                    nombre = paciente.Nombres + " " +
                             (paciente.PrimerApellido ?? "") + " " +
                             (paciente.SegundoApellido ?? "")
                },
                citas = citas,
                mensaje = citas.Count == 0
                    ? "El paciente no tiene citas pendientes de atención."
                    : "Seleccione la cita que será atendida."
            });
        }

        // GUARDAR HISTORIAL Y MARCAR CITA COMO ATENDIDA
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HistorialClinicoViewModel modelo)
        {
            if (modelo.IdCita == null)
            {
                ModelState.AddModelError(
                    "IdCita",
                    "Debe seleccionar una cita pendiente.");
            }

            if (ModelState.IsValid)
            {
                var cita = await _context.Cita
                    .FirstOrDefaultAsync(c =>
                        c.Id == modelo.IdCita &&
                        c.IdPaciente == modelo.IdPaciente &&
                        c.Estado == "Pendiente");

                if (cita == null)
                {
                    ModelState.AddModelError(
                        "IdCita",
                        "La cita seleccionada no está disponible.");
                }
                else
                {
                    bool historialExistente = await _context.HistorialClinico
                        .AnyAsync(h => h.IdCita == cita.Id);

                    if (historialExistente)
                    {
                        ModelState.AddModelError(
                            "IdCita",
                            "Esta cita ya tiene un historial clínico registrado.");
                    }
                    else
                    {
                        var historial = new HistorialClinico
                        {
                            IdPaciente = modelo.IdPaciente,
                            IdCita = cita.Id,
                            FechaAtencion = DateTime.Now,
                            Diagnostico = modelo.Diagnostico.Trim(),
                            Tratamiento = modelo.Tratamiento.Trim(),
                            Observaciones = modelo.Observaciones?.Trim(),
                        };

                        cita.Estado = "Atendida";

                        _context.HistorialClinico.Add(historial);

                        // Ambos cambios se guardan en una misma operación.
                        await _context.SaveChangesAsync();

                        TempData["Mensaje"] =
                            "Historial clínico registrado y cita marcada como atendida.";

                        return RedirectToAction(nameof(Index));
                    }
                }
            }

            return View(modelo);
        }

        // EDITAR HISTORIAL
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var historial = await _context.HistorialClinico
                .Include(h => h.IdPacienteNavigation)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (historial == null)
                return NotFound();

            var modelo = new HistorialClinicoViewModel
            {
                Id = historial.Id,
                IdPaciente = historial.IdPaciente,
                IdCita = historial.IdCita,
                Ci = historial.IdPacienteNavigation.Ci,
                Diagnostico = historial.Diagnostico,
                Tratamiento = historial.Tratamiento,
                Observaciones = historial.Observaciones
            };

            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            HistorialClinicoViewModel modelo)
        {
            if (id != modelo.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(modelo);

            var historial = await _context.HistorialClinico
                .FirstOrDefaultAsync(h => h.Id == id);

            if (historial == null)
                return NotFound();

            // No se modifica el paciente ni la cita asociada.
            historial.Diagnostico = modelo.Diagnostico.Trim();
            historial.Tratamiento = modelo.Tratamiento.Trim();
            historial.Observaciones = modelo.Observaciones?.Trim();
            historial.FechaModificacion = DateTime.Now;

            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Historial clínico actualizado correctamente.";

            return RedirectToAction(nameof(Index));
        }
    }
}
