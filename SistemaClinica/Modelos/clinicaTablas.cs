using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SistemaClinica.Modelos
{
    public class clinicaTablas
    {
    }

    public class usuario
    {
        [Key]
        public int id_usuario { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string tipo_usuario { get; set; }

        // Relaciones
        public virtual ICollection<administrador> administrador { get; set; }
        public virtual ICollection<secretaria> secretaria { get; set; }
        public virtual ICollection<enfermera> enfermera { get; set; }
        public virtual ICollection<medico> medico { get; set; }
        public virtual ICollection<paciente> paciente { get; set; }
        public virtual ICollection<farmacia> farmacia { get; set; }
    }

    public class administrador
    {
        [Key]
        public int id_administrador { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }

        [ForeignKey("usuario")]
        public int id_usuario { get; set; }
        public virtual usuario usuario { get; set; }
    }

    public class secretaria
    {
        [Key]
        public int id_secretaria { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }

        [ForeignKey("usuario")]
        public int id_usuario { get; set; }
        public virtual usuario usuario { get; set; }
    }

    public class enfermera
    {
        [Key]
        public int id_enfermera { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string turno { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }

        [ForeignKey("usuario")]
        public int id_usuario { get; set; }
        public virtual usuario usuario { get; set; }
    }

    public class medico
    {
        [Key]
        public int id_medico { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string especialidad { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }

        [ForeignKey("usuario")]
        public int id_usuario { get; set; }
        public virtual usuario usuario { get; set; }
        public virtual ICollection<historialmedico> historialmedico { get; set; }
        public virtual ICollection<cita> cita { get; set; }
    }

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

        public virtual ICollection<historialmedico> historialmedico { get; set; }
        public virtual ICollection<cita> cita { get; set; }
    }

    public class cita
    {
        [Key]
        public int id_cita { get; set; }

        [ForeignKey("paciente")]
        public int id_paciente { get; set; }

        [ForeignKey("medico")]
        public int id_medico { get; set; }

        public DateTime fecha_cita { get; set; }
        public TimeSpan hora { get; set; }
        public string motivo { get; set; }

        public virtual paciente paciente { get; set; }
        public virtual medico medico { get; set; }
    }

    public class historialmedico
    {
        [Key]
        public int id_historial { get; set; }

        [ForeignKey("paciente")]
        public int id_paciente { get; set; }

        [ForeignKey("medico")]
        public int id_medico { get; set; }

        public DateTime fecha { get; set; }
        public string diagnostico { get; set; }
        public string tratamiento { get; set; }

        public virtual paciente paciente { get; set; }
        public virtual medico medico { get; set; }
    }

    public class farmacia
    {
        [Key]
        public int id_medicamento { get; set; }
        public string nombre_medicamento { get; set; }
        public int cantidad { get; set; }
        public string presentacion { get; set; }
        public DateTime fecha_expiracion { get; set; }

        [ForeignKey("usuario")]
        public int id_usuario { get; set; }
        public virtual usuario usuario { get; set; }
    }
}
