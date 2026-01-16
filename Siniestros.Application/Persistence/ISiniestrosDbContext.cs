using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Siniestros.Domain.Entities;

namespace Siniestros.Application.Persistence
{
    /// <summary>
    /// Interfaz que define el contrato del contexto de base de datos para el sistema de siniestros.
    /// Esta interfaz abstrae el acceso a las entidades y permite la inyección de dependencias
    /// sin acoplar el código a la implementación específica de Entity Framework Core.
    /// </summary>
    public interface ISiniestrosDbContext
    {
        /// <summary>
        /// Acceso a la colección de siniestros viales en la base de datos.
        /// </summary>
        DbSet<Siniestro> Siniestros { get; }

        /// <summary>
        /// Acceso a la colección de ciudades en la base de datos.
        /// </summary>
        DbSet<Ciudad> Ciudades { get; }

        /// <summary>
        /// Acceso a la colección de departamentos en la base de datos.
        /// </summary>
        DbSet<Departamento> Departamentos { get; }

        /// <summary>
        /// Acceso a la colección de tipos de siniestros en la base de datos.
        /// </summary>
        DbSet<TipoSiniestro> TiposSiniestro { get; }

        /// <summary>
        /// Guarda todos los cambios realizados en el contexto de forma asíncrona.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
        /// <returns>Número de entidades afectadas por los cambios guardados.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
