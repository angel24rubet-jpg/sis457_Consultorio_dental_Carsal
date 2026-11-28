
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCarsal.Models;

public class HistorialClinicoController : Controller
{
    private readonly CarsalContext _context;

    public HistorialClinicoController(CarsalContext context)
    {
        _context = context;
    }

    // GET: HISTORIALCLINICOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.HistorialClinico.ToListAsync());
    }

    // GET: HISTORIALCLINICOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var historialclinico = await _context.HistorialClinico
            .FirstOrDefaultAsync(m => m.Id == id);
        if (historialclinico == null)
        {
            return NotFound();
        }

        return View(historialclinico);
    }

    // GET: HISTORIALCLINICOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: HISTORIALCLINICOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,IdPaciente,IdCita,FechaAtencion,Diagnostico,Tratamiento,Observaciones,IdUsuarioRegistro,IdUsuarioModificacion,FechaModificacion,IdCitaNavigation,IdPacienteNavigation,IdUsuarioModificacionNavigation,IdUsuarioRegistroNavigation")] HistorialClinico historialclinico)
    {
        if (ModelState.IsValid)
        {
            _context.Add(historialclinico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(historialclinico);
    }

    // GET: HISTORIALCLINICOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var historialclinico = await _context.HistorialClinico.FindAsync(id);
        if (historialclinico == null)
        {
            return NotFound();
        }
        return View(historialclinico);
    }

    // POST: HISTORIALCLINICOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,IdPaciente,IdCita,FechaAtencion,Diagnostico,Tratamiento,Observaciones,IdUsuarioRegistro,IdUsuarioModificacion,FechaModificacion,IdCitaNavigation,IdPacienteNavigation,IdUsuarioModificacionNavigation,IdUsuarioRegistroNavigation")] HistorialClinico historialclinico)
    {
        if (id != historialclinico.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(historialclinico);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistorialClinicoExists(historialclinico.Id))
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
        return View(historialclinico);
    }

    // GET: HISTORIALCLINICOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var historialclinico = await _context.HistorialClinico
            .FirstOrDefaultAsync(m => m.Id == id);
        if (historialclinico == null)
        {
            return NotFound();
        }

        return View(historialclinico);
    }

    // POST: HISTORIALCLINICOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var historialclinico = await _context.HistorialClinico.FindAsync(id);
        if (historialclinico != null)
        {
            _context.HistorialClinico.Remove(historialclinico);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool HistorialClinicoExists(int? id)
    {
        return _context.HistorialClinico.Any(e => e.Id == id);
    }
}
