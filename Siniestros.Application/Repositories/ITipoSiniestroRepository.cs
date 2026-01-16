using Siniestros.Domain.Entities;

namespace Siniestros.Application.Repositories;

/// <summary>
/// Interfaz del repositorio para la entidad TipoSiniestro.
/// Define las operaciones de acceso a datos para tipos de siniestros viales,
/// siguiendo el patrón Repository para abstraer la lógica de persistencia.
/// </summary>
public interface ITipoSiniestroRepository
{
    /// <summary>
    /// Obtiene un tipo de siniestro por su identificador único.
    /// </summary>
    /// <param name="id">Identificador del tipo de siniestro.</param>
    /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
    /// <returns>El tipo de siniestro encontrado o null si no existe.</returns>
    Task<TipoSiniestro?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todos los tipos de siniestros disponibles en el sistema.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
    /// <returns>Colección de todos los tipos de siniestros.</returns>
    Task<IEnumerable<TipoSiniestro>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica si existe un tipo de siniestro con el identificador especificado.
    /// </summary>
    /// <param name="id">Identificador del tipo de siniestro a verificar.</param>
    /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
    /// <returns>True si el tipo de siniestro existe, false en caso contrario.</returns>
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
}
