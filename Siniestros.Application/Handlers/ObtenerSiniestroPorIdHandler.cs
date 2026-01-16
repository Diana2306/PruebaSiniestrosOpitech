using MediatR;
using Microsoft.EntityFrameworkCore;
using Siniestros.Application.Dtos;
using Siniestros.Application.Persistence;
using Siniestros.Application.Queries;
using Siniestros.Application.Repositories;

namespace Siniestros.Application.Handlers;

/// <summary>
/// Handler que procesa la consulta para obtener un siniestro específico por su identificador único.
/// Si el siniestro no existe, retorna null en lugar de lanzar una excepción.
/// </summary>
public sealed class ObtenerSiniestroPorIdHandler
    : IRequestHandler<ObtenerSiniestroPorIdQuery, SiniestroDto?>
{
    private readonly ISiniestrosDbContext _db;
    private readonly ISiniestroRepository _siniestroRepository;

    /// <summary>
    /// Inicializa una nueva instancia del handler con las dependencias necesarias.
    /// </summary>
    /// <param name="db">Contexto de base de datos para consultar siniestros.</param>
    /// <param name="siniestroRepository">Repositorio de siniestros para obtener el siniestro por ID.</param>
    public ObtenerSiniestroPorIdHandler(ISiniestrosDbContext db, ISiniestroRepository siniestroRepository)
    {
        _db = db;
        _siniestroRepository = siniestroRepository;
    }

    /// <summary>
    /// Procesa la consulta para obtener un siniestro por su identificador único.
    /// Incluye las relaciones con Ciudad, Departamento y TipoSiniestro para construir el DTO completo.
    /// </summary>
    /// <param name="request">Query con el identificador del siniestro a buscar.</param>
    /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
    /// <returns>
    /// El DTO del siniestro si existe, o null si no se encuentra.
    /// </returns>
    public async Task<SiniestroDto?> Handle(
        ObtenerSiniestroPorIdQuery request,
        CancellationToken cancellationToken)
    {
        var siniestro = await _siniestroRepository.GetByIdAsync(request.Id, cancellationToken);

        if (siniestro is null)
            return null;

        return new SiniestroDto
        {
            Id = siniestro.Id,
            FechaHora = siniestro.FechaHora,
            IdCiudad = siniestro.Ciudad.Id,
            Ciudad = siniestro.Ciudad.Nombre,
            IdDepartamento = siniestro.Ciudad.Departamento.Id,
            Departamento = siniestro.Ciudad.Departamento.Nombre,
            IdTipoSiniestro = siniestro.TipoSiniestro.Id,
            TipoSiniestro = siniestro.TipoSiniestro.Nombre,
            VehiculosInvolucrados = siniestro.VehiculosInvolucrados,
            NumeroVictimas = siniestro.NumeroVictimas,
            Descripcion = siniestro.Descripcion
        };
    }
}