using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaClinica.Modelos;
using SistemaClinica.Models;
using System.Text.Json;

namespace SistemaClinica.Controllers
{
    public class InicioController : Controller
    {
        private readonly DBContext _context;
        private readonly ILogger<InicioController> _logger;

        public InicioController(DBContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            var listaDeRoles = (from m in _context.rol
                                select m).ToList();
            ViewData["listadoDeRoles"] = new SelectList(listaDeRoles, "id_rol", "nombre_rol");

            //Aqui recuperamos los datos de la variable de session hacia el objeto "datosUsuario"
            if (HttpContext.Session.GetString("user") != null)
            {
                var datosUsuario = JsonSerializer.Deserialize<empleado>(HttpContext.Session.GetString("user"));
                ViewBag.NombreUsuario = datosUsuario.usuario;
            }
            return View();
        }

        public IActionResult ValidarUsuario(empleado credenciales)
        {

            empleado? empleados = (from user in _context.empleado
                                where user.usuario == credenciales.usuario
                                 && user.contrasenya == credenciales.contrasenya
                                select user).FirstOrDefault();

            //Si las credenciales no son correctas, saltara el mensaje de error
            if (empleados == null)
            {
                ViewBag.Mensaje = "Credenciales incorrectas.";
                return View("Login");
            }

            //Si no da error, saltara a estas lineas de codigo
            string datoUsuario = JsonSerializer.Serialize(empleados);
            HttpContext.Session.SetString("user", datoUsuario);

            //Comprobación de tipo de usuario al autenticar...
            if (empleados.id_rol == 1)
            {
                HttpContext.Session.SetString("user", datoUsuario);
                return RedirectToAction("AdminHome", "Admin");
            }
            else if (empleados.id_rol == 2)
            {
                HttpContext.Session.SetString("user", datoUsuario);
                return RedirectToAction("Doctores", "Doctores");
            }
            else if (empleados.id_rol == 3)
            {
                HttpContext.Session.SetString("user", datoUsuario);
                return RedirectToAction("Enfermeros", "Enfermeros");
            }
            else
            {
                HttpContext.Session.SetString("user", datoUsuario);
                return RedirectToAction("Secretaria", "Secretaria"); // Redirige a la vista AdminHome en el controlador Admin
            }

            //Me redirige al Index de la carpeta Home, asi se hace para redirigir a otras vistas de diferentes carpetas
            //return RedirectToAction("Index", "Home");
        }



    }
}
