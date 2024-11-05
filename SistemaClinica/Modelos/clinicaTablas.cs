using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SistemaClinica.Modelos
{
    public class clinicaTablas
    {
    }

    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string TipoUsuario { get; set; }

        // Relaciones
        public virtual ICollection<Administrador> Administradores { get; set; }
        public virtual ICollection<Secretaria> Secretarias { get; set; }
        public virtual ICollection<Enfermera> Enfermeras { get; set; }
        public virtual ICollection<Medico> Medicos { get; set; }
        public virtual ICollection<Paciente> Pacientes { get; set; }
        public virtual ICollection<Farmacia> Farmacias { get; set; }
    }

    public class Administrador
    {
        [Key]
        public int IdAdministrador { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }
    }

    public class Secretaria
    {
        [Key]
        public int IdSecretaria { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }
    }

    public class Enfermera
    {
        [Key]
        public int IdEnfermera { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Turno { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }
    }

    public class Medico
    {
        [Key]
        public int IdMedico { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Especialidad { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<HistorialMedico> HistorialMedico { get; set; }
        public virtual ICollection<Cita> Citas { get; set; }
    }

    public class Paciente
    {
        [Key]
        public int IdPaciente { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Apellido { get; set; }
        public string Peso { get; set; }
        public string Altura { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Email { get; set; }

        public virtual ICollection<HistorialMedico> HistorialMedico { get; set; }
        public virtual ICollection<Cita> Citas { get; set; }
    }

    public class Cita
    {
        [Key]
        public int IdCita { get; set; }

        [ForeignKey("Paciente")]
        public int IdPaciente { get; set; }

        [ForeignKey("Medico")]
        public int IdMedico { get; set; }

        public DateTime FechaCita { get; set; }
        public TimeSpan Hora { get; set; }
        public string Motivo { get; set; }

        public virtual Paciente Paciente { get; set; }
        public virtual Medico Medico { get; set; }
    }

    public class HistorialMedico
    {
        [Key]
        public int IdHistorial { get; set; }

        [ForeignKey("Paciente")]
        public int IdPaciente { get; set; }

        [ForeignKey("Medico")]
        public int IdMedico { get; set; }

        public DateTime Fecha { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }

        public virtual Paciente Paciente { get; set; }
        public virtual Medico Medico { get; set; }
    }

    public class Farmacia
    {
        [Key]
        public int IdMedicamento { get; set; }
        public string NombreMedicamento { get; set; }
        public int Cantidad { get; set; }
        public string Presentacion { get; set; }
        public DateTime FechaExpiracion { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
