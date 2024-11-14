using Microsoft.AspNetCore.Mvc;
using SistemaClinica.Modelos;
using SistemaClinica.Models;

namespace SistemaClinica.Controllers
{
    public class AdminController : Controller
    {
        private readonly DBContext _context;
        private readonly ILogger<AdminController> _logger;

        public AdminController(DBContext context, ILogger<AdminController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult AdminHome()
        {
            return View("AdminHome");
        }

        public IActionResult AgregarUsuarios()
        {
            return View("AgregarUsuarios");
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> AgregarEmpleado(empleado empleado)
        {
            _logger.LogInformation("Datos recibidos para crear un empleado: {@Empleado}", empleado);

            try
            {
                _context.empleado.Add(empleado);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Empleado creado con éxito.");
                return RedirectToAction("AdminHome");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al guardar el empleado: {Error}", ex.Message);
                return View("AgregarUsuarios", empleado);
            }
        }

    }
}