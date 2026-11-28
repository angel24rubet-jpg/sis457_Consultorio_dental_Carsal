
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCarsal.Models;

public class EspecialidadesController : Controller
{
    private readonly CarsalContext _context;

    public EspecialidadesController(CarsalContext context)
    {
        _context = context;
    }

    // GET: ESPECIALIDADS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Especialidad.ToListAsync());
    }

    // GET: ESPECIALIDADS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var especialidad = await _context.Especialidad
            .FirstOrDefaultAsync(m => m.Id == id);
        if (especialidad == null)
        {
            return NotFound();
        }

        return View(especialidad);
    }

    // GET: ESPECIALIDADS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ESPECIALIDADS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Estado,IdUsuarioRegistro,FechaRegistro,IdUsuarioModificacion,FechaModificacion,Doctor,IdUsuarioModificacionNavigation,IdUsuarioRegistroNavigation")] Especialidad especialidad)
    {
        if (ModelState.IsValid)
        {
            _context.Add(especialidad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(especialidad);
    }

    // GET: ESPECIALIDADS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var especialidad = await _context.Especialidad.FindAsync(id);
        if (especialidad == null)
        {
            return NotFound();
        }
        return View(especialidad);
    }

    // POST: ESPECIALIDADS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nombre,Descripcion,Estado,IdUsuarioRegistro,FechaRegistro,IdUsuarioModificacion,FechaModificacion,Doctor,IdUsuarioModificacionNavigation,IdUsuarioRegistroNavigation")] Especialidad especialidad)
    {
        if (id != especialidad.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(especialidad);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EspecialidadExists(especialidad.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(especialidad);
    }

    // GET: ESPECIALIDADS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var especialidad = await _context.Especialidad
            .FirstOrDefaultAsync(m => m.Id == id);
        if (especialidad == null)
        {
            return NotFound();
        }

        return View(especialidad);
    }

    // POST: ESPECIALIDADS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var especialidad = await _context.Especialidad.FindAsync(id);
        if (especialidad != null)
        {
            _context.Especialidad.Remove(especialidad);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EspecialidadExists(int? id)
    {
        return _context.Especialidad.Any(e => e.Id == id);
    }
}
