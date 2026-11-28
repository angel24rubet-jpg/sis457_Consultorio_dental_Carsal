
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCarsal.Models;

namespace WebCarsal.Controllers
{
    public class DoctoresController : Controller
    {
        private readonly CarsalContext _context;

        public DoctoresController(CarsalContext context)
        {
            _context = context;
        }

        // Verificar si la solicitud viene de AJAX
        private bool EsSolicitudAjax()
        {
            return Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

        // Cargar especialidades activas
        private async Task CargarEspecialidades(int? seleccionada = null)
        {
            var especialidades = await _context.Especialidad
                .AsNoTracking()
                .Where(e => e.Estado)
                .OrderBy(e => e.Nombre)
                .ToListAsync();

            ViewBag.IdEspecialidad = new SelectList(
                especialidades,
                "Id",
                "Nombre",
                seleccionada
            );
        }

        // GET: DOCTORES
        public async Task<IActionResult> Index(string buscar)
        {
            var doctores = _context.Doctor
                .AsNoTracking()
                .Include(d => d.IdEspecialidadNavigation)
                .Where(d => d.Estado);

            if (!string.IsNullOrWhiteSpace(buscar))
            {
                buscar = buscar.Trim();

                doctores = doctores.Where(d =>
                    d.Ci.Contains(buscar) ||
                    d.Nombres.Contains(buscar) ||
                    d.Apellidos.Contains(buscar)
                );
            }

            var lista = await doctores
                .OrderBy(d => d.Apellidos)
                .ThenBy(d => d.Nombres)
                .ToListAsync();

            ViewData["Buscar"] = buscar;

            return View(lista);
        }

        // GET: DOCTORES/DETAILS/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var doctor = await _context.Doctor
                .AsNoTracking()
                .Include(d => d.IdEspecialidadNavigation)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (doctor == null)
                return NotFound();

            return PartialView("Details", doctor);
        }

        // GET: DOCTORES/CREATE
        public async Task<IActionResult> Create()
        {
            await CargarEspecialidades();

            return PartialView("Create", new DoctorFormViewModel());
        }

        // POST: DOCTORES/CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorFormViewModel modelo)
        {
            modelo.Ci = modelo.Ci?.Trim() ?? string.Empty;
            modelo.Nombres = modelo.Nombres?.Trim() ?? string.Empty;
            modelo.Apellidos = modelo.Apellidos?.Trim() ?? string.Empty;
            modelo.Celular = modelo.Celular?.Trim();
            modelo.Direccion = modelo.Direccion?.Trim();

            bool ciExiste = await _context.Doctor
                .AnyAsync(d => d.Ci == modelo.Ci);

            if (ciExiste)
            {
                ModelState.AddModelError(
                    nameof(modelo.Ci),
                    "Ya existe un doctor registrado con este CI."
                );
            }

            bool especialidadValida = modelo.IdEspecialidad.HasValue &&
                await _context.Especialidad.AnyAsync(e =>
                    e.Id == modelo.IdEspecialidad.Value && e.Estado);

            if (!especialidadValida)
            {
                ModelState.AddModelError(
                    nameof(modelo.IdEspecialidad),
                    "Seleccione una especialidad activa."
                );
            }

            if (!ModelState.IsValid)
            {
                await CargarEspecialidades(modelo.IdEspecialidad);

                return PartialView("Create", modelo);
            }

            var doctor = new Doctor
            {
                Ci = modelo.Ci,
                Nombres = modelo.Nombres,
                Apellidos = modelo.Apellidos,
                Celular = modelo.Celular,
                Direccion = modelo.Direccion,
                IdEspecialidad = modelo.IdEspecialidad!.Value,
                Estado = true,
                FechaRegistro = DateTime.Now
            };

            _context.Doctor.Add(doctor);
            await _context.SaveChangesAsync();

            if (EsSolicitudAjax())
            {
                return Json(new
                {
                    success = true,
                    message = "Doctor registrado correctamente."
                });
            }

            TempData["Mensaje"] = "Doctor registrado correctamente.";

            return RedirectToAction(nameof(Index));
        }

        // GET: DOCTORES/EDIT/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var doctor = await _context.Doctor.FindAsync(id);

            if (doctor == null || !doctor.Estado)
                return NotFound();

