using MediatR;
using Siniestros.Application.Common;
using Siniestros.Application.Dtos;

namespace Siniestros.Application.Queries;

/// <summary>
/// Query CQRS para consultar siniestros viales con filtros opcionales y paginación.
/// Permite buscar siniestros por departamento, ciudad, rango de fechas, y paginar los resultados.
/// La búsqueda de departamento y ciudad es flexible: no distingue mayúsculas/minúsculas,
/// acentos ni espacios adicionales (ej: "bogota" encontrará "Bogotá D.C.").
/// </summary>
/// <param name="Departamento">Nombre del departamento para filtrar (opcional). Búsqueda flexible e insensible a mayúsculas, acentos y espacios.</param>
/// <param name="Ciudad">Nombre de la ciudad para filtrar (opcional). Búsqueda flexible e insensible a mayúsculas, acentos y espacios.</param>
/// <param name="Desde">Fecha de inicio del rango para filtrar siniestros (opcional). Si se proporciona, solo se incluyen siniestros desde esta fecha.</param>
/// <param name="Hasta">Fecha de fin del rango para filtrar siniestros (opcional). Si se proporciona sin hora, se incluye todo el día. Si se proporciona con hora, se incluye hasta esa hora exacta.</param>
/// <param name="Page">Número de página para la paginación (por defecto: 1). Debe ser mayor que 0.</param>
/// <param name="PageSize">Cantidad de elementos por página (por defecto: 10, máximo: 100). Debe ser mayor que 0.</param>
public sealed record ConsultarSiniestrosQuery(
    string? Departamento,
    string? Ciudad,
    DateTime? Desde,
    DateTime? Hasta,
    int Page = 1,
    int PageSize = 10
) : IRequest<PagedResult<SiniestroDto>>;