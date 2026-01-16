using FluentValidation;
using MediatR;

namespace Siniestros.Application.Common.Behaviors;

/// <summary>
/// Behavior de MediatR que intercepta todas las requests y ejecuta las validaciones
/// de FluentValidation antes de que el handler procese la request.
/// Si alguna validación falla, se lanza una ValidationException con los errores encontrados.
/// </summary>
/// <typeparam name="TRequest">Tipo de la request que se está validando (Command o Query).</typeparam>
/// <typeparam name="TResponse">Tipo de la respuesta que retorna el handler.</typeparam>
public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// Inicializa una nueva instancia del behavior con los validadores disponibles.
    /// </summary>
    /// <param name="validators">Colección de validadores FluentValidation registrados para el tipo de request.</param>
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    /// <summary>
    /// Intercepta la request, ejecuta todas las validaciones y, si son exitosas, continúa con el handler.
    /// </summary>
    /// <param name="request">Request que se está procesando (Command o Query).</param>
    /// <param name="next">Delegado que invoca al siguiente behavior o handler en la cadena.</param>
    /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
    /// <returns>La respuesta del handler si todas las validaciones pasan.</returns>
    /// <exception cref="ValidationException">
    /// Se lanza si alguna validación falla, conteniendo todos los errores de validación encontrados.
    /// </exception>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        var results = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken))
        );

        var failures = results
            .SelectMany(r => r.Errors)
            .Where(f => f is not null)
            .ToList();

        if (failures.Count != 0)
            throw new ValidationException(failures);

        return await next();
    }
}
