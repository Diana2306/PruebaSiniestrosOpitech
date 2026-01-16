using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siniestros.Domain.Entities;

/// <summary>
/// Representa una ciudad de Colombia identificada por su código DIVIPOLA.
/// Cada ciudad pertenece a un departamento y puede tener múltiples siniestros asociados.
/// </summary>
public class Ciudad
{
    /// <summary>
    /// Código DIVIPOLA único de la ciudad (5 dígitos).
    /// Este código sigue el estándar del DANE y identifica de manera única cada ciudad en Colombia.
    /// </summary>
    public string Id { get; set; } = default!;

    /// <summary>
    /// Código DIVIPOLA del departamento al que pertenece esta ciudad (2 dígitos).
    /// </summary>
    public string IdDepartamento { get; set; } = default!;

    /// <summary>
    /// Navegación a la entidad Departamento al que pertenece esta ciudad.
    /// </summary>
    public Departamento Departamento { get; set; } = default!;

    /// <summary>
    /// Nombre de la ciudad (ej: "Bogotá D.C.", "Medellín", "Cali").
    /// </summary>
    public string Nombre { get; set; } = default!;

    /// <summary>
    /// Colección de siniestros viales que han ocurrido en esta ciudad.
    /// </summary>
    public ICollection<Siniestro> Siniestros { get; set; } = new List<Siniestro>();
}