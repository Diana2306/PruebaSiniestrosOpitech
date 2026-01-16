using Microsoft.EntityFrameworkCore;
using Siniestros.Application.Persistence;
using Siniestros.Application.Repositories;
using Siniestros.Domain.Entities;

namespace Siniestros.Infrastructure.Repositories;

/// <summary>
/// Implementación del repositorio para la entidad TipoSiniestro utilizando Entity Framework Core.
/// Esta clase proporciona acceso a datos para tipos de siniestros viales, incluyendo operaciones
/// de consulta y verificación de existencia.
/// </summary>
public sealed class TipoSiniestroRepository : ITipoSiniestroRepository
{
    private readonly ISiniestrosDbContext _context;

    /// <summary>
    /// Inicializa una nueva instancia del repositorio con el contexto de base de datos.
    /// </summary>
    /// <param name="context">Contexto de base de datos para acceder a los tipos de siniestros.</param>
    public TipoSiniestroRepository(ISiniestrosDbContext context)
    {
        _context = context;
    }

    public async Task<TipoSiniestro?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.TiposSiniestro
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<TipoSiniestro>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.TiposSiniestro
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.TiposSiniestro.AnyAsync(t => t.Id == id, cancellationToken);
    }
}
