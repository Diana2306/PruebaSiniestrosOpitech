# ADR-004: Uso de Códigos DIVIPOLA

## Contexto
Se requiere identificar departamentos y ciudades de Colombia de manera estándar y consistente. Se necesita decidir entre usar GUIDs, IDs numéricos autoincrementales, o códigos estándar.

## Decisión
Utilizar códigos DIVIPOLA del DANE como identificadores primarios para departamentos y ciudades:
- **Departamentos**: `CHAR(2)` - Código DIVIPOLA de 2 dígitos
- **Ciudades**: `CHAR(5)` - Código DIVIPOLA de 5 dígitos

### Ejemplos
- Departamento Tolima: `73`
- Ciudad Ibagué: `73001` (73 = Tolima, 001 = Ibagué)
- Departamento Bogotá D.C.: `11`
- Ciudad Bogotá: `11001`

## Resultado
- Estándar oficial del DANE.
- Códigos únicos y consistentes.
- Relación natural entre departamento y ciudad (primeros 2 dígitos).
- Compatible con sistemas gubernamentales.
- No requiere conversiones o mapeos adicionales.

## Implementación
```csharp
// Entidades
public class Departamento
{
    public string Id { get; set; } // CHAR(2)
    public string Nombre { get; set; }
}

public class Ciudad
{
    public string Id { get; set; } // CHAR(5)
    public string IdDepartamento { get; set; } // CHAR(2)
    public Departamento Departamento { get; set; }
}
```