using Microsoft.AspNetCore.Mvc;

namespace SistemaClinica.Controllers
{
    public class FarmaciaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
