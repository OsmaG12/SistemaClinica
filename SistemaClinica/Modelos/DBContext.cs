using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
namespace SistemaClinica.Modelos
{
    public class DBContext : DbContext
    {

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        // Tablas de la base de datos(DbSet)
        public DbSet<usuario> usuario { get; set; }
        public DbSet<administrador> administrador { get; set; }
        public DbSet<secretaria> secretaria { get; set; }
        public DbSet<enfermera> enfermera { get; set; }
        public DbSet<medico> medico { get; set; }
        public DbSet<paciente> paciente { get; set; }
        public DbSet<cita> cita { get; set; }
        public DbSet<historialmedico> historialesMedico { get; set; }
        public DbSet<farmacia> farmacia { get; set; }

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
