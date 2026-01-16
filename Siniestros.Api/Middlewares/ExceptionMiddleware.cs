using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Siniestros.Api.Middlewares;

/// <summary>
/// Middleware global para el manejo centralizado de excepciones en la API.
/// Este middleware captura todas las excepciones no manejadas y las convierte
/// en respuestas HTTP apropiadas con códigos de estado y mensajes de error estructurados.
/// </summary>
public sealed class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    /// <summary>
    /// Inicializa una nueva instancia del middleware con el logger para registrar errores.
    /// </summary>
    /// <param name="logger">Logger para registrar información sobre las excepciones capturadas.</param>
    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Intercepta las requests HTTP, captura excepciones y las convierte en respuestas apropiadas.
    /// Maneja tres tipos de excepciones:
    /// - ValidationException: Retorna 400 Bad Request con detalles de validación
    /// - ArgumentException: Retorna 400 Bad Request con el mensaje de error
    /// - Exception genérica: Retorna 500 Internal Server Error con mensaje genérico
    /// </summary>
    /// <param name="context">Contexto HTTP de la request actual.</param>
    /// <param name="next">Delegado que invoca el siguiente middleware en la cadena.</param>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Error de validación en {Path}", context.Request.Path);

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var errors = ex.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            var problem = new ValidationProblemDetails(errors)
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation error",
                Detail = "Uno o más campos no cumplen las reglas de validación."
            };

            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Argumento inválido en {Path}", context.Request.Path);

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var problem = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Detail = ex.Message
            };

            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado en {Path}", context.Request.Path);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var problem = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "Ocurrió un error inesperado."
            };

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}