using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Siniestros.Domain.Enums;

namespace Siniestros.Application.Dtos;

/// <summary>
/// Objeto de transferencia de datos (DTO) que representa un siniestro vial.
/// Este DTO se utiliza para exponer información de siniestros a través de la API,
/// incluyendo datos relacionados como el nombre de la ciudad y departamento.
/// </summary>
public sealed class SiniestroDto
{
    /// <summary>
    /// Identificador único del siniestro (GUID).
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Fecha y hora exacta en que ocurrió el siniestro.
    /// </summary>
    public DateTime FechaHora { get; init; }

    /// <summary>
    /// Código DIVIPOLA de la ciudad donde ocurrió el siniestro (5 dígitos).
    /// </summary>
    public string IdCiudad { get; init; } = default!;

    /// <summary>
    /// Nombre de la ciudad donde ocurrió el siniestro (ej: "Bogotá D.C.", "Medellín").
    /// </summary>
    public string Ciudad { get; init; } = default!;

    /// <summary>
    /// Código DIVIPOLA del departamento donde ocurrió el siniestro (2 dígitos).
    /// </summary>
    public string IdDepartamento { get; init; } = default!;

    /// <summary>
    /// Nombre del departamento donde ocurrió el siniestro (ej: "Bogotá D.C.", "Antioquia").
    /// </summary>
    public string Departamento { get; init; } = default!;

    /// <summary>
    /// Identificador del tipo de siniestro.
    /// </summary>
    public int IdTipoSiniestro { get; init; }

    /// <summary>
    /// Nombre del tipo de siniestro (ej: "Choque", "Atropello", "Volcamiento").
    /// </summary>
    public string TipoSiniestro { get; init; } = default!;

    /// <summary>
    /// Cantidad de vehículos involucrados en el siniestro.
    /// </summary>
    public int VehiculosInvolucrados { get; init; }

    /// <summary>
    /// Número de víctimas del siniestro.
    /// </summary>
    public int NumeroVictimas { get; init; }

    /// <summary>
    /// Descripción opcional del siniestro con detalles adicionales.
    /// </summary>
    public string? Descripcion { get; init; }
}
