using Microsoft.AspNetCore.Mvc;

namespace SistemaClinica.Controllers
{
    public class EnfermerosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
