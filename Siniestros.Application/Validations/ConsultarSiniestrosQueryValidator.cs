using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;
using Siniestros.Application.Queries;

namespace Siniestros.Application.Validations;

/// <summary>
/// Validador FluentValidation para la query ConsultarSiniestrosQuery.
/// Define las reglas de validación para los parámetros de paginación y filtros de fecha.
/// Estas validaciones se ejecutan automáticamente mediante el ValidationBehavior de MediatR.
/// </summary>
public sealed class ConsultarSiniestrosQueryValidator : AbstractValidator<ConsultarSiniestrosQuery>
{
    /// <summary>
    /// Inicializa las reglas de validación para la query de consultar siniestros.
    /// </summary>
    public ConsultarSiniestrosQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0).WithMessage("page debe ser mayor que 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("pageSize debe ser mayor que 0.")
            .LessThanOrEqualTo(100).WithMessage("pageSize no puede ser mayor a 100.");

        RuleFor(x => x)
            .Must(x => !(x.Desde.HasValue && x.Hasta.HasValue && x.Desde.Value > x.Hasta.Value))
            .WithMessage("El parámetro 'desde' no puede ser mayor que 'hasta'.");
    }
}