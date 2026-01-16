using Microsoft.EntityFrameworkCore;
using Siniestros.Application.Persistence;
using Siniestros.Application.Repositories;
using Siniestros.Domain.Entities;

namespace Siniestros.Infrastructure.Repositories;

/// <summary>
/// Implementación del repositorio para la entidad Ciudad utilizando Entity Framework Core.
/// Esta clase proporciona acceso a datos para ciudades, incluyendo operaciones
/// de consulta y verificación de existencia basadas en códigos DIVIPOLA.
/// </summary>
public sealed class CiudadRepository : ICiudadRepository
{
    private readonly ISiniestrosDbContext _context;

    /// <summary>
    /// Inicializa una nueva instancia del repositorio con el contexto de base de datos.
    /// </summary>
    /// <param name="context">Contexto de base de datos para acceder a las ciudades.</param>
    public CiudadRepository(ISiniestrosDbContext context)
    {
        _context = context;
    }

    public async Task<Ciudad?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Ciudades
            .Include(c => c.Departamento)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Ciudad>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Ciudades
            .Include(c => c.Departamento)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Ciudad>> GetByDepartamentoAsync(string idDepartamento, CancellationToken cancellationToken = default)
    {
        return await _context.Ciudades
            .Include(c => c.Departamento)
            .Where(c => c.IdDepartamento == idDepartamento)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Ciudades.AnyAsync(c => c.Id == id, cancellationToken);
    }
}
