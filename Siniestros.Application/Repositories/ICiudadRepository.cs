using Siniestros.Domain.Entities;

namespace Siniestros.Application.Repositories;

/// <summary>
/// Interfaz del repositorio para la entidad Ciudad.
/// Define las operaciones de acceso a datos para ciudades,
/// siguiendo el patrón Repository para abstraer la lógica de persistencia.
/// </summary>
public interface ICiudadRepository
{
    /// <summary>
    /// Obtiene una ciudad por su código DIVIPOLA, incluyendo su relación con el departamento.
    /// </summary>
    /// <param name="id">Código DIVIPOLA de la ciudad (5 dígitos).</param>
    /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
    /// <returns>La ciudad encontrada o null si no existe.</returns>
    Task<Ciudad?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todas las ciudades del sistema, incluyendo su relación con el departamento.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
    /// <returns>Colección de todas las ciudades.</returns>
    Task<IEnumerable<Ciudad>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todas las ciudades que pertenecen a un departamento específico.
    /// </summary>
    /// <param name="idDepartamento">Código DIVIPOLA del departamento (2 dígitos).</param>
    /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
    /// <returns>Colección de ciudades del departamento especificado.</returns>
    Task<IEnumerable<Ciudad>> GetByDepartamentoAsync(string idDepartamento, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica si existe una ciudad con el código DIVIPOLA especificado.
    /// </summary>
    /// <param name="id">Código DIVIPOLA de la ciudad a verificar (5 dígitos).</param>
    /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
    /// <returns>True si la ciudad existe, false en caso contrario.</returns>
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
