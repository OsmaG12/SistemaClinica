using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaClinica.Modelos;
using System.Diagnostics;
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
			var listaDeRoles = (from m in _context.usuario
								select m).ToList();
			ViewData["listadoDeRoles"] = new SelectList(listaDeRoles, "id_usuario", "tipo_usuario");

			//Aqui recuperamos los datos de la variable de session hacia el objeto "datosUsuario"
			if (HttpContext.Session.GetString("user") != null)
			{
				var datosUsuario = JsonSerializer.Deserialize<usuario>(HttpContext.Session.GetString("user"));
				ViewBag.NombreUsuario = datosUsuario.tipo_usuario;
			}
			return View();
		}

		public IActionResult ValidarUsuario(usuario credenciales)
		{

            usuario? usuario = (from user in _context.usuario
                                where user.username == credenciales.username
								 && user.password == credenciales.password
								 select user).FirstOrDefault();

			//Si las credenciales no son correctas, saltara el mensaje de error
			if (usuario == null)
			{
				ViewBag.Mensaje = "Credenciales incorrectas.";
				return View("Login");
			}

			//Si no da error, saltara a estas lineas de codigo
			string datoUsuario = JsonSerializer.Serialize(usuario);
			HttpContext.Session.SetString("user", datoUsuario);

			//Comprobación de tipo de usuario al autenticar...
			if (usuario.tipo_usuario == "medico")
			{
				HttpContext.Session.SetString("user", datoUsuario);
				return RedirectToAction("HomeCliente", "Cliente");
			}
			else if (usuario.tipo_usuario == "enfermera")
			{
				HttpContext.Session.SetString("user", datoUsuario);
				return RedirectToAction("HomeAdmin", "Admin");
			}
			else if (usuario.tipo_usuario == "secretaria")
			{
				HttpContext.Session.SetString("user", datoUsuario);
				return RedirectToAction("Secretaria", "SecretariaController");
			}
			else if (usuario.tipo_usuario == "farmacia")
			{
				HttpContext.Session.SetString ("user", datoUsuario);
				return RedirectToAction("Farmacia", "FarmaciaController");
			}
            else
            {
                HttpContext.Session.SetString("user", datoUsuario);
                return RedirectToAction("AdminHome", "Admin"); // Redirige a la vista AdminHome en el controlador Admin
            }

            //Me redirige al Index de la carpeta Home, asi se hace para redirigir a otras vistas de diferentes carpetas
            //return RedirectToAction("Index", "Home");
        }



	}
}
