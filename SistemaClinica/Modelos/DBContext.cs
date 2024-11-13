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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Cargar configuración desde appsettings.json
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                string connectionString = configuration.GetConnectionString("clinicaConexion");

                optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 0)));
            }
        }

    }
}
