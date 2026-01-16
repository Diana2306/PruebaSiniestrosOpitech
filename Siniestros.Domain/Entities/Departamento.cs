using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siniestros.Domain.Entities;

/// <summary>
/// Representa un departamento de Colombia identificado por su código DIVIPOLA.
/// Cada departamento puede contener múltiples ciudades y, a través de ellas, múltiples siniestros.
/// </summary>
public class Departamento
{
    /// <summary>
    /// Código DIVIPOLA único del departamento (2 dígitos).
    /// Este código sigue el estándar del DANE y identifica de manera única cada departamento en Colombia.
    /// Ejemplos: "11" para Bogotá D.C., "05" para Antioquia, "08" para Atlántico.
    /// </summary>
    public string Id { get; set; } = default!;

    /// <summary>
    /// Nombre del departamento (ej: "Antioquia", "Bogotá D.C.", "Atlántico").
    /// </summary>
    public string Nombre { get; set; } = default!;

    /// <summary>
    /// Colección de ciudades que pertenecen a este departamento.
    /// </summary>
    public ICollection<Ciudad> Ciudades { get; set; } = new List<Ciudad>();
}