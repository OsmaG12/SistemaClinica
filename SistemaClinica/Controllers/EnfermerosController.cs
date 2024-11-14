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
            var datosPaciente = (from p in _context.paciente
                                 join h in _context.historial on p.id_paciente equals h.id_paciente
                                 where p.id_paciente == id_paciente
                                 select new
                                 {
                                     nombre = p.nombre,
                                     apellido = p.apellido,
                                     diagnostico = h.diagnostico,
                                     motivo = p.motivo,
                                     observacion = h.observacion
                                 }).FirstOrDefault();

            if (datosPaciente == null)
            {
                return NotFound("Datos del paciente no encontrados.");
            }

            using (var ms = new MemoryStream())
            {
                PdfDocument pdfDocument = new PdfDocument();
                pdfDocument.Info.Title = "Reporte del Paciente";
                PdfPage page = pdfDocument.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Cargar y centrar la imagen con un margen inferior
                string imagePath = "C:\\Users\\oscar\\OneDrive\\Escritorio\\SC-Borrador\\SistemaClinica\\wwwroot\\img\\logo.jpg";
                XImage image = XImage.FromFile(imagePath);
                gfx.DrawImage(image, (page.Width - image.PixelWidth) / 2, 10); // Centrado en X, margen superior de 10

                // Espacio adicional debajo de la imagen
                double imageMarginBottom = 20; // Ajusta este valor para el margen inferior deseado

                // Establecer la fuente y margen inicial para el texto
                XFont font = new XFont("Verdana", 12);
                double marginTop = 10 + image.PixelHeight + imageMarginBottom;

                // Centrar y escribir el texto
                string[] lines = {
            $"Nombre: {datosPaciente.nombre}",
            $"Apellido: {datosPaciente.apellido}",
            $"Diagnóstico: {datosPaciente.diagnostico}",
            $"Motivo: {datosPaciente.motivo}",
            $"Observaciones: {datosPaciente.observacion}"
        };

                foreach (string line in lines)
                {
                    double textWidth = gfx.MeasureString(line, font).Width;
                    gfx.DrawString(line, font, XBrushes.Black, (page.Width - textWidth) / 2, marginTop);
                    marginTop += 20; // Espacio entre líneas
                }

                pdfDocument.Save(ms);
                ms.Position = 0;

                return File(ms.ToArray(), "application/pdf", $"Reporte_Paciente_{datosPaciente.nombre}.pdf");
            }
        }

    }
}
