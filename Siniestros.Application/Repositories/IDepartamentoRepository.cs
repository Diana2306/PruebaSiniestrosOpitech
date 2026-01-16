using Siniestros.Domain.Entities;

namespace Siniestros.Application.Repositories;

/// <summary>
/// Interfaz del repositorio para la entidad Departamento.
/// Define las operaciones de acceso a datos para departamentos,
/// siguiendo el patrón Repository para abstraer la lógica de persistencia.
/// </summary>
public interface IDepartamentoRepository
{
    /// <summary>
    /// Obtiene un departamento por su código DIVIPOLA, incluyendo sus ciudades relacionadas.
    /// </summary>
    /// <param name="id">Código DIVIPOLA del departamento (2 dígitos).</param>
    /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
    /// <returns>El departamento encontrado o null si no existe.</returns>
    Task<Departamento?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene un departamento por su nombre, incluyendo sus ciudades relacionadas.
    /// La búsqueda es case-insensitive.
    /// </summary>
    /// <param name="nombre">Nombre del departamento a buscar.</param>
    /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
    /// <returns>El departamento encontrado o null si no existe.</returns>
    Task<Departamento?> GetByNombreAsync(string nombre, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todos los departamentos del sistema, incluyendo sus ciudades relacionadas.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
    /// <returns>Colección de todos los departamentos.</returns>
    Task<IEnumerable<Departamento>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica si existe un departamento con el código DIVIPOLA especificado.
    /// </summary>
    /// <param name="id">Código DIVIPOLA del departamento a verificar (2 dígitos).</param>
    /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
    /// <returns>True si el departamento existe, false en caso contrario.</returns>
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
