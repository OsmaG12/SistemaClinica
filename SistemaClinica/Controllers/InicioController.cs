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
		public IActionResult Index()
        {
			var listaDeRoles = (from m in _context.Usuarios
								select m).ToList();
			ViewData["listadoDeRoles"] = new SelectList(listaDeRoles, "id_usuario", "tipo_usuario");

			//Aqui recuperamos los datos de la variable de session hacia el objeto "datosUsuario"
			if (HttpContext.Session.GetString("user") != null)
			{
				var datosUsuario = JsonSerializer.Deserialize<Usuario>(HttpContext.Session.GetString("user"));
				ViewBag.NombreUsuario = datosUsuario.TipoUsuario;
			}
			return View();
		}

		public IActionResult ValidarUsuario(Usuario credenciales)
		{

			Usuario? usuario = (from user in _context.Usuarios
								 where user.Username == credenciales.Username
								 && user.Password == credenciales.Password
								 select user).FirstOrDefault();

			//Si las credenciales no son correctas, saltara el mensaje de error
			if (usuario == null)
			{
				ViewBag.Mensaje = "Credenciales incorrectas.";
				return View("Index");
			}

			//Si no da error, saltara a estas lineas de codigo
			string datoUsuario = JsonSerializer.Serialize(usuario);
			HttpContext.Session.SetString("user", datoUsuario);

			//Comprobación de tipo de usuario al autenticar...
			if (usuario.TipoUsuario == "medico")
			{
				HttpContext.Session.SetString("user", datoUsuario);
				return RedirectToAction("HomeCliente", "Cliente");
			}
			else if (usuario.TipoUsuario == "enfermera")
			{
				HttpContext.Session.SetString("user", datoUsuario);
				return RedirectToAction("HomeAdmin", "Admin");
			}
			else if (usuario.TipoUsuario == "secretaria")
			{
				HttpContext.Session.SetString("user", datoUsuario);
				return RedirectToAction("Secretaria", "SecretariaController");
			}
			else if (usuario.TipoUsuario == "farmacia")
			{
				HttpContext.Session.SetString ("user", datoUsuario);
				return RedirectToAction("Farmacia", "FarmaciaController");
			}
			else
			{
				HttpContext.Session.SetString("user", datoUsuario);
				return RedirectToAction("Administrador", "AdministradorController");
			}

			//Me redirige al Index de la carpeta Home, asi se hace para redirigir a otras vistas de diferentes carpetas
			//return RedirectToAction("Index", "Home");
		}



	}
}
