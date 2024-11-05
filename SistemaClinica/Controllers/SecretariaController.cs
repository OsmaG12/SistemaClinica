using Microsoft.AspNetCore.Mvc;

namespace SistemaClinica.Controllers
{
    public class SecretariaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
