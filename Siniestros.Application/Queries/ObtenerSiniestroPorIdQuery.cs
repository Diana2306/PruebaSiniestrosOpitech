using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using Siniestros.Application.Dtos;

namespace Siniestros.Application.Queries;

/// <summary>
/// Query CQRS para obtener un siniestro específico por su identificador único.
/// Si el siniestro no existe, retorna null en lugar de lanzar una excepción.
/// </summary>
/// <param name="Id">Identificador único (Guid) del siniestro a consultar.</param>
public sealed record ObtenerSiniestroPorIdQuery(Guid Id) : IRequest<SiniestroDto?>;
