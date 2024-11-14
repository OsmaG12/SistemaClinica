using Microsoft.EntityFrameworkCore;
using SistemaClinica.Models;
namespace SistemaClinica.Modelos
{
    public class DBContext : DbContext
    {

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        // Tablas de la base de datos(DbSet)
        public DbSet<paciente> paciente { get; set; }
        public DbSet<rol> rol { get; set; }
        public DbSet<empleado> empleado { get; set; }
        public DbSet<historial> historial { get; set; }

    }
}
