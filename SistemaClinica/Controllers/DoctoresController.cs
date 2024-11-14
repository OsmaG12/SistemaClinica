using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemaClinica.Modelos;
using SistemaClinica.Models;
using System.Text.Json;

namespace SistemaClinica.Controllers
{
    public class DoctoresController : Controller
    {
        private readonly DBContext _context;

        public DoctoresController(DBContext context)
        {
            _context = context;
        }



        public IActionResult Doctores()
        {


            var listadoPacientes = (from p in _context.paciente
                                    join h in _context.historial on p.id_paciente equals h.id_paciente
                                    select new
                                    {
                                        id_paciente = p.id_paciente,
                                        Nombre = p.nombre,
                                        Apellido = p.apellido,
                                        FechaCita = h.fecha,
                                        IdPaciente = p.id_paciente,
                                    }).ToList();

            ViewData["listadoPacientes"] = listadoPacientes;
            return View(Doctores);


        }


        [HttpGet]
        public IActionResult Expediente(int id_paciente)
        {
            var paciente = (from p in _context.paciente
                            join h in _context.historial on p.id_paciente equals h.id_paciente
                            where p.id_paciente == id_paciente
                            select new
                            {
                                id_paciente = p.id_paciente,
                                Nombre = p.nombre, // Propiedad en minúsculas
                                Apellido = p.apellido,
                                Edad = DateTime.Now.Year - p.fecha_nacimiento.Year,  // Calcula la edad
                                Peso = p.peso,
                                Altura = p.altura,
                                Telefono = p.telefono,
                                Fecha_nacimiento = p.fecha_nacimiento,
                                Motivo = p.motivo,
                                Observacion = h.observacion // Asume que h.observacion existe
                            }).FirstOrDefault();

            // Asegúrate de pasar los datos a ViewData con la clave "cumplimientodatos"
            ViewData["cumplimientodatos"] = new[] { paciente };
            return View();
        }
    }
}