using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaClinica.Modelos;
using System;
using System.Threading.Tasks;

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
        public async Task<IActionResult> CrearUsuario(ViewModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Modelo no válido");
                return BadRequest("Datos inválidos.");
            }

            // Asignar id_usuario en función de tipo_usuario
            switch (model.tipo_usuario.ToLower())
            {
                case "administrador":
                    model.id_usuario = 4;
                    break;
                case "enfermera":
                    model.id_usuario = 5;
                    break;
                case "medico":
                    model.id_usuario = 6;
                    break;
                case "farmacia":
                    model.id_usuario = 7;
                    break;
                case "secretaria":
                    model.id_usuario = 8;
                    break;
                default:
                    _logger.LogWarning("Tipo de usuario no válido");
                    return BadRequest("Tipo de usuario no válido.");
            }

            try
            {
                // Crear y guardar el usuario en la tabla 'usuario'
                var nuevoUsuario = new usuario
                {
                    username = model.username,
                    password = model.password,
                    tipo_usuario = model.tipo_usuario,
                    id_usuario = model.id_usuario
                };

                _context.usuario.Add(nuevoUsuario);
                await _context.SaveChangesAsync();

                // Dependiendo del tipo de usuario, crear el registro correspondiente
                switch (model.tipo_usuario.ToLower())
                {
                    case "administrador":
                        var administrador = new administrador
                        {
                            nombre = model.nombre,
                            telefono = model.telefono,
                            email = model.email,
                            id_usuario = nuevoUsuario.id_usuario
                        };
                        _context.administrador.Add(administrador);
                        break;

                    case "secretaria":
                        var secretaria = new secretaria
                        {
                            nombre = model.nombre,
                            apellido = model.apellido,
                            telefono = model.telefono,
                            email = model.email,
                            id_usuario = nuevoUsuario.id_usuario
                        };
                        _context.secretaria.Add(secretaria);
                        break;

                    case "enfermera":
                        var enfermera = new enfermera
                        {
                            nombre = model.nombre,
                            apellido = model.apellido,
                            turno = model.turno,
                            telefono = model.telefono,
                            email = model.email,
                            id_usuario = nuevoUsuario.id_usuario
                        };
                        _context.enfermera.Add(enfermera);
                        break;

                    case "medico":
                        var medico = new medico
                        {
                            nombre = model.nombre,
                            apellido = model.apellido,
                            especialidad = model.especialidad,
                            telefono = model.telefono,
                            email = model.email,
                            id_usuario = nuevoUsuario.id_usuario
                        };
                        _context.medico.Add(medico);
                        break;

                    case "farmacia":
                        var farmacia = new farmacia
                        {
                            nombre_medicamento = model.nombre,
                            cantidad = 0,
                            presentacion = "tabletas",
                            fecha_expiracion = DateTime.Now.AddYears(1),
                            id_usuario = nuevoUsuario.id_usuario
                        };
                        _context.farmacia.Add(farmacia);
                        break;
                }

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
                _logger.LogInformation("Usuario creado con éxito");

                return RedirectToAction("AdminHome", "Admin");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear usuario");
                return StatusCode(500, "Error interno al crear usuario.");
            }
        }
    }
}