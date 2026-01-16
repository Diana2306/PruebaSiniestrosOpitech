using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siniestros.Application.Common;

/// <summary>
/// Representa el resultado de una consulta paginada.
/// Esta clase encapsula los elementos de una página específica junto con
/// información sobre el total de elementos y la paginación.
/// </summary>
/// <typeparam name="T">Tipo de elementos contenidos en el resultado paginado.</typeparam>
public sealed class PagedResult<T>
{
    /// <summary>
    /// Lista de elementos de la página actual.
    /// </summary>
    public required IReadOnlyList<T> Items { get; init; }

    /// <summary>
    /// Total de elementos que cumplen con los criterios de búsqueda (sin paginación).
    /// </summary>
    public required int TotalItems { get; init; }

    /// <summary>
    /// Número de la página actual (basado en 1, no en 0).
    /// </summary>
    public required int Page { get; init; }

    /// <summary>
    /// Cantidad de elementos por página.
    /// </summary>
    public required int PageSize { get; init; }

    /// <summary>
    /// Total de páginas disponibles basado en el total de elementos y el tamaño de página.
    /// </summary>
    public required int TotalPages { get; init; }

    /// <summary>
    /// Crea una nueva instancia de PagedResult con validación y normalización de parámetros.
    /// Asegura que la página y el tamaño de página sean válidos antes de crear el resultado.
    /// </summary>
    /// <param name="items">Lista de elementos de la página actual.</param>
    /// <param name="totalItems">Total de elementos que cumplen con los criterios de búsqueda.</param>
    /// <param name="page">Número de página (se normaliza a 1 si es menor).</param>
    /// <param name="pageSize">Tamaño de página (se normaliza a 10 si es menor a 1).</param>
    /// <returns>Una nueva instancia de PagedResult con los valores calculados correctamente.</returns>
    public static PagedResult<T> Create(IReadOnlyList<T> items, int totalItems, int page, int pageSize)
    {
        if (pageSize < 1) pageSize = 10;
        if (page < 1) page = 1;

        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        return new PagedResult<T>
        {
            Items = items,
            TotalItems = totalItems,
            Page = page,
            PageSize = pageSize,
            TotalPages = totalPages
        };
    }
}
