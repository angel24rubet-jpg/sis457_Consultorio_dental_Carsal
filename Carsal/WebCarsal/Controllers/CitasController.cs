
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebCarsal.Models;

namespace WebCarsal.Controllers
{
    public class CitasController : Controller
    {
        private readonly CarsalContext _context;

        public CitasController(CarsalContext context)
        {
            _context = context;
        }

        // LISTADO
        public async Task<IActionResult> Index()
        {
            var citas = await _context.Cita
                .Include(c => c.IdPacienteNavigation)
                .Include(c => c.IdDoctorNavigation)
                    .ThenInclude(d => d.IdEspecialidadNavigation)
                .OrderByDescending(c => c.FechaHora)
                .ToListAsync();

            return View(citas);
        }

        // DETALLES
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var cita = await _context.Cita
                .Include(c => c.IdPacienteNavigation)
                .Include(c => c.IdDoctorNavigation)
                    .ThenInclude(d => d.IdEspecialidadNavigation)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cita == null)
                return NotFound();

            return View(cita);
        }

        // REGISTRAR CITA
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await CargarListas();
            return View(new CitaFormViewModel
            {
                Fecha = DateTime.Today
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CitaFormViewModel modelo)
        {
            DateTime fechaHora;

            if (!ValidarFechaHora(modelo, out fechaHora))
                ModelState.AddModelError("Hora", "Seleccione una fecha y hora válidas.");

            if (ModelState.IsValid)
            {
                bool pacienteActivo = await _context.Paciente
                    .AnyAsync(p => p.Id == modelo.IdPaciente && p.Estado);

                bool doctorActivo = await _context.Doctor
                    .AnyAsync(d => d.Id == modelo.IdDoctor &&
                                   d.Estado &&
                                   d.IdEspecialidad == modelo.IdEspecialidad);

                bool especialidadActiva = await _context.Especialidad
                    .AnyAsync(e => e.Id == modelo.IdEspecialidad && e.Estado);

                if (!pacienteActivo)
                    ModelState.AddModelError("IdPaciente", "El paciente no está activo.");

                if (!especialidadActiva)
                    ModelState.AddModelError("IdEspecialidad", "La especialidad no está activa.");

                if (!doctorActivo)
                    ModelState.AddModelError("IdDoctor", "El doctor seleccionado no está disponible.");

                bool horarioOcupado = await _context.Cita.AnyAsync(c =>
                    c.IdDoctor == modelo.IdDoctor &&
                    c.FechaHora == fechaHora);

                if (horarioOcupado)
                    ModelState.AddModelError("Hora", "El doctor ya tiene una cita en ese horario.");
            }

            if (!ModelState.IsValid)
            {
                await CargarListas(
                    modelo.IdPaciente,
                    modelo.IdEspecialidad,
                    modelo.IdDoctor);

                return View(modelo);
            }

            var cita = new Cita
            {
                IdPaciente = modelo.IdPaciente,
                IdDoctor = modelo.IdDoctor,
                FechaHora = fechaHora,
                Estado = "Pendiente",
                FechaRegistro = DateTime.Now
            };

            _context.Cita.Add(cita);
            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Cita registrada correctamente.";

            return RedirectToAction(nameof(Index));
        }

        // EDITAR CITA
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var cita = await _context.Cita.FindAsync(id);

            if (cita == null)
                return NotFound();

            if (cita.Estado == "Atendida")
            {
                TempData["Error"] = "No se puede modificar una cita atendida.";
                return RedirectToAction(nameof(Index));
            }

            var modelo = new CitaFormViewModel
            {
                IdPaciente = cita.IdPaciente,
                IdDoctor = cita.IdDoctor,
                IdEspecialidad = await _context.Doctor
                    .Where(d => d.Id == cita.IdDoctor)
                    .Select(d => d.IdEspecialidad)
                    .FirstAsync(),
                Fecha = cita.FechaHora.Date,
                Hora = cita.FechaHora.ToString("HH:mm")
            };

            await CargarListas(
                modelo.IdPaciente,
                modelo.IdEspecialidad,
                modelo.IdDoctor);

            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CitaFormViewModel modelo)
        {
            var cita = await _context.Cita.FindAsync(id);

            if (cita == null)
                return NotFound();

            if (cita.Estado == "Atendida")
            {
                TempData["Error"] = "No se puede modificar una cita atendida.";
                return RedirectToAction(nameof(Index));
            }

            DateTime fechaHora;

            if (!ValidarFechaHora(modelo, out fechaHora))
                ModelState.AddModelError("Hora", "Seleccione una fecha y hora válidas.");

            if (ModelState.IsValid)
            {
                bool pacienteActivo = await _context.Paciente
                    .AnyAsync(p => p.Id == modelo.IdPaciente && p.Estado);

                bool doctorActivo = await _context.Doctor
                    .AnyAsync(d => d.Id == modelo.IdDoctor &&
                                   d.Estado &&
                                   d.IdEspecialidad == modelo.IdEspecialidad);

                bool especialidadActiva = await _context.Especialidad
                    .AnyAsync(e => e.Id == modelo.IdEspecialidad && e.Estado);

                if (!pacienteActivo)
                    ModelState.AddModelError("IdPaciente", "El paciente no está activo.");

                if (!especialidadActiva)
                    ModelState.AddModelError("IdEspecialidad", "La especialidad no está activa.");

                if (!doctorActivo)
                    ModelState.AddModelError("IdDoctor", "El doctor seleccionado no está disponible.");

                bool horarioOcupado = await _context.Cita.AnyAsync(c =>
                    c.Id != id &&
                    c.IdDoctor == modelo.IdDoctor &&
                    c.FechaHora == fechaHora);

                if (horarioOcupado)
                    ModelState.AddModelError("Hora", "El doctor ya tiene una cita en ese horario.");
            }

            if (!ModelState.IsValid)
            {
                await CargarListas(
                    modelo.IdPaciente,
                    modelo.IdEspecialidad,
                    modelo.IdDoctor);

                return View(modelo);
            }

            cita.IdPaciente = modelo.IdPaciente;
            cita.IdDoctor = modelo.IdDoctor;
            cita.FechaHora = fechaHora;
            cita.IdUsuarioModificacion = null;
            cita.FechaModificacion = DateTime.Now;

            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Cita modificada correctamente.";

            return RedirectToAction(nameof(Index));
        }

        // OBTENER DOCTORES SEGUN ESPECIALIDAD
        [HttpGet]
        public async Task<IActionResult> ObtenerDoctores(int idEspecialidad)
        {
            var doctores = await _context.Doctor
                .Where(d => d.Estado && d.IdEspecialidad == idEspecialidad)
                .OrderBy(d => d.Apellidos)
                .ThenBy(d => d.Nombres)
                .Select(d => new
                {
                    id = d.Id,
                    nombre = d.Nombres + " " + d.Apellidos
                })
                .ToListAsync();

            return Json(doctores);
        }

        // OBTENER HORARIOS DISPONIBLES
        [HttpGet]
        public async Task<IActionResult> ObtenerHorarios(
            int idDoctor,
            DateTime fecha,
            int? idCita)
        {
            var inicio = fecha.Date;
            var fin = inicio.AddDays(1);

            var ocupados = await _context.Cita
                .Where(c =>
                    c.IdDoctor == idDoctor &&
                    c.FechaHora >= inicio &&
                    c.FechaHora < fin &&
                    (!idCita.HasValue || c.Id != idCita.Value))
                .Select(c => c.FechaHora)
                .ToListAsync();

            var horarios = new List<string>();

            for (int hora = 8; hora <= 19; hora++)
            {
                for (int minuto = 0; minuto < 60; minuto += 30)
                {
                    if (hora == 19 && minuto > 30)
                        continue;

                    var horario = inicio.AddHours(hora).AddMinutes(minuto);

                    if (horario <= DateTime.Now)
                        continue;

                    if (!ocupados.Contains(horario))
                        horarios.Add(horario.ToString("HH:mm"));
                }
            }

            return Json(horarios);
        }

        // CARGAR LISTAS PARA LOS FORMULARIOS
        private async Task CargarListas(
            int? idPaciente = null,
            int? idEspecialidad = null,
            int? idDoctor = null)
        {
            var pacientes = await _context.Paciente
                .Where(p => p.Estado)
                .OrderBy(p => p.Nombres)
                .Select(p => new
                {
                    p.Id,
                    Nombre = p.Nombres + " " +
                             (p.PrimerApellido ?? "") + " " +
                             (p.SegundoApellido ?? "")
                })
                .ToListAsync();

            var especialidades = await _context.Especialidad
                .Where(e => e.Estado)
                .OrderBy(e => e.Nombre)
                .ToListAsync();

            var doctores = await _context.Doctor
                .Where(d => d.Estado &&
                            (!idEspecialidad.HasValue ||
                             d.IdEspecialidad == idEspecialidad.Value))
                .OrderBy(d => d.Apellidos)
                .Select(d => new
                {
                    d.Id,
                    Nombre = d.Nombres + " " + d.Apellidos
                })
                .ToListAsync();

            ViewBag.Pacientes = new SelectList(
                pacientes, "Id", "Nombre", idPaciente);

            ViewBag.Especialidades = new SelectList(
                especialidades, "Id", "Nombre", idEspecialidad);

            ViewBag.Doctores = new SelectList(
                doctores, "Id", "Nombre", idDoctor);
        }

        // VALIDAR FECHA Y HORA
        private bool ValidarFechaHora(
            CitaFormViewModel modelo,
            out DateTime fechaHora)
        {
            fechaHora = DateTime.MinValue;

            if (modelo.Fecha.Date < DateTime.Today)
                return false;

            if (!TimeSpan.TryParseExact(
                modelo.Hora,
                @"hh\:mm",
                CultureInfo.InvariantCulture,
                out TimeSpan hora))
                return false;

            if (hora < new TimeSpan(8, 0, 0) ||
                hora > new TimeSpan(19, 30, 0))
                return false;

            if (hora.Minutes != 0 && hora.Minutes != 30)
                return false;

            fechaHora = modelo.Fecha.Date.Add(hora);

            if (fechaHora <= DateTime.Now)
                return false;

            return true;
        }
    }
}
