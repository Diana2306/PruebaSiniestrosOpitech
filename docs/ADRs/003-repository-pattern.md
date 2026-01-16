# ADR-003: Repository Pattern con DbContext

## Contexto
Se requiere abstraer el acceso a datos para facilitar testing y permitir cambiar la implementación de persistencia en el futuro.

## Decisión
Implementar el Repository Pattern utilizando una interfaz `ISiniestrosDbContext` que expone los `DbSet<T>` de Entity Framework Core, en lugar de crear repositorios individuales por entidad.

### Implementación
- **ISiniestrosDbContext**: Interfaz que expone los DbSet y SaveChangesAsync
- **SiniestrosDbContext**: Implementación concreta que hereda de DbContext
- Los handlers acceden directamente a los DbSet a través de la interfaz

## Resultado
- Abstracción del acceso a datos.
- Facilita testing con bases de datos en memoria.
- Permite cambiar la implementación de persistencia.