using Siniestros.Domain.Entities;

namespace Siniestros.Application.Repositories;

/// <summary>
/// Interfaz del repositorio para la entidad Siniestro.
/// Define las operaciones de acceso a datos para siniestros viales,
/// siguiendo el patrón Repository para abstraer la lógica de persistencia.
/// </summary>
public interface ISiniestroRepository
{
    /// <summary>
    /// Obtiene un siniestro por su identificador único, incluyendo sus relaciones (Ciudad, Departamento, TipoSiniestro).
    /// </summary>
    /// <param name="id">Identificador único del siniestro (GUID).</param>
    /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
    /// <returns>El siniestro encontrado o null si no existe.</returns>
    Task<Siniestro?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todos los siniestros del sistema, incluyendo sus relaciones.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
    /// <returns>Colección de todos los siniestros.</returns>
    Task<IEnumerable<Siniestro>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todos los siniestros que ocurrieron dentro de un rango de fechas específico.
    /// </summary>
    /// <param name="desde">Fecha de inicio del rango (inclusive).</param>
    /// <param name="hasta">Fecha de fin del rango (inclusive).</param>
    /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
    /// <returns>Colección de siniestros dentro del rango de fechas.</returns>
    Task<IEnumerable<Siniestro>> GetByDateRangeAsync(DateTime desde, DateTime hasta, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todos los siniestros que ocurrieron en ciudades pertenecientes a un departamento específico.
    /// </summary>
    /// <param name="nombreDepartamento">Nombre del departamento (búsqueda case-insensitive).</param>
    /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
    /// <returns>Colección de siniestros del departamento especificado.</returns>
    Task<IEnumerable<Siniestro>> GetByDepartamentoAsync(string nombreDepartamento, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todos los siniestros que ocurrieron en una ciudad específica.
    /// </summary>
    /// <param name="nombreCiudad">Nombre de la ciudad (búsqueda case-insensitive).</param>
    /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
    /// <returns>Colección de siniestros de la ciudad especificada.</returns>
    Task<IEnumerable<Siniestro>> GetByCiudadAsync(string nombreCiudad, CancellationToken cancellationToken = default);

    /// <summary>
    /// Cuenta el total de siniestros en el sistema.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
    /// <returns>Número total de siniestros.</returns>
    Task<int> CountAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Agrega un nuevo siniestro al contexto de base de datos.
    /// Nota: Los cambios no se persisten hasta que se llame a SaveChangesAsync del contexto.
    /// </summary>
    /// <param name="siniestro">Siniestro a agregar.</param>
    /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
    Task AddAsync(Siniestro siniestro, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica si existe un siniestro con el identificador especificado.
    /// </summary>
    /// <param name="id">Identificador único del siniestro a verificar.</param>
    /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
    /// <returns>True si el siniestro existe, false en caso contrario.</returns>
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}
