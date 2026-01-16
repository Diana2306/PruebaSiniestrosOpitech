# ADR-005: FluentValidation para Validaciones

## Contexto
Se requiere validar los datos de entrada (commands y queries) de manera consistente y declarativa antes de ejecutar la lógica de negocio.

## Decisión
Utilizar FluentValidation para implementar validaciones declarativas y reutilizables, integrado con MediatR mediante un Pipeline Behavior.

### Implementación
- **Validators**: Clases que heredan de `AbstractValidator<T>`.
- **ValidationBehavior**: Pipeline behavior que ejecuta validaciones antes de los handlers.
- **ExceptionMiddleware**: Captura `ValidationException` y retorna respuestas HTTP 400.

## Resultado
- Validaciones declarativas y legibles.
- Separación de lógica de validación de lógica de negocio.
- Reutilización de reglas de validación.
- Mensajes de error personalizados.
- Respuestas HTTP consistentes (400 Bad Request).

## Ejemplo de Implementación
```csharp
public sealed class CrearSiniestroCommandValidator : AbstractValidator<CrearSiniestroCommand>
{
    public CrearSiniestroCommandValidator()
    {
        RuleFor(x => x.IdCiudad)
            .NotEmpty()
            .Length(5)
            .Matches(@"^\d{5}$")
            .WithMessage("El código de ciudad debe ser un código DIVIPOLA de 5 dígitos.");
    }
}
```