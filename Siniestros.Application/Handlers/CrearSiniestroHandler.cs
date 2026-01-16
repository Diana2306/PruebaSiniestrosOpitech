using MediatR;
using Siniestros.Application.Commands;
using Siniestros.Application.Persistence;
using Siniestros.Application.Repositories;
using Siniestros.Domain.Entities;

namespace Siniestros.Application.Handlers;

/// <summary>
/// Handler que procesa el comando para crear un nuevo siniestro vial.
/// Este handler valida que la ciudad y el tipo de siniestro existan antes de crear el siniestro,
/// y luego persiste la nueva entidad en la base de datos.
/// </summary>
public sealed class CrearSiniestroHandler
    : IRequestHandler<CrearSiniestroCommand, Guid>
{
    private readonly ISiniestrosDbContext _db;
    private readonly ICiudadRepository _ciudadRepository;
    private readonly ITipoSiniestroRepository _tipoSiniestroRepository;
    private readonly ISiniestroRepository _siniestroRepository;

    /// <summary>
    /// Inicializa una nueva instancia del handler con las dependencias necesarias.
    /// </summary>
    /// <param name="db">Contexto de base de datos para guardar cambios.</param>
    /// <param name="ciudadRepository">Repositorio para validar la existencia de ciudades.</param>
    /// <param name="tipoSiniestroRepository">Repositorio para validar la existencia de tipos de siniestro.</param>
    /// <param name="siniestroRepository">Repositorio para agregar el nuevo siniestro.</param>
    public CrearSiniestroHandler(
        ISiniestrosDbContext db,
        ICiudadRepository ciudadRepository,
        ITipoSiniestroRepository tipoSiniestroRepository,
        ISiniestroRepository siniestroRepository)
    {
        _db = db;
        _ciudadRepository = ciudadRepository;
        _tipoSiniestroRepository = tipoSiniestroRepository;
        _siniestroRepository = siniestroRepository;
    }

    /// <summary>
    /// Procesa el comando para crear un nuevo siniestro.
    /// Realiza validaciones de existencia de ciudad y tipo de siniestro,
    /// crea la entidad de dominio y la persiste en la base de datos.
    /// </summary>
    /// <param name="request">Comando con la información del siniestro a crear.</param>
    /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
    /// <returns>El identificador único (Guid) del siniestro creado.</returns>
    /// <exception cref="ArgumentException">
    /// Se lanza si la ciudad o el tipo de siniestro especificados no existen en la base de datos.
    /// </exception>
    public async Task<Guid> Handle(CrearSiniestroCommand request, CancellationToken cancellationToken)
    {
        // Validar ciudad (código DIVIPOLA)
        var ciudadExiste = await _ciudadRepository.ExistsAsync(request.IdCiudad, cancellationToken);

        if (!ciudadExiste)
            throw new ArgumentException("La ciudad enviada no existe.");

        // Validar tipo siniestro
        var tipoExiste = await _tipoSiniestroRepository.ExistsAsync(request.IdTipoSiniestro, cancellationToken);

        if (!tipoExiste)
            throw new ArgumentException("El tipo de siniestro enviado no existe.");

        // Crear entidad
        var siniestro = Siniestro.Crear(
            request.FechaHora,
            request.IdCiudad,
            request.IdTipoSiniestro,
            request.VehiculosInvolucrados,
            request.NumeroVictimas,
            request.Descripcion
        );

        await _siniestroRepository.AddAsync(siniestro, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return siniestro.Id;
    }
}