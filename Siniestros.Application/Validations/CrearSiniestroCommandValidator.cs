using FluentValidation;
using Siniestros.Application.Commands;

namespace Siniestros.Application.Validations;

/// <summary>
/// Validador FluentValidation para el comando CrearSiniestroCommand.
/// Define todas las reglas de validación que deben cumplirse antes de crear un siniestro.
/// Estas validaciones se ejecutan automáticamente mediante el ValidationBehavior de MediatR.
/// </summary>
public sealed class CrearSiniestroCommandValidator : AbstractValidator<CrearSiniestroCommand>
{
    /// <summary>
    /// Inicializa las reglas de validación para el comando de crear siniestro.
    /// </summary>
    public CrearSiniestroCommandValidator()
    {
        RuleFor(x => x.FechaHora)
            .NotEmpty();

        // Validar código DIVIPOLA de ciudad (5 dígitos)
        RuleFor(x => x.IdCiudad)
            .NotEmpty()
            .Length(5)
            .Matches(@"^\d{5}$")
            .WithMessage("El código de ciudad debe ser un código DIVIPOLA de 5 dígitos.");

        // Si IdTipoSiniestro es int
        RuleFor(x => x.IdTipoSiniestro)
            .GreaterThan(0)
            .WithMessage("El tipo de siniestro es obligatorio.");

        RuleFor(x => x.VehiculosInvolucrados)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.NumeroVictimas)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Descripcion)
            .MaximumLength(1000);
    }
}