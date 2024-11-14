using PdfSharp.Pdf;
using PdfSharp.Drawing;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.IO;
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

        public async Task<IActionResult> Enfermeros()
        {
            var listadoPacientes = (from p in _context.paciente
                                    join h in _context.historial on p.id_paciente equals h.id_paciente
                                    select new
                                    {
                                        id_paciente = p.id_paciente,
                                        Nombre = p.nombre,
                                        Apellido = p.apellido,
                                        FechaCita = h.fecha,
                                        Estado = p.motivo,
                                    }).ToList();

            ViewData["listadoPacientes"] = listadoPacientes;
            return View(Enfermeros);
        }

        public async Task<IActionResult> cumplimiento(int id_paciente)
        {
            // obtener el cumplimiento de farmacia
            var cumplimientodatos = (from p in _context.paciente
                                     join h in _context.historial on p.id_paciente equals h.id_paciente
                                     where p.id_paciente == id_paciente
                                     select new
                                     {
                                         id_paciente = p.id_paciente,
                                         nombre = p.nombre,
                                         apellido = p.apellido,
                                         diagnostico = h.diagnostico,
                                         motivo = p.motivo,
                                         peso = p.peso,
                                         observacion = h.observacion // Asegúrate de que este campo existe
                                     }).FirstOrDefault();

            ViewData["cumplimientodatos"] = new[] { cumplimientodatos };
            return View();
        }

        [HttpPost]
        public IActionResult GenerarReporte(int id_paciente)
        {
            // Recupera los datos del paciente
            var datosPaciente = (from p in _context.paciente
                                 join h in _context.historial on p.id_paciente equals h.id_paciente
                                 where p.id_paciente == id_paciente
                                 select new
                                 {
                                     nombre = p.nombre,
                                     apellido = p.apellido,
                                     diagnostico = h.diagnostico,
                                     motivo = p.motivo,
                                     peso = p.peso,
                                     observacion = h.observacion
                                 }).FirstOrDefault();

            if (datosPaciente == null)
            {
                return NotFound("Datos del paciente no encontrados.");
            }

            // Crear un documento PDF
            using (var ms = new MemoryStream())
            {
                // Crear el documento PDF
                PdfDocument pdfDocument = new PdfDocument();
                pdfDocument.Info.Title = "Reporte del Paciente";

                // Crear una página
                PdfPage page = pdfDocument.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Establecer la fuente
                XFont font = new XFont("Verdana", 12);

                // Escribir los datos en el PDF
                gfx.DrawString($"Nombre: {datosPaciente.nombre}", font, XBrushes.Black, 10, 20);
                gfx.DrawString($"Apellido: {datosPaciente.apellido}", font, XBrushes.Black, 10, 40);
                gfx.DrawString($"Diagnóstico: {datosPaciente.diagnostico}", font, XBrushes.Black, 10, 60);
                gfx.DrawString($"Motivo: {datosPaciente.motivo}", font, XBrushes.Black, 10, 80);
                gfx.DrawString($"Peso: {datosPaciente.peso}", font, XBrushes.Black, 10, 100);
                gfx.DrawString($"Observaciones: {datosPaciente.observacion}", font, XBrushes.Black, 10, 120);

                // Guardar el PDF en el MemoryStream
                pdfDocument.Save(ms);

                // Restablecer la posición del MemoryStream antes de enviarlo
                ms.Position = 0;

                // Enviar el archivo PDF al cliente
                return File(ms.ToArray(), "application/pdf", "Reporte_Paciente.pdf");
            }
        }
    }
}
