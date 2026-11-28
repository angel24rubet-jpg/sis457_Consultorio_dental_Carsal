
using System;
using System.Linq;
using System.Threading.Tasks;
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

        // GET: PACIENTES
        public async Task<IActionResult> Index(string buscar)
        {
            var pacientes = _context.Paciente
                .AsNoTracking()
                .Where(p => p.Estado);

            if (!string.IsNullOrWhiteSpace(buscar))
            {
                buscar = buscar.Trim();

                pacientes = pacientes.Where(p =>
                    p.Ci.Contains(buscar) ||
                    p.Nombres.Contains(buscar) ||
                    (p.PrimerApellido != null && p.PrimerApellido.Contains(buscar)) ||
                    (p.SegundoApellido != null && p.SegundoApellido.Contains(buscar))
                );
            }

            var lista = await pacientes
                .OrderBy(p => p.PrimerApellido)
                .ThenBy(p => p.SegundoApellido)
                .ThenBy(p => p.Nombres)
                .ToListAsync();

            ViewData["Buscar"] = buscar;

            return View(lista);
        }

        // GET: PACIENTES/DETAILS/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var paciente = await _context.Paciente
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (paciente == null)
                return NotFound();

            return View(paciente);
        }

        // GET: PACIENTES/CREATE
        public IActionResult Create()
        {
            return View(new PacienteFormViewModel());
        }

        // POST: PACIENTES/CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PacienteFormViewModel modelo)
        {
            // Limpiar espacios
            modelo.Ci = modelo.Ci?.Trim() ?? string.Empty;
            modelo.Nombres = modelo.Nombres?.Trim() ?? string.Empty;
            modelo.PrimerApellido = modelo.PrimerApellido?.Trim();
            modelo.SegundoApellido = modelo.SegundoApellido?.Trim();
            modelo.Sexo = modelo.Sexo?.Trim();
            modelo.Celular = modelo.Celular?.Trim();
            modelo.Direccion = modelo.Direccion?.Trim();

            // Validar que exista al menos un apellido
            if (string.IsNullOrWhiteSpace(modelo.PrimerApellido) &&
                string.IsNullOrWhiteSpace(modelo.SegundoApellido))
            {
                ModelState.AddModelError(
                    nameof(modelo.PrimerApellido),
                    "Debe ingresar al menos un apellido."
                );
            }

            // Verificar CI entre todos los pacientes
            bool ciExiste = await _context.Paciente
                .AnyAsync(p => p.Ci == modelo.Ci);

            if (ciExiste)
            {
                ModelState.AddModelError(
                    nameof(modelo.Ci),
                    "Ya existe un paciente registrado con este CI."
                );
            }

            if (!ModelState.IsValid)
                return View(modelo);

            // Crear paciente
            var paciente = new Paciente
            {
                Ci = modelo.Ci,
                Nombres = modelo.Nombres,
                PrimerApellido = modelo.PrimerApellido,
                SegundoApellido = modelo.SegundoApellido,
                FechaNacimiento = modelo.FechaNacimiento,
                Sexo = modelo.Sexo,
                Celular = modelo.Celular,
                Direccion = modelo.Direccion,
                Estado = true,
                FechaRegistro = DateTime.Now
            };

            _context.Paciente.Add(paciente);
            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Paciente registrado correctamente.";

            return RedirectToAction(nameof(Index));
        }

        // GET: PACIENTES/EDIT/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var paciente = await _context.Paciente.FindAsync(id);

            if (paciente == null || !paciente.Estado)
                return NotFound();

            var modelo = new PacienteFormViewModel
            {
                Ci = paciente.Ci,
                Nombres = paciente.Nombres,
                PrimerApellido = paciente.PrimerApellido,
                SegundoApellido = paciente.SegundoApellido,
                FechaNacimiento = paciente.FechaNacimiento,
                Sexo = paciente.Sexo,
                Celular = paciente.Celular,
                Direccion = paciente.Direccion
            };

            return View(modelo);
        }

        // POST: PACIENTES/EDIT/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PacienteFormViewModel modelo)
        {
            var paciente = await _context.Paciente.FindAsync(id);

            if (paciente == null || !paciente.Estado)
                return NotFound();

            // Limpiar espacios
            modelo.Ci = modelo.Ci?.Trim() ?? string.Empty;
            modelo.Nombres = modelo.Nombres?.Trim() ?? string.Empty;
            modelo.PrimerApellido = modelo.PrimerApellido?.Trim();
            modelo.SegundoApellido = modelo.SegundoApellido?.Trim();
            modelo.Sexo = modelo.Sexo?.Trim();
            modelo.Celular = modelo.Celular?.Trim();
            modelo.Direccion = modelo.Direccion?.Trim();

            // Validar apellidos
            if (string.IsNullOrWhiteSpace(modelo.PrimerApellido) &&
                string.IsNullOrWhiteSpace(modelo.SegundoApellido))
            {
                ModelState.AddModelError(
                    nameof(modelo.PrimerApellido),
                    "Debe ingresar al menos un apellido."
                );
            }

            // Verificar CI sin contar al propio paciente
            bool ciExiste = await _context.Paciente
                .AnyAsync(p => p.Ci == modelo.Ci && p.Id != id);

            if (ciExiste)
            {
                ModelState.AddModelError(
                    nameof(modelo.Ci),
                    "Ya existe otro paciente registrado con este CI."
                );
            }

            if (!ModelState.IsValid)
                return View(modelo);

            // Actualizar datos personales
            paciente.Ci = modelo.Ci;
            paciente.Nombres = modelo.Nombres;
            paciente.PrimerApellido = modelo.PrimerApellido;
            paciente.SegundoApellido = modelo.SegundoApellido;
            paciente.FechaNacimiento = modelo.FechaNacimiento;
            paciente.Sexo = modelo.Sexo;
            paciente.Celular = modelo.Celular;
            paciente.Direccion = modelo.Direccion;

            paciente.FechaModificacion = DateTime.Now;

            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Paciente modificado correctamente.";

            return RedirectToAction(nameof(Index));
        }

        // GET: PACIENTES/DELETE/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var paciente = await _context.Paciente
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id && p.Estado);

            if (paciente == null)
                return NotFound();

            return View(paciente);
        }

        // POST: PACIENTES/DELETE/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paciente = await _context.Paciente.FindAsync(id);

            if (paciente == null || !paciente.Estado)
                return NotFound();

            // Desactivar sin eliminar de la base de datos
            paciente.Estado = false;
            paciente.FechaModificacion = DateTime.Now;

            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Paciente desactivado correctamente.";

            return RedirectToAction(nameof(Index));
        }
    }
}
