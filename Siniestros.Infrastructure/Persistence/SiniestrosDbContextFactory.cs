using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Siniestros.Infrastructure.Persistence
{
    /// <summary>
    /// Factory para crear instancias de SiniestrosDbContext en tiempo de diseño.
    /// Esta clase es utilizada por las herramientas de Entity Framework Core (como migrations)
    /// para crear el contexto cuando no hay un host de aplicación ejecutándose.
    /// </summary>
    public class SiniestrosDbContextFactory : IDesignTimeDbContextFactory<SiniestrosDbContext>
    {
        /// <summary>
        /// Crea una nueva instancia del contexto de base de datos con la cadena de conexión configurada.
        /// Esta cadena de conexión se utiliza únicamente para operaciones de diseño (migrations, scaffolding).
        /// </summary>
        /// <param name="args">Argumentos de línea de comandos (no utilizados en esta implementación).</param>
        /// <returns>Una nueva instancia de SiniestrosDbContext configurada para SQL Server.</returns>
        public SiniestrosDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SiniestrosDbContext>();

            // Connection string (igual a la de appsettings)
            var connectionString =
                "Server=localhost;Database=SiniestrosDb;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False;";

            optionsBuilder.UseSqlServer(connectionString);

            return new SiniestrosDbContext(optionsBuilder.Options);
        }
    }
}