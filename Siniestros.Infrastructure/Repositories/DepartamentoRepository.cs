using Microsoft.EntityFrameworkCore;
using Siniestros.Application.Persistence;
using Siniestros.Application.Repositories;
using Siniestros.Domain.Entities;

namespace Siniestros.Infrastructure.Repositories;

/// <summary>
/// Implementación del repositorio para la entidad Departamento utilizando Entity Framework Core.
/// Esta clase proporciona acceso a datos para departamentos, incluyendo operaciones
/// de consulta por código DIVIPOLA o nombre, y verificación de existencia.
/// </summary>
public sealed class DepartamentoRepository : IDepartamentoRepository
{
    private readonly ISiniestrosDbContext _context;

    /// <summary>
    /// Inicializa una nueva instancia del repositorio con el contexto de base de datos.
    /// </summary>
    /// <param name="context">Contexto de base de datos para acceder a los departamentos.</param>
    public DepartamentoRepository(ISiniestrosDbContext context)
    {
        _context = context;
    }

    public async Task<Departamento?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Departamentos
            .Include(d => d.Ciudades)
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public async Task<Departamento?> GetByNombreAsync(string nombre, CancellationToken cancellationToken = default)
    {
        var nombreLower = nombre.Trim().ToLower();
        return await _context.Departamentos
            .Include(d => d.Ciudades)
            .FirstOrDefaultAsync(d => d.Nombre.ToLower() == nombreLower, cancellationToken);
    }

    public async Task<IEnumerable<Departamento>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Departamentos
            .Include(d => d.Ciudades)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Departamentos.AnyAsync(d => d.Id == id, cancellationToken);
    }
}
