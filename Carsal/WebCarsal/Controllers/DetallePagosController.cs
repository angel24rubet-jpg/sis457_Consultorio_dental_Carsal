
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCarsal.Models;

public class DetallePagosController : Controller
{
    private readonly CarsalContext _context;

    public DetallePagosController(CarsalContext context)
    {
        _context = context;
    }

    // GET: DETALLEPAGOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.DetallePago.ToListAsync());
    }

    // GET: DETALLEPAGOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var detallepago = await _context.DetallePago
            .FirstOrDefaultAsync(m => m.Id == id);
        if (detallepago == null)
        {
            return NotFound();
        }

        return View(detallepago);
    }

    // GET: DETALLEPAGOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: DETALLEPAGOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,IdPago,IdServicio,Cantidad,PrecioUnitario,Subtotal,IdPagoNavigation,IdServicioNavigation")] DetallePago detallepago)
    {
        if (ModelState.IsValid)
        {
            _context.Add(detallepago);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(detallepago);
    }

    // GET: DETALLEPAGOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var detallepago = await _context.DetallePago.FindAsync(id);
        if (detallepago == null)
        {
            return NotFound();
        }
        return View(detallepago);
    }

    // POST: DETALLEPAGOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,IdPago,IdServicio,Cantidad,PrecioUnitario,Subtotal,IdPagoNavigation,IdServicioNavigation")] DetallePago detallepago)
    {
        if (id != detallepago.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(detallepago);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetallePagoExists(detallepago.Id))
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
        return View(detallepago);
    }

    // GET: DETALLEPAGOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var detallepago = await _context.DetallePago
            .FirstOrDefaultAsync(m => m.Id == id);
        if (detallepago == null)
        {
            return NotFound();
        }

        return View(detallepago);
    }

    // POST: DETALLEPAGOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var detallepago = await _context.DetallePago.FindAsync(id);
        if (detallepago != null)
        {
            _context.DetallePago.Remove(detallepago);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool DetallePagoExists(int? id)
    {
        return _context.DetallePago.Any(e => e.Id == id);
    }
}
