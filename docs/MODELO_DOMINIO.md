# Modelo de Dominio - Sistema de Siniestros Viales

## Descripción General

El dominio del sistema está centrado en la gestión de siniestros viales en Colombia. El modelo utiliza códigos DIVIPOLA del DANE para identificar departamentos y ciudades de manera estándar.

## Entidades del Dominio

### 1. Siniestro

**Descripción:** Representa un evento de siniestro vial ocurrido en una ubicación específica.

**Propiedades:**
- `Id` (Guid): Identificador único del siniestro
- `IdCiudad` (string, CHAR(5)): Código DIVIPOLA de la ciudad donde ocurrió
- `Ciudad` (navegación): Relación con la entidad Ciudad
- `IdTipoSiniestro` (int): Identificador del tipo de siniestro
- `TipoSiniestro` (navegación): Relación con la entidad TipoSiniestro
- `FechaHora` (DateTime): Fecha y hora exacta del siniestro
- `VehiculosInvolucrados` (int): Cantidad de vehículos involucrados (>= 0)
- `NumeroVictimas` (int): Número de víctimas (>= 0)
- `Descripcion` (string?): Descripción opcional del siniestro

**Reglas de Negocio:**
- La ciudad es obligatoria (IdCiudad debe ser un código DIVIPOLA válido de 5 dígitos).
- El tipo de siniestro es obligatorio (IdTipoSiniestro > 0).
- Los vehículos involucrados no pueden ser negativos.
- El número de víctimas no puede ser negativo.
- La descripción es opcional, pero si se proporciona, se trima automáticamente.

**Métodos:**
- `Crear()`: Factory method estático que valida y crea una nueva instancia de Siniestro.

**Ejemplo de uso:**
```csharp
var siniestro = Siniestro.Crear(
    fechaHora: DateTime.UtcNow,
    idCiudad: "73001", // Ibagué
    idTipoSiniestro: 1,
    vehiculosInvolucrados: 2,
    numeroVictimas: 0,
    descripcion: "Choque en intersección"
);
```

### 2. Ciudad

**Descripción:** Representa una ciudad o municipio de Colombia identificado por código DIVIPOLA.

**Propiedades:**
- `Id` (string, CHAR(5)): Código DIVIPOLA de 5 dígitos (clave primaria).
- `IdDepartamento` (string, CHAR(2)): Código DIVIPOLA del departamento (2 dígitos).
- `Departamento` (navegación): Relación con la entidad Departamento.
- `Nombre` (string): Nombre de la ciudad.
- `Siniestros` (ICollection<Siniestro>): Colección de siniestros ocurridos en esta ciudad.

**Reglas de Negocio:**
- El código DIVIPOLA debe tener exactamente 5 dígitos.
- Los primeros 2 dígitos corresponden al código del departamento.
- El nombre de la ciudad es obligatorio.

**Ejemplo:**
- `73001` = Ibagué (Tolima)
- `11001` = Bogotá D.C.
- `05001` = Medellín (Antioquia)

### 3. Departamento

**Descripción:** Representa un departamento de Colombia identificado por código DIVIPOLA.

**Propiedades:**
- `Id` (string, CHAR(2)): Código DIVIPOLA de 2 dígitos (clave primaria).
- `Nombre` (string): Nombre del departamento.
- `Ciudades` (ICollection<Ciudad>): Colección de ciudades pertenecientes al departamento.

**Reglas de Negocio:**
- El código DIVIPOLA debe tener exactamente 2 dígitos.
- El nombre del departamento es obligatorio y único.

**Ejemplo:**
- `73` = Tolima
- `11` = Bogotá D.C.
- `05` = Antioquia

### 4. TipoSiniestro

**Descripción:** Catálogo de tipos de siniestros viales.

**Propiedades:**
- `Id` (int): Identificador numérico del tipo.
- `Nombre` (string): Nombre del tipo de siniestro.
- `Siniestros` (ICollection<Siniestro>): Colección de siniestros de este tipo.

**Tipos Disponibles:**
1. Choque frontal
2. Choque lateral
3. Choque trasero
4. Volcamiento
5. Atropello
6. Caída de ocupante
7. Colisión con objeto fijo
8. Incendio
9. Salida de vía
10. Colisión múltiple
11. Choque con animal
12. Otro

## Relaciones

**Cardinalidades:**
- Un Departamento tiene muchas Ciudades.
- Una Ciudad pertenece a un Departamento.
- Una Ciudad tiene muchos Siniestros.
- Un Siniestro pertenece a una Ciudad.
- Un TipoSiniestro tiene muchos Siniestros.
- Un Siniestro pertenece a un TipoSiniestro.

## Diagrama de Entidad-Relación

```
┌─────────────────┐
│  Departamento   │
│─────────────────│
│ Id (CHAR(2)) PK │
│ Nombre          │
└────────┬────────┘
         │ 1
         │
         │ N
┌────────▼────────┐
│     Ciudad      │
│─────────────────│
│ Id (CHAR(5)) PK │
│ IdDepto FK      │
│ Nombre          │
└────────┬────────┘
         │ 1
         │
         │ N
┌────────▼────────┐      ┌──────────────┐
│   Siniestro     │      │TipoSiniestro │
│─────────────────│      │──────────────│
│ Id (Guid) PK    │      │ Id (int) PK  │
│ IdCiudad FK     │      │ Nombre       │
│ IdTipoSiniestro │──────┤              │
│ FechaHora       │      └──────────────┘
│ Vehiculos       │
│ Victimas        │
│ Descripcion     │
└─────────────────┘
```

## Conceptos Clave

### Códigos DIVIPOLA

**DIVIPOLA** (División Político-Administrativa) es el estándar del DANE para identificar departamentos y municipios en Colombia.

- **Departamentos**: 2 dígitos (ej: `73` = Tolima).
- **Ciudades/Municipios**: 5 dígitos (ej: `73001` = Ibagué).
  - Los primeros 2 dígitos corresponden al departamento.
  - Los últimos 3 dígitos identifican el municipio.

### Agregados

- **Agregado Raíz**: `Siniestro`
- **Entidades**: `Ciudad`, `Departamento`, `TipoSiniestro` (actúan como catálogos)

## Validaciones de negocio

1. **Un siniestro siempre debe tener una ciudad válida**
   - La ciudad debe existir en el sistema.
   - El código DIVIPOLA debe tener 5 dígitos.

2. **Un siniestro siempre debe tener un tipo válido**
   - El tipo de siniestro debe existir en el catálogo.
   - El ID debe ser mayor que 0.

3. **Los valores numéricos no pueden ser negativos**
   - Vehículos involucrados >= 0.
   - Número de víctimas >= 0.

4. **La fecha del siniestro debe ser válida**
   - No puede ser una fecha futura (validación opcional).
   - Debe estar en un formato válido.