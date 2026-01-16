using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using Siniestros.Domain.Enums;

namespace Siniestros.Application.Commands
{
    /// <summary>
    /// Comando CQRS para crear un nuevo siniestro vial en el sistema.
    /// Este comando encapsula toda la información necesaria para registrar un accidente de tránsito.
    /// Al ejecutarse, retorna el identificador único (Guid) del siniestro creado.
    /// </summary>
    /// <param name="FechaHora">Fecha y hora exacta en que ocurrió el siniestro.</param>
    /// <param name="IdCiudad">Código DIVIPOLA de 5 dígitos de la ciudad donde ocurrió el siniestro.</param>
    /// <param name="IdTipoSiniestro">Identificador del tipo de siniestro (1=Choque, 2=Atropello, 3=Volcamiento, 4=Incendio, 5=Otro).</param>
    /// <param name="VehiculosInvolucrados">Cantidad de vehículos involucrados en el siniestro (debe ser >= 0).</param>
    /// <param name="NumeroVictimas">Número de víctimas del siniestro (debe ser >= 0).</param>
    /// <param name="Descripcion">Descripción opcional con detalles adicionales del siniestro.</param>
    public sealed record CrearSiniestroCommand(
        DateTime FechaHora,
        string IdCiudad,
        int IdTipoSiniestro,
        int VehiculosInvolucrados,
        int NumeroVictimas,
        string? Descripcion
    ) : IRequest<Guid>;
}
