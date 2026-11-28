using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCarsal.Models;

public class UsuariosController : Controller
{
    private readonly CarsalContext _context;
    private readonly PasswordHasher<Usuario> _hasher;

    public UsuariosController(CarsalContext context)
    {
        _context = context;
        _hasher = new PasswordHasher<Usuario>();
    }

    // LISTAR USUARIOS
    public async Task<IActionResult> Index()
    {
        var usuarios = await _context.Usuario
            .Include(u => u.IdDoctorNavigation)
            .OrderBy(u => u.NombreUsuario)
            .ToListAsync();

        return View(usuarios);
    }

    // REGISTRAR USUARIO
    public IActionResult Create()
    {
        CargarDoctores();
        return View(new UsuarioFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UsuarioFormViewModel modelo)
    {
        modelo.NombreUsuario = modelo.NombreUsuario.Trim();

        if (await _context.Usuario.AnyAsync(u =>
            u.NombreUsuario.ToLower() == modelo.NombreUsuario.ToLower()))
        {
            ModelState.AddModelError("NombreUsuario",
                "Este nombre de usuario ya está registrado.");
        }

        if (modelo.Rol != "Administrador" && modelo.Rol != "Doctor")
        {
            ModelState.AddModelError("Rol", "Seleccione un rol válido.");
        }

        if (string.IsNullOrWhiteSpace(modelo.Clave))
        {
            ModelState.AddModelError("Clave", "Ingrese una contraseña.");
        }

        if (modelo.Rol == "Administrador")
        {
            modelo.IdDoctor = null;
        }
        else if (modelo.Rol == "Doctor")
        {
            await ValidarDoctor(modelo.IdDoctor, null);
        }

        if (!ModelState.IsValid)
        {
            CargarDoctores(modelo.IdDoctor);
            return View(modelo);
        }

        var usuario = new Usuario
        {
            NombreUsuario = modelo.NombreUsuario,
            Rol = modelo.Rol,
            IdDoctor = modelo.IdDoctor,
            Estado = true
        };

        usuario.ClaveHash = _hasher.HashPassword(
            usuario, modelo.Clave!);

        _context.Usuario.Add(usuario);
        await _context.SaveChangesAsync();

        TempData["Mensaje"] = "Usuario registrado correctamente.";

        return RedirectToAction(nameof(Index));
    }

    // EDITAR USUARIO
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var usuario = await _context.Usuario.FindAsync(id);

        if (usuario == null)
            return NotFound();

        var modelo = new UsuarioFormViewModel
        {
            Id = usuario.Id,
            NombreUsuario = usuario.NombreUsuario,
            Rol = usuario.Rol,
            IdDoctor = usuario.IdDoctor
        };

        CargarDoctores(usuario.IdDoctor);

        return View(modelo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UsuarioFormViewModel modelo)
    {
        if (id != modelo.Id)
            return NotFound();

        var usuario = await _context.Usuario.FindAsync(id);

        if (usuario == null)
            return NotFound();

        modelo.NombreUsuario = modelo.NombreUsuario.Trim();

        if (await _context.Usuario.AnyAsync(u =>
            u.Id != id &&
            u.NombreUsuario.ToLower() == modelo.NombreUsuario.ToLower()))
        {
            ModelState.AddModelError("NombreUsuario",
                "Este nombre de usuario ya está registrado.");
        }

        if (modelo.Rol != "Administrador" && modelo.Rol != "Doctor")
        {
            ModelState.AddModelError("Rol", "Seleccione un rol válido.");
        }

        if (modelo.Rol == "Administrador")
        {
            modelo.IdDoctor = null;
        }
        else if (modelo.Rol == "Doctor")
        {
            await ValidarDoctor(modelo.IdDoctor, id);
        }

        if (!ModelState.IsValid)
        {
            CargarDoctores(modelo.IdDoctor);
            return View(modelo);
        }

        usuario.NombreUsuario = modelo.NombreUsuario;
        usuario.Rol = modelo.Rol;
        usuario.IdDoctor = modelo.IdDoctor;

        // Solo cambia la contraseña si se ingresó una nueva
        if (!string.IsNullOrWhiteSpace(modelo.Clave))
        {
            usuario.ClaveHash = _hasher.HashPassword(
                usuario, modelo.Clave);
        }

        await _context.SaveChangesAsync();

        TempData["Mensaje"] = "Usuario actualizado correctamente.";

        return RedirectToAction(nameof(Index));
    }

    // ACTIVAR / DESACTIVAR USUARIO
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CambiarEstado(int id)
    {
        var usuario = await _context.Usuario.FindAsync(id);

        if (usuario == null)
            return NotFound();

        usuario.Estado = !usuario.Estado;

        await _context.SaveChangesAsync();

        TempData["Mensaje"] = usuario.Estado
            ? "Usuario activado correctamente."
            : "Usuario desactivado correctamente.";

        return RedirectToAction(nameof(Index));
    }

    // VALIDAR DOCTOR ASOCIADO
    private async Task ValidarDoctor(int? idDoctor, int? idUsuarioActual)
    {
        if (idDoctor == null)
        {
            ModelState.AddModelError("IdDoctor",
                "Debe seleccionar un doctor.");
            return;
        }

        var doctor = await _context.Doctor
            .FirstOrDefaultAsync(d => d.Id == idDoctor && d.Estado);

        if (doctor == null)
        {
            ModelState.AddModelError("IdDoctor",
                "El doctor seleccionado no existe o está inactivo.");
            return;
        }

        bool yaAsignado = await _context.Usuario.AnyAsync(u =>
            u.IdDoctor == idDoctor &&
            u.Id != idUsuarioActual);

        if (yaAsignado)
        {
            ModelState.AddModelError("IdDoctor",
                "Este doctor ya tiene una cuenta de usuario.");
        }
    }

    // CARGAR DOCTORES ACTIVOS
    private void CargarDoctores(int? seleccionado = null)
    {
        ViewBag.Doctores = new SelectList(
            _context.Doctor
                .Where(d => d.Estado)
                .OrderBy(d => d.Nombres)
                .Select(d => new
                {
                    d.Id,
                    Nombre = d.Nombres + " " + d.Apellidos
                })
                .ToList(),
            "Id",
            "Nombre",
            seleccionado
        );
    }
}