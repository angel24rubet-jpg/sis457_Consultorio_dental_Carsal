
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCarsal.Models;

public class PagosController : Controller
{
    private readonly CarsalContext _context;

    public PagosController(CarsalContext context)
    {
        _context = context;
    }

    // GET: PAGOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Pago.ToListAsync());
    }

    // GET: PAGOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pago = await _context.Pago
            .FirstOrDefaultAsync(m => m.Id == id);
        if (pago == null)
        {
            return NotFound();
        }

        return View(pago);
    }

    // GET: PAGOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PAGOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,IdCita,FechaPago,Monto,MetodoPago,Concepto,Estado,IdUsuarioRegistro,IdUsuarioModificacion,FechaModificacion,DetallePago,IdCitaNavigation,IdUsuarioModificacionNavigation,IdUsuarioRegistroNavigation")] Pago pago)
    {
        if (ModelState.IsValid)
        {
            _context.Add(pago);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(pago);
    }

    // GET: PAGOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pago = await _context.Pago.FindAsync(id);
        if (pago == null)
        {
            return NotFound();
        }
        return View(pago);
    }

    // POST: PAGOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,IdCita,FechaPago,Monto,MetodoPago,Concepto,Estado,IdUsuarioRegistro,IdUsuarioModificacion,FechaModificacion,DetallePago,IdCitaNavigation,IdUsuarioModificacionNavigation,IdUsuarioRegistroNavigation")] Pago pago)
    {
        if (id != pago.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(pago);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagoExists(pago.Id))
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
        return View(pago);
    }

    // GET: PAGOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pago = await _context.Pago
            .FirstOrDefaultAsync(m => m.Id == id);
        if (pago == null)
        {
            return NotFound();
        }

        return View(pago);
    }

    // POST: PAGOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var pago = await _context.Pago.FindAsync(id);
        if (pago != null)
        {
            _context.Pago.Remove(pago);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PagoExists(int? id)
    {
        return _context.Pago.Any(e => e.Id == id);
    }
}