            var modelo = new DoctorFormViewModel
            {
                Ci = doctor.Ci,
                Nombres = doctor.Nombres,
                Apellidos = doctor.Apellidos,
                Celular = doctor.Celular,
                Direccion = doctor.Direccion,
                IdEspecialidad = doctor.IdEspecialidad
            };

            ViewBag.IdDoctor = doctor.Id;

            await CargarEspecialidades(doctor.IdEspecialidad);

            return PartialView("Edit", modelo);
        }

        // POST: DOCTORES/EDIT/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DoctorFormViewModel modelo)
        {
            var doctor = await _context.Doctor.FindAsync(id);

            if (doctor == null || !doctor.Estado)
                return NotFound();

            modelo.Ci = modelo.Ci?.Trim() ?? string.Empty;
            modelo.Nombres = modelo.Nombres?.Trim() ?? string.Empty;
            modelo.Apellidos = modelo.Apellidos?.Trim() ?? string.Empty;
            modelo.Celular = modelo.Celular?.Trim();
            modelo.Direccion = modelo.Direccion?.Trim();

            bool ciExiste = await _context.Doctor
                .AnyAsync(d => d.Ci == modelo.Ci && d.Id != id);

            if (ciExiste)
            {
                ModelState.AddModelError(
                    nameof(modelo.Ci),
                    "Ya existe otro doctor registrado con este CI."
                );
            }

            bool especialidadValida = modelo.IdEspecialidad.HasValue &&
                await _context.Especialidad.AnyAsync(e =>
                    e.Id == modelo.IdEspecialidad.Value && e.Estado);

            if (!especialidadValida)
            {
                ModelState.AddModelError(
                    nameof(modelo.IdEspecialidad),
                    "Seleccione una especialidad activa."
                );
            }

            if (!ModelState.IsValid)
            {
                ViewBag.IdDoctor = id;

                await CargarEspecialidades(modelo.IdEspecialidad);

                return PartialView("Edit", modelo);
            }

            doctor.Ci = modelo.Ci;
            doctor.Nombres = modelo.Nombres;
            doctor.Apellidos = modelo.Apellidos;
            doctor.Celular = modelo.Celular;
            doctor.Direccion = modelo.Direccion;
            doctor.IdEspecialidad = modelo.IdEspecialidad!.Value;
            doctor.FechaModificacion = DateTime.Now;

            await _context.SaveChangesAsync();

            if (EsSolicitudAjax())
            {
                return Json(new
                {
                    success = true,
                    message = "Doctor modificado correctamente."
                });
            }

            TempData["Mensaje"] = "Doctor modificado correctamente.";

            return RedirectToAction(nameof(Index));
        }

        // GET: DOCTORES/DELETE/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var doctor = await _context.Doctor
                .AsNoTracking()
                .Include(d => d.IdEspecialidadNavigation)
                .FirstOrDefaultAsync(d => d.Id == id && d.Estado);

            if (doctor == null)
                return NotFound();

            return PartialView("Delete", doctor);
        }

        // POST: DOCTORES/DELETE/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctor = await _context.Doctor.FindAsync(id);

            if (doctor == null || !doctor.Estado)
                return NotFound();

            // No desactivar si tiene citas pendientes o confirmadas
            bool tieneCitasActivas = await _context.Cita.AnyAsync(c =>
                c.IdDoctor == id &&
                (c.Estado == "Pendiente" || c.Estado == "Confirmada"));

            if (tieneCitasActivas)
            {
                string mensaje =
                    "No se puede desactivar al doctor porque tiene citas pendientes o confirmadas.";

                if (EsSolicitudAjax())
                {
                    Response.StatusCode = 400;

                    return Json(new
                    {
                        success = false,
                        message = mensaje
                    });
                }

                TempData["Error"] = mensaje;

                return RedirectToAction(nameof(Index));
            }

            doctor.Estado = false;
            doctor.FechaModificacion = DateTime.Now;

            await _context.SaveChangesAsync();

            if (EsSolicitudAjax())
            {
                return Json(new
                {
                    success = true,
                    message = "Doctor desactivado correctamente."
                });
            }

            TempData["Mensaje"] = "Doctor desactivado correctamente.";

            return RedirectToAction(nameof(Index));
        }
    }
}
