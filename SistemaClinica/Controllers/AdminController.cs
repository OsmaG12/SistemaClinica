using Microsoft.AspNetCore.Mvc;

namespace SistemaClinica.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
