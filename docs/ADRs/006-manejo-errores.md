# ADR-006: Manejo Centralizado de Errores

## Contexto
Se requiere manejar errores de manera consistente en toda la aplicación, proporcionando respuestas HTTP apropiadas y mensajes de error claros.

## Decisión
Implementar un ExceptionMiddleware centralizado que capture todas las excepciones y las convierta en respuestas HTTP apropiadas con formato `ProblemDetails`.

### Tipos de Errores Manejados
1. **ValidationException**: Errores de validación → 400 Bad Request
2. **ArgumentException**: Argumentos inválidos → 400 Bad Request
3. **NotFoundException**: Recursos no encontrados → 404 Not Found
4. **Exception genérica**: Errores inesperados → 500 Internal Server Error

## Resultado
- Manejo consistente de errores en toda la aplicación.
- Respuestas HTTP apropiadas según el tipo de error.
- Formato estándar (ProblemDetails) para errores.

## Implementación
```csharp
public sealed class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            // 400 Bad Request con detalles de validación
        }
        catch (Exception ex)
        {
            // 500 Internal Server Error
        }
    }
}
```