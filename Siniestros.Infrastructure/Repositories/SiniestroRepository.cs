using Microsoft.EntityFrameworkCore;
using Siniestros.Application.Persistence;
using Siniestros.Application.Repositories;
using Siniestros.Domain.Entities;

namespace Siniestros.Infrastructure.Repositories;

/// <summary>
/// Implementación del repositorio para la entidad Siniestro utilizando Entity Framework Core.
/// Esta clase proporciona acceso a datos para siniestros viales, incluyendo operaciones
/// de consulta, agregación y verificación de existencia.
/// </summary>
public sealed class SiniestroRepository : ISiniestroRepository
{
    private readonly ISiniestrosDbContext _context;

    /// <summary>
    /// Inicializa una nueva instancia del repositorio con el contexto de base de datos.
    /// </summary>
    /// <param name="context">Contexto de base de datos para acceder a los siniestros.</param>
    public SiniestroRepository(ISiniestrosDbContext context)
    {
        _context = context;
    }

    public async Task<Siniestro?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Siniestros
            .Include(x => x.Ciudad)
                .ThenInclude(c => c.Departamento)
            .Include(x => x.TipoSiniestro)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Siniestro>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Siniestros
            .Include(x => x.Ciudad)
                .ThenInclude(c => c.Departamento)
            .Include(x => x.TipoSiniestro)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Siniestro>> GetByDateRangeAsync(DateTime desde, DateTime hasta, CancellationToken cancellationToken = default)
    {
        return await _context.Siniestros
            .Include(x => x.Ciudad)
                .ThenInclude(c => c.Departamento)
            .Include(x => x.TipoSiniestro)
            .Where(x => x.FechaHora >= desde && x.FechaHora <= hasta)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Siniestro>> GetByDepartamentoAsync(string nombreDepartamento, CancellationToken cancellationToken = default)
    {
        var nombreLower = nombreDepartamento.Trim().ToLower();
        return await _context.Siniestros
            .Include(x => x.Ciudad)
                .ThenInclude(c => c.Departamento)
            .Include(x => x.TipoSiniestro)
            .Where(x => x.Ciudad.Departamento.Nombre.ToLower() == nombreLower)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Siniestro>> GetByCiudadAsync(string nombreCiudad, CancellationToken cancellationToken = default)
    {
        var nombreLower = nombreCiudad.Trim().ToLower();
        return await _context.Siniestros
            .Include(x => x.Ciudad)
                .ThenInclude(c => c.Departamento)
            .Include(x => x.TipoSiniestro)
            .Where(x => x.Ciudad.Nombre.ToLower() == nombreLower)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Siniestros.CountAsync(cancellationToken);
    }

    public async Task AddAsync(Siniestro siniestro, CancellationToken cancellationToken = default)
    {
        await _context.Siniestros.AddAsync(siniestro, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Siniestros.AnyAsync(x => x.Id == id, cancellationToken);
    }
}
