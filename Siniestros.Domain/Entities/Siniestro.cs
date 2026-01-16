using Siniestros.Domain.Entities;

namespace Siniestros.Domain.Entities;

/// <summary>
/// Representa un siniestro vial ocurrido en una ciudad específica.
/// Esta entidad encapsula toda la información relacionada con un accidente de tránsito,
/// incluyendo la ubicación, tipo, fecha, vehículos involucrados y víctimas.
/// </summary>
public class Siniestro
{
    /// <summary>
    /// Identificador único del siniestro (GUID).
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Código DIVIPOLA de la ciudad donde ocurrió el siniestro (5 dígitos).
    /// Este código sigue el estándar del DANE (Departamento Administrativo Nacional de Estadística).
    /// </summary>
    public string IdCiudad { get; private set; } = default!;

    /// <summary>
    /// Navegación a la entidad Ciudad donde ocurrió el siniestro.
    /// </summary>
    public Ciudad Ciudad { get; private set; } = default!;

    /// <summary>
    /// Identificador del tipo de siniestro (ej: Choque, Atropello, Volcamiento, etc.).
    /// </summary>
    public int IdTipoSiniestro { get; private set; }

    /// <summary>
    /// Navegación a la entidad TipoSiniestro que describe la naturaleza del accidente.
    /// </summary>
    public TipoSiniestro TipoSiniestro { get; private set; } = default!;

    /// <summary>
    /// Fecha y hora exacta en que ocurrió el siniestro.
    /// </summary>
    public DateTime FechaHora { get; private set; }

    /// <summary>
    /// Cantidad de vehículos involucrados en el siniestro.
    /// Debe ser un número mayor o igual a cero.
    /// </summary>
    public int VehiculosInvolucrados { get; private set; }

    /// <summary>
    /// Número de víctimas (heridos o fallecidos) como resultado del siniestro.
    /// Debe ser un número mayor o igual a cero.
    /// </summary>
    public int NumeroVictimas { get; private set; }

    /// <summary>
    /// Descripción opcional del siniestro que puede incluir detalles adicionales
    /// sobre las circunstancias del accidente.
    /// </summary>
    public string? Descripcion { get; private set; }

    /// <summary>
    /// Constructor privado requerido por Entity Framework Core para la creación de instancias.
    /// No debe ser utilizado directamente. Use el método estático <see cref="Crear"/> en su lugar.
    /// </summary>
    private Siniestro() { }

    /// <summary>
    /// Crea una nueva instancia de Siniestro con validación de reglas de negocio.
    /// Este método es el único punto de entrada para crear siniestros y garantiza
    /// que todas las validaciones se cumplan antes de crear la entidad.
    /// </summary>
    /// <param name="fechaHora">Fecha y hora en que ocurrió el siniestro.</param>
    /// <param name="idCiudad">Código DIVIPOLA de 5 dígitos de la ciudad donde ocurrió el siniestro.</param>
    /// <param name="idTipoSiniestro">Identificador del tipo de siniestro (debe ser mayor que cero).</param>
    /// <param name="vehiculosInvolucrados">Cantidad de vehículos involucrados (debe ser mayor o igual a cero).</param>
    /// <param name="numeroVictimas">Número de víctimas del siniestro (debe ser mayor o igual a cero).</param>
    /// <param name="descripcion">Descripción opcional del siniestro. Se normaliza (trim) si se proporciona.</param>
    /// <returns>Una nueva instancia de Siniestro con un Id único generado automáticamente.</returns>
    /// <exception cref="ArgumentException">
    /// Se lanza cuando alguno de los parámetros no cumple con las validaciones:
    /// - Si idCiudad es nulo, vacío o no tiene exactamente 5 dígitos.
    /// - Si idTipoSiniestro es menor o igual a cero.
    /// - Si vehiculosInvolucrados o numeroVictimas son negativos.
    /// </exception>
    public static Siniestro Crear(
        DateTime fechaHora,
        string idCiudad,
        int idTipoSiniestro,
        int vehiculosInvolucrados,
        int numeroVictimas,
        string? descripcion
    )
    {
        if (string.IsNullOrWhiteSpace(idCiudad))
            throw new ArgumentException("La ciudad es obligatoria (IdCiudad).");
        
        if (idCiudad.Length != 5)
            throw new ArgumentException("El código de ciudad debe tener 5 dígitos (código DIVIPOLA).");

        if (idTipoSiniestro <= 0)
            throw new ArgumentException("El tipo de siniestro es obligatorio (IdTipoSiniestro).");

        if (vehiculosInvolucrados < 0)
            throw new ArgumentException("Los vehículos involucrados no pueden ser negativos");

        if (numeroVictimas < 0)
            throw new ArgumentException("El número de víctimas no puede ser negativo");

        return new Siniestro
        {
            Id = Guid.NewGuid(),
            FechaHora = fechaHora,
            IdCiudad = idCiudad,
            IdTipoSiniestro = idTipoSiniestro,
            VehiculosInvolucrados = vehiculosInvolucrados,
            NumeroVictimas = numeroVictimas,
            Descripcion = string.IsNullOrWhiteSpace(descripcion) ? null : descripcion.Trim()
        };
    }
}