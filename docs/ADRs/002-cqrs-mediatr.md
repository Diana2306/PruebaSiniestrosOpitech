# ADR-002: CQRS con MediatR

## Contexto
Se requiere separar claramente las operaciones de lectura (queries) y escritura (commands) para mejorar la mantenibilidad y escalabilidad del sistema.

## Decisión
Implementar CQRS (Command Query Responsibility Segregation) utilizando MediatR como mediador entre controladores y handlers.

### Estructura
- **Commands**: Operaciones de escritura (CrearSiniestroCommand).
- **Queries**: Operaciones de lectura (ConsultarSiniestrosQuery, ObtenerSiniestroPorIdQuery).
- **Handlers**: Implementación de la lógica de negocio para cada comando/query.

## Resultado
- Separación clara entre lectura y escritura.
- Handlers enfocados en una única responsabilidad.
- Facilita testing unitario.
- Permite optimizar queries y commands de forma independiente.

## Implementación
```csharp
// Command
public sealed record CrearSiniestroCommand(...) : IRequest<Guid>;

// Handler
public sealed class CrearSiniestroHandler 
    : IRequestHandler<CrearSiniestroCommand, Guid>

// Query
public sealed record ConsultarSiniestrosQuery(...) : IRequest<PagedResult<SiniestroDto>>;
```