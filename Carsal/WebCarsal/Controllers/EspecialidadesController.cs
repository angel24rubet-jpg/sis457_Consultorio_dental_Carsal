
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCarsal.Models;

namespace WebCarsal.Controllers
{
    public class EspecialidadesController : Controller
    {
        private readonly CarsalContext _context;

        public EspecialidadesController(CarsalContext context)
        {
            _context = context;
        }

        // GET: ESPECIALIDADES
        public async Task<IActionResult> Index(string buscar)
        {
            var especialidades = _context.Especialidad
                .AsNoTracking()
                .Where(e => e.Estado);

            if (!string.IsNullOrWhiteSpace(buscar))
            {
                buscar = buscar.Trim();

                especialidades = especialidades.Where(e =>
                    e.Nombre.Contains(buscar));
            }

            var lista = await especialidades
                .OrderBy(e => e.Nombre)
                .ToListAsync();

            ViewData["Buscar"] = buscar;

            return View(lista);
        }

        // GET: ESPECIALIDADES/DETAILS/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var especialidad = await _context.Especialidad
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (especialidad == null)
                return NotFound();

            return View(especialidad);
        }

        // GET: ESPECIALIDADES/CREATE
        public IActionResult Create()
        {
            return View(new EspecialidadFormViewModel());
        }

        // POST: ESPECIALIDADES/CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EspecialidadFormViewModel modelo)
        {
            modelo.Nombre = modelo.Nombre?.Trim() ?? string.Empty;
            modelo.Descripcion = modelo.Descripcion?.Trim();

            // Verificar nombre duplicado, incluso en desactivadas
            bool nombreExiste = await _context.Especialidad
                .AnyAsync(e => e.Nombre == modelo.Nombre);

            if (nombreExiste)
            {
                ModelState.AddModelError(
                    nameof(modelo.Nombre),
                    "Ya existe una especialidad registrada con este nombre."
                );
            }

            if (!ModelState.IsValid)
                return View(modelo);

            var especialidad = new Especialidad
            {
                Nombre = modelo.Nombre,
                Descripcion = modelo.Descripcion,
                Estado = true,
                FechaRegistro = DateTime.Now
            };

            _context.Especialidad.Add(especialidad);
            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Especialidad registrada correctamente.";

            return RedirectToAction(nameof(Index));
        }

        // GET: ESPECIALIDADES/EDIT/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var especialidad = await _context.Especialidad.FindAsync(id);

            if (especialidad == null || !especialidad.Estado)
                return NotFound();

            var modelo = new EspecialidadFormViewModel
            {
                Nombre = especialidad.Nombre,
                Descripcion = especialidad.Descripcion
            };

            return View(modelo);
        }

        // POST: ESPECIALIDADES/EDIT/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EspecialidadFormViewModel modelo)
        {
            var especialidad = await _context.Especialidad.FindAsync(id);

            if (especialidad == null || !especialidad.Estado)
                return NotFound();

            modelo.Nombre = modelo.Nombre?.Trim() ?? string.Empty;
            modelo.Descripcion = modelo.Descripcion?.Trim();

            // Verificar nombre duplicado sin contar la especialidad actual
            bool nombreExiste = await _context.Especialidad
                .AnyAsync(e => e.Nombre == modelo.Nombre && e.Id != id);

            if (nombreExiste)
            {
                ModelState.AddModelError(
                    nameof(modelo.Nombre),
                    "Ya existe otra especialidad con este nombre."
                );
            }

            if (!ModelState.IsValid)
                return View(modelo);

            especialidad.Nombre = modelo.Nombre;
            especialidad.Descripcion = modelo.Descripcion;
            especialidad.FechaModificacion = DateTime.Now;

            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Especialidad modificada correctamente.";

            return RedirectToAction(nameof(Index));
        }

        // GET: ESPECIALIDADES/DELETE/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var especialidad = await _context.Especialidad
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id && e.Estado);

            if (especialidad == null)
                return NotFound();

            return View(especialidad);
        }

        // POST: ESPECIALIDADES/DELETE/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var especialidad = await _context.Especialidad.FindAsync(id);

            if (especialidad == null || !especialidad.Estado)
                return NotFound();

            // Verificar si existen doctores activos asociados
            bool tieneDoctoresActivos = await _context.Doctor
                .AnyAsync(d => d.IdEspecialidad == id && d.Estado);

            if (tieneDoctoresActivos)
            {
                TempData["Error"] =
                    "No se puede desactivar esta especialidad porque tiene doctores activos asociados.";

                return RedirectToAction(nameof(Index));
            }

            especialidad.Estado = false;
            especialidad.FechaModificacion = DateTime.Now;

            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Especialidad desactivada correctamente.";

            return RedirectToAction(nameof(Index));
        }
    }
}
