using System.Linq;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Siniestros.Application.Common;
using Siniestros.Application.Dtos;
using Siniestros.Application.Persistence;
using Siniestros.Application.Queries;
using Siniestros.Application.Repositories;

namespace Siniestros.Application.Handlers;

/// <summary>
/// Handler que procesa la consulta para buscar siniestros con filtros opcionales y paginación.
/// Este handler implementa una estrategia híbrida de filtrado:
/// - Los filtros de fecha se aplican en SQL para eficiencia
/// - Los filtros de texto (departamento y ciudad) se aplican en memoria para permitir
///   búsquedas flexibles que ignoran mayúsculas, acentos y espacios.
/// </summary>
public sealed class ConsultarSiniestrosHandler
    : IRequestHandler<ConsultarSiniestrosQuery, PagedResult<SiniestroDto>>
{
    private readonly ISiniestrosDbContext _db;
    private readonly ISiniestroRepository _siniestroRepository;

    /// <summary>
    /// Inicializa una nueva instancia del handler con las dependencias necesarias.
    /// </summary>
    /// <param name="db">Contexto de base de datos para consultar siniestros.</param>
    /// <param name="siniestroRepository">Repositorio de siniestros (actualmente no utilizado, se usa directamente el contexto).</param>
    public ConsultarSiniestrosHandler(ISiniestrosDbContext db, ISiniestroRepository siniestroRepository)
    {
        _db = db;
        _siniestroRepository = siniestroRepository;
    }

    /// <summary>
    /// Procesa la consulta de siniestros aplicando los filtros especificados y retornando resultados paginados.
    /// </summary>
    /// <param name="request">Query con los filtros y parámetros de paginación.</param>
    /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
    /// <returns>
    /// Un resultado paginado con los siniestros que cumplen los criterios de búsqueda.
    /// Los resultados se ordenan por fecha y hora descendente (más recientes primero).
    /// </returns>
    public async Task<PagedResult<SiniestroDto>> Handle(
        ConsultarSiniestrosQuery request,
        CancellationToken cancellationToken)
    {
        var page = request.Page < 1 ? 1 : request.Page;
        var pageSize = request.PageSize < 1 ? 10 : request.PageSize;
        if (pageSize > 100) pageSize = 100;

        // Construir query base usando repositorio
        var query = _db.Siniestros
            .AsNoTracking()
            .Include(x => x.Ciudad)
                .ThenInclude(c => c.Departamento)
            .Include(x => x.TipoSiniestro)
            .AsQueryable();

        // Normalizar términos de búsqueda para búsqueda flexible
        var depSearchNormalized = !string.IsNullOrWhiteSpace(request.Departamento) 
            ? StringHelper.NormalizeForSearch(request.Departamento) 
            : null;
        var ciuSearchNormalized = !string.IsNullOrWhiteSpace(request.Ciudad) 
            ? StringHelper.NormalizeForSearch(request.Ciudad) 
            : null;

        // Filtro por fechas en SQL (eficiente)
        if (request.Desde.HasValue)
            query = query.Where(x => x.FechaHora >= request.Desde.Value);

        if (request.Hasta.HasValue)
        {
            var hasta = request.Hasta.Value;
            if (hasta.TimeOfDay == TimeSpan.Zero)
                hasta = hasta.Date.AddDays(1).AddTicks(-1);

            query = query.Where(x => x.FechaHora <= hasta);
        }

        // Cargar datos con relaciones (sin filtro de texto en SQL para permitir búsqueda flexible)
        var allItems = await query
            .OrderByDescending(x => x.FechaHora)
            .Select(x => new SiniestroDto
            {
                Id = x.Id,
                FechaHora = x.FechaHora,
                IdCiudad = x.Ciudad.Id,
                Ciudad = x.Ciudad.Nombre,
                IdDepartamento = x.Ciudad.Departamento.Id,
                Departamento = x.Ciudad.Departamento.Nombre,
                IdTipoSiniestro = x.TipoSiniestro.Id,
                TipoSiniestro = x.TipoSiniestro.Nombre,
                VehiculosInvolucrados = x.VehiculosInvolucrados,
                NumeroVictimas = x.NumeroVictimas,
                Descripcion = x.Descripcion
            })
            .ToListAsync(cancellationToken);

        // Filtrado en memoria con normalización completa (maneja acentos, espacios, case-insensitive)
        var filteredItems = allItems.AsEnumerable();

        if (depSearchNormalized != null)
        {
            filteredItems = filteredItems.Where(x => 
                StringHelper.NormalizeForSearch(x.Departamento).Contains(depSearchNormalized));
        }

        if (ciuSearchNormalized != null)
        {
            filteredItems = filteredItems.Where(x => 
                StringHelper.NormalizeForSearch(x.Ciudad).Contains(ciuSearchNormalized));
        }

        var filteredList = filteredItems.ToList();
        var totalItems = filteredList.Count;

        // Aplicar paginación
        var items = filteredList
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return PagedResult<SiniestroDto>.Create(items, totalItems, page, pageSize);
    }
}