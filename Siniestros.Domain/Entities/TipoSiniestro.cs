using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siniestros.Domain.Entities;

/// <summary>
/// Representa un tipo o categoría de siniestro vial.
/// Define la naturaleza del accidente de tránsito (ej: Choque, Atropello, Volcamiento).
/// </summary>
public class TipoSiniestro
{
    /// <summary>
    /// Identificador único del tipo de siniestro.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre descriptivo del tipo de siniestro (ej: "Choque", "Atropello", "Volcamiento", "Incendio", "Otro").
    /// </summary>
    public string Nombre { get; set; } = default!;

    /// <summary>
    /// Colección de siniestros que están clasificados con este tipo.
    /// </summary>
    public ICollection<Siniestro> Siniestros { get; set; } = new List<Siniestro>();
}