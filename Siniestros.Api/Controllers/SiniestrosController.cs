using MediatR;
using Microsoft.AspNetCore.Mvc;
using Siniestros.Application.Commands;
using Siniestros.Application.Common;
using Siniestros.Application.Dtos;
using Siniestros.Application.Queries;

namespace Siniestros.Api.Controllers
{
    /// <summary>
    /// Controlador REST para gestionar siniestros viales.
    /// Este controlador expone endpoints para crear, consultar y obtener siniestros,
    /// utilizando el patrón CQRS mediante MediatR para procesar comandos y queries.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SiniestrosController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Inicializa una nueva instancia del controlador con el mediador de MediatR.
        /// </summary>
        /// <param name="mediator">Mediador de MediatR para procesar comandos y queries.</param>
        public SiniestrosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Crea un nuevo siniestro vial en el sistema.
        /// </summary>
        /// <param name="command">Comando con la información del siniestro a crear.</param>
        /// <returns>
        /// 201 Created con el identificador del siniestro creado y la ubicación del recurso,
        /// o 400 Bad Request si la validación falla.
        /// </returns>
        // POST: api/Siniestros
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Crear([FromBody] CrearSiniestroCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(ObtenerPorId), new { id }, id);
        }

        /// <summary>
        /// Obtiene un siniestro específico por su identificador único.
        /// </summary>
        /// <param name="id">Identificador único (GUID) del siniestro a consultar.</param>
        /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
        /// <returns>
        /// 200 OK con el siniestro encontrado, o 404 Not Found si no existe.
        /// </returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(SiniestroDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObtenerPorId(Guid id, CancellationToken cancellationToken)
        {
            var siniestro = await _mediator.Send(new ObtenerSiniestroPorIdQuery(id), cancellationToken);

            if (siniestro is null)
                return NotFound();

            return Ok(siniestro);
        }


        /// <summary>
        /// Consulta siniestros viales con filtros opcionales y paginación.
        /// Permite filtrar por departamento, ciudad y rango de fechas.
        /// La búsqueda de departamento y ciudad es flexible: no distingue mayúsculas/minúsculas,
        /// acentos ni espacios adicionales (ej: "bogota" encontrará "Bogotá D.C.").
        /// </summary>
        /// <param name="departamento">Nombre del departamento para filtrar (opcional). Búsqueda flexible.</param>
        /// <param name="ciudad">Nombre de la ciudad para filtrar (opcional). Búsqueda flexible.</param>
        /// <param name="desde">Fecha de inicio del rango para filtrar siniestros (opcional).</param>
        /// <param name="hasta">Fecha de fin del rango para filtrar siniestros (opcional).</param>
        /// <param name="page">Número de página para la paginación (por defecto: 1).</param>
        /// <param name="pageSize">Cantidad de elementos por página (por defecto: 10, máximo: 100).</param>
        /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
        /// <returns>
        /// 200 OK con el resultado paginado de siniestros que cumplen los criterios,
        /// o 400 Bad Request si los parámetros de paginación o fechas son inválidos.
        /// </returns>
        // GET: api/Siniestros?departamento=...&ciudad=...&desde=...&hasta=...&page=1&pageSize=10
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<SiniestroDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Listar(
            [FromQuery] string? departamento,
            [FromQuery] string? ciudad,
            [FromQuery] DateTime? desde,
            [FromQuery] DateTime? hasta,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            if (desde.HasValue && hasta.HasValue && desde.Value > hasta.Value)
                return BadRequest("El parámetro 'desde' no puede ser mayor que 'hasta'.");

            var result = await _mediator.Send(
                new ConsultarSiniestrosQuery(departamento, ciudad, desde, hasta, page, pageSize),
                cancellationToken);

            return Ok(result);
        }
    }
}