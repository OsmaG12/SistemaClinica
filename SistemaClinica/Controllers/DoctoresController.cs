using Microsoft.AspNetCore.Mvc;

namespace SistemaClinica.Controllers
{
    public class DoctoresController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
