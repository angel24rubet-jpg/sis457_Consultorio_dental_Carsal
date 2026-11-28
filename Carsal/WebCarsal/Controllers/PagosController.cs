
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCarsal.Models;

namespace WebCarsal.Controllers
{
    public class PagosController : Controller
    {
        private readonly CarsalContext _context;

        public PagosController(CarsalContext context)
        {
            _context = context;
        }

        // LISTADO DE PAGOS
        public async Task<IActionResult> Index()
        {
            var pagos = await _context.Pago
                .Include(p => p.IdCitaNavigation)
                    .ThenInclude(c => c.IdPacienteNavigation)
                .Include(p => p.DetallePago)
                    .ThenInclude(d => d.IdServicioNavigation)
                .OrderByDescending(p => p.FechaPago)
                .ToListAsync();

            return View(pagos);
        }

        // DETALLES DEL PAGO
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var pago = await _context.Pago
                .Include(p => p.IdCitaNavigation)
                    .ThenInclude(c => c.IdPacienteNavigation)
                .Include(p => p.IdCitaNavigation)
                    .ThenInclude(c => c.IdDoctorNavigation)
                .Include(p => p.DetallePago)
                    .ThenInclude(d => d.IdServicioNavigation)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pago == null)
                return NotFound();

            return View(pago);
        }

        // FORMULARIO DE REGISTRO
        [HttpGet]
        public IActionResult Create()
        {
            return View(new PagoFormViewModel());
        }

        // BUSCAR PACIENTE Y SUS CITAS ATENDIDAS
        [HttpGet]
        public async Task<IActionResult> BuscarPaciente(string ci)
        {
            if (string.IsNullOrWhiteSpace(ci))
            {
                return Json(new
                {
                    encontrado = false,
                    mensaje = "Ingrese el carnet de identidad."
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
                    c.Estado == "Atendida")
                .OrderByDescending(c => c.FechaHora)
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
                citas,
                mensaje = citas.Count == 0
                    ? "El paciente no tiene citas atendidas."
                    : "Seleccione la cita que desea cobrar."
            });
        }

        // OBTENER SERVICIOS ACTIVOS
        [HttpGet]
        public async Task<IActionResult> ObtenerServicios()
        {
            var servicios = await _context.Servicio
                .AsNoTracking()
                .Where(s => s.Estado)
                .OrderBy(s => s.Nombre)
                .Select(s => new
                {
                    id = s.Id,
                    nombre = s.Nombre,
                    precio = s.Precio
                })
                .ToListAsync();

            return Json(servicios);
        }

        // REGISTRAR PAGO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PagoFormViewModel modelo)
        {
            if (modelo.IdCita == null)
                ModelState.AddModelError("IdCita", "Seleccione una cita atendida.");

            if (modelo.Detalles == null || !modelo.Detalles.Any())
                ModelState.AddModelError("Detalles", "Agregue al menos un servicio.");

            if (modelo.MetodoPago != "Efectivo" && modelo.MetodoPago != "QR")
                ModelState.AddModelError("MetodoPago", "Seleccione Efectivo o QR.");

            if (modelo.Detalles != null)
            {
                foreach (var detalle in modelo.Detalles)
                {
                    if (detalle.Cantidad < 1)
                        ModelState.AddModelError("Detalles", "La cantidad debe ser mayor a cero.");
                }

                if (modelo.Detalles
                    .GroupBy(d => d.IdServicio)
                    .Any(g => g.Count() > 1))
                {
                    ModelState.AddModelError("Detalles", "No agregue el mismo servicio más de una vez.");
                }
            }

            Cita? cita = null;
            List<Servicio> servicios = new();

            if (modelo.IdCita.HasValue)
            {
                cita = await _context.Cita
                    .Include(c => c.IdPacienteNavigation)
                    .FirstOrDefaultAsync(c =>
                        c.Id == modelo.IdCita.Value &&
                        c.IdPaciente == modelo.IdPaciente &&
                        c.Estado == "Atendida");

                if (cita == null)
                    ModelState.AddModelError("IdCita", "La cita no existe o no está atendida.");
            }

            if (modelo.Detalles != null && modelo.Detalles.Any())
            {
                var idsServicios = modelo.Detalles
                    .Select(d => d.IdServicio)
                    .Distinct()
                    .ToList();

                servicios = await _context.Servicio
                    .Where(s => idsServicios.Contains(s.Id) && s.Estado)
                    .ToListAsync();

                if (servicios.Count != idsServicios.Count)
                    ModelState.AddModelError("Detalles", "Uno o más servicios no están disponibles.");
            }

            decimal total = 0;

            if (servicios.Count > 0 && modelo.Detalles != null)
            {
                foreach (var detalle in modelo.Detalles)
                {
                    var servicio = servicios.FirstOrDefault(s =>
                        s.Id == detalle.IdServicio);

                    if (servicio != null)
                        total += servicio.Precio * detalle.Cantidad;
                }
            }

            if (modelo.MetodoPago == "Efectivo" &&
                modelo.EfectivoRecibido < total)
            {
                ModelState.AddModelError(
                    "EfectivoRecibido",
                    "El efectivo recibido no puede ser menor al total.");
            }

            if (!ModelState.IsValid)
                return View(modelo);

            await using var transaccion =
                await _context.Database.BeginTransactionAsync();

            try
            {
                var pago = new Pago
                {
                    IdCita = cita!.Id,
                    FechaPago = DateTime.Now,
                    Monto = total,
                    MetodoPago = modelo.MetodoPago,
                    Concepto = "Pago de servicios odontológicos",
                    Estado = "Pagado"
                };

                _context.Pago.Add(pago);

                await _context.SaveChangesAsync();

                foreach (var detalle in modelo.Detalles)
                {
                    var servicio = servicios.First(s =>
                        s.Id == detalle.IdServicio);

                    var detallePago = new DetallePago
                    {
                        IdPago = pago.Id,
                        IdServicio = servicio.Id,
                        Cantidad = detalle.Cantidad,
                        PrecioUnitario = servicio.Precio,
                        Subtotal = servicio.Precio * detalle.Cantidad
                    };

                    _context.DetallePago.Add(detallePago);
                }

                await _context.SaveChangesAsync();
                await transaccion.CommitAsync();

                TempData["Mensaje"] =
                    "Pago registrado correctamente. Estado: Pagado.";

                TempData["TotalPago"] = total.ToString("0.00");
                TempData["Cambio"] =
                    modelo.MetodoPago == "Efectivo"
                        ? (modelo.EfectivoRecibido - total).ToString("0.00")
                        : "0.00";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                await transaccion.RollbackAsync();

                ModelState.AddModelError(
                    "",
                    "Ocurrió un error al registrar el pago. Intente nuevamente.");

                return View(modelo);
            }
        }
    }
}
