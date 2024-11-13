using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaClinica.Models
{
    public class paciente
    {
        [Key]
        public int id_paciente { get; set; }

        public string nombre { get; set; }

        public string direccion { get; set; }

        public string apellido { get; set; }

        public string peso { get; set; }

        public string altura { get; set; }

        public string telefono { get; set; }

        public DateTime fecha_nacimiento { get; set; }

        public string email { get; set; }

        public string motivo { get; set; }

        // Relación con Historial
        public ICollection<historial> historiales { get; set; }
    }

    public class empleado
    {
        [Key]
        public int id_empleado { get; set; }

        public string nombre { get; set; }

        public string correo { get; set; }

        public int id_rol { get; set; }

        [ForeignKey("id_rol")]
        public rol rol { get; set; }

        public string telefono { get; set; }

        public string usuario { get; set; }

        public string contrasenya { get; set; }

        public string direccion { get; set; }

        // Relación con Historial
        public ICollection<historial> historiales { get; set; }
    }

    public class rol
    {
        [Key]
        public int id_rol { get; set; }

        public string nombre_rol { get; set; }

        // Relación con Empleado
        public ICollection<empleado> empleado { get; set; }
    }

    public class historial
    {
        [Key]
        public int id_historial { get; set; }

        public int id_paciente { get; set; }

        [ForeignKey("id_paciente")]
        public paciente paciente { get; set; }

        public int id_empleado { get; set; }

        [ForeignKey("id_empleado")]
        public empleado empleado { get; set; }

        public DateTime fecha { get; set; }

        public string diagnostico { get; set; }

        public string observacion { get; set; }
    }
}