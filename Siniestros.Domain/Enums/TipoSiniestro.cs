using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siniestros.Domain.Enums
{
    /// <summary>
    /// Enumera los tipos de siniestros viales que pueden ocurrir.
    /// Este enum define las categorías principales de accidentes de tránsito
    /// que el sistema puede registrar y clasificar.
    /// </summary>
    public enum TipoSiniestro
    {
        /// <summary>
        /// Siniestro tipo choque: colisión entre dos o más vehículos.
        /// </summary>
        Choque = 1,

        /// <summary>
        /// Siniestro tipo atropello: un vehículo impacta a un peatón.
        /// </summary>
        Atropello = 2,

        /// <summary>
        /// Siniestro tipo volcamiento: un vehículo se voltea o vuelca.
        /// </summary>
        Volcamiento = 3,

        /// <summary>
        /// Siniestro tipo incendio: un vehículo se incendia.
        /// </summary>
        Incendio = 4,

        /// <summary>
        /// Otro tipo de siniestro que no se ajusta a las categorías anteriores.
        /// </summary>
        Otro = 5
    }
}
