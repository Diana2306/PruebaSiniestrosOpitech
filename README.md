# API REST de Siniestros Viales

API REST desarrollada en .NET 8 para el registro y consulta de siniestros viales en Colombia, implementando arquitectura limpia, DDD, CQRS y principios SOLID.

## Tabla de Contenido

- [Características](#características)
- [Arquitectura](#arquitectura)
- [Requisitos](#requisitos)
- [Instalación y Configuración](#instalación-y-configuración)
- [Ejecución](#ejecución)
- [Endpoints](#endpoints)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [Decisiones Arquitectónicas](#decisiones-arquitectónicas)
- [Testing](#testing)

## Características

- Registro de siniestros viales con validaciones
- Consulta de siniestros con filtros por departamento y rango de fechas
- Paginación de resultados
- Arquitectura limpia con separación de responsabilidades
- Implementación de CQRS con MediatR
- Domain-Driven Design (DDD)
- Validaciones con FluentValidation
- Manejo centralizado de errores
- Uso de códigos DIVIPOLA del DANE para departamentos y ciudades

## Arquitectura

El proyecto sigue una arquitectura limpia con las siguientes capas:

```
┌─────────────────────────────────────┐
│         Siniestros.Api              │  ← Capa de Presentación
│      (Controllers, Middlewares)     │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│      Siniestros.Application         │  ← Capa de Aplicación
│  (Commands, Queries, Handlers, DTOs)│
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│       Siniestros.Domain             │  ← Capa de Dominio
│      (Entities, Business Logic)      │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│    Siniestros.Infrastructure         │  ← Capa de Infraestructura
│   (Persistence, DbContext, EF Core) │
└─────────────────────────────────────┘
```

### Principios Aplicados

- **CQRS**: Separación de comandos (escritura) y queries (lectura).
- **DDD**: Entidades de dominio con lógica de negocio encapsulada.
- **SOLID**: Principios aplicados en toda la solución.
- **Repository Pattern**: Abstracción de acceso a datos.
- **Mediator Pattern**: Desacoplamiento mediante MediatR.

## Requisitos

- .NET 8 SDK.
- SQL Server 2019 o superior.
- Visual Studio 2022.

## Instalación y Configuración

### 1. Clonar el repositorio

```bash
git clone <url-del-repositorio>
cd PruebaSiniestros
```

### 2. Configurar la base de datos

Editar `Siniestros.Api/appsettings.json` y configurar la cadena de conexión:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SiniestrosDb;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False;"
  }
}
```

### 3. Crear la base de datos

```bash
# Desde la raíz del proyecto
dotnet ef database update --project Siniestros.Infrastructure --startup-project Siniestros.Api
```

### 4. Poblar datos iniciales

Ejecutar el script SQL para insertar departamentos, ciudades y tipos de siniestros el cual se encuentra en la ruta Scripts/PoblarDatos.sql
este deberá ser ejecutado desde SQL Server Management Studio.

## Ejecución

### Desarrollo

```bash
cd Siniestros.Api
dotnet run
```

La API estará disponible en:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger UI: `https://localhost:5001/swagger`

## Endpoints

### POST /api/Siniestros

Registra un nuevo siniestro vial.

**Request Body:**
```json
{
  "fechaHora": "2024-01-15T10:30:00Z",
  "idCiudad": "73001",
  "idTipoSiniestro": 1,
  "vehiculosInvolucrados": 2,
  "numeroVictimas": 0,
  "descripcion": "Choque en intersección"
}
```

**Response:** `200 OK`
```json
"3fa85f64-5717-4562-b3fc-2c963f66afa6"
```

**Validaciones:**
- `fechaHora`: Requerido
- `idCiudad`: Requerido, debe ser código DIVIPOLA de 5 dígitos
- `idTipoSiniestro`: Requerido, mayor que 0
- `vehiculosInvolucrados`: Mayor o igual a 0
- `numeroVictimas`: Mayor o igual a 0
- `descripcion`: Opcional, máximo 1000 caracteres

### GET /api/Siniestros

Consulta siniestros con filtros y paginación.

**Query Parameters:**
- `departamento` (opcional): Nombre del departamento
- `ciudad` (opcional): Nombre de la ciudad
- `desde` (opcional): Fecha inicial (ISO 8601)
- `hasta` (opcional): Fecha final (ISO 8601)
- `page` (opcional, default: 1): Número de página
- `pageSize` (opcional, default: 10, max: 100): Tamaño de página

**Ejemplo:**
```
GET /api/Siniestros?departamento=Tolima&desde=2024-01-01&hasta=2024-01-31&page=1&pageSize=10
```

**Response:** `200 OK`
```json
{
  "items": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "fechaHora": "2024-01-15T10:30:00Z",
      "idCiudad": "73001",
      "ciudad": "Ibagué",
      "idDepartamento": "73",
      "departamento": "Tolima",
      "idTipoSiniestro": 1,
      "tipoSiniestro": "Choque frontal",
      "vehiculosInvolucrados": 2,
      "numeroVictimas": 0,
      "descripcion": "Choque en intersección"
    }
  ],
  "totalItems": 100,
  "page": 1,
  "pageSize": 10,
  "totalPages": 10
}
```

### GET /api/Siniestros/{id}

Obtiene un siniestro por su ID.

**Response:** `200 OK` o `404 Not Found`

## Estructura del Proyecto

```
PruebaSiniestros/
├── Siniestros.Api/                 # Capa de presentación
│   ├── Controllers/                 # Controladores REST
│   ├── Middlewares/                 # Middlewares (Exception handling)
│   └── Program.cs                  # Configuración de la aplicación
│
├── Siniestros.Application/          # Capa de aplicación
│   ├── Commands/                    # Comandos (CQRS - Escritura)
│   ├── Queries/                     # Queries (CQRS - Lectura)
│   ├── Handlers/                    # Handlers de MediatR
│   ├── Dtos/                        # Data Transfer Objects
│   ├── Validations/                 # Validadores FluentValidation
│   ├── Common/                      # Utilidades comunes
│   └── Persistence/                 # Interfaces de persistencia
│
├── Siniestros.Domain/               # Capa de dominio
│   ├── Entities/                    # Entidades de dominio
│   └── Enums/                       # Enumeraciones
│
├── Siniestros.Infrastructure/       # Capa de infraestructura
│   └── Persistence/                  # Implementación de persistencia
│       ├── SiniestrosDbContext.cs    # DbContext de EF Core
│       └── Repositories/             # Implementación de repositorios
│
├── Siniestros.Tests/                # Pruebas unitarias
│   ├── Application/                 # Tests de handlers
│   ├── Domain/                      # Tests de dominio
│   └── Helpers/                     # Helpers para tests
│
├── Scripts/                          # Scripts SQL
│   └── PoblarDatos.sql               # Script de datos iniciales
│
└── docs/                             # Documentación del proyecto
    ├── ADRs/                         # Architecture Decision Records
    │   ├── 001-arquitectura-limpia.md        # Decisión sobre arquitectura en capas
    │   ├── 002-cqrs-mediatr.md               # Separación de comandos y queries
    │   ├── 003-repository-pattern.md         # Patrón de repositorio con DbContext
    │   ├── 004-codigos-divipola.md           # Uso de códigos DANE para ubicaciones
    │   ├── 005-fluent-validation.md          # Validaciones declarativas
    │   └── 006-manejo-errores.md             # Middleware centralizado de excepciones
    └── MODELO_DOMINIO.md             # Documentación del modelo de dominio
```

## Decisiones Arquitectónicas

Para conocer más detalles sobre las decisiones arquitectónicas, por favor consultar la documentación de [ADRs (Architecture Decision Records)](./docs/ADRs/).

### Principales Decisiones:

1. **CQRS con MediatR**: Separación clara entre comandos y queries.
2. **FluentValidation**: Validaciones declarativas y reutilizables.
3. **Repository Pattern**: Abstracción sobre EF Core para testabilidad.
4. **Códigos DIVIPOLA**: Uso de estándar DANE para departamentos y ciudades.
5. **Arquitectura Limpia**: Separación de responsabilidades por capas.

## Testing

### Ejecutar tests

```bash
dotnet test
```

### Cobertura de tests

- Tests de dominio (entidades).
- Tests de handlers (Application).

## Documentación Adicional

- [Modelo de Dominio](./docs/MODELO_DOMINIO.md)
- [Architecture Decision Records](./docs/ADRs/)
- [Validación de Requisitos](./VALIDACION_REQUISITOS.md)

## Autor


Desarrollado como prueba técnica para Senior .NET Backend Developer por Diana Carolina Villanueva A.
