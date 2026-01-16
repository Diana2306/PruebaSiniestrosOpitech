# ADR-001: Arquitectura Limpia

## Contexto
Se requiere desarrollar una API REST para gestión de siniestros viales que sea mantenible, escalable y testeable. La solución debe seguir buenas prácticas de arquitectura de software.

## Decisión
Implementar una Arquitectura Limpia con separación en capas:

- **Siniestros.Domain**: Capa de dominio sin dependencias externas.
- **Siniestros.Application**: Capa de aplicación con lógica de casos de uso.
- **Siniestros.Infrastructure**: Capa de infraestructura con implementaciones técnicas.
- **Siniestros.Api**: Capa de presentación con controladores y configuración.

## Resultado

- Separación clara de responsabilidades.
- Independencia del framework y base de datos.
- Facilita testing unitario.
- Facilita mantenimiento y evolución.
- Permite cambiar implementaciones sin afectar el dominio.