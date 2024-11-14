using Microsoft.AspNetCore.Mvc;
using SistemaClinica.Modelos;

namespace SistemaClinica.Controllers
{
    public class EnfermerosController : Controller
    {
        private readonly DBContext _context;

        public EnfermerosController(DBContext context)
        {
            _context = context;
        }

        //public async Task<IActionResult> Enfermeros()
        //{
        //    var listadoPacientes = (from p in _context.paciente
        //                            join c in _context.cita on p.id_paciente equals c.id_paciente
        //                            select new
        //                            {
        //                                Nombre = p.Nombre,
        //                                Apellido = p.Apellido,
        //                                FechaCita = c.fecha_cita,
        //                                Estado = c.motivo,
        //                            }).ToList();

        //    ViewData["listadoPacientes"] = listadoPacientes;
        //    return View(Enfermeros);
        //}
    }
}
