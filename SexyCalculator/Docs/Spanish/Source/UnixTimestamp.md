# Unix Timestamp

Proporciona funciones para convertir entre valores **DateTime** de .NET y **timestamps Unix**, permitiendo interoperabilidad entre sistemas que utilizan diferentes referencias temporales.

## Propiedades

▶ `epochTime`

Instancia privada de `DateTime` que representa el inicio de la era Unix (1970-01-01), usada como referencia en las conversiones.

## Métodos

▶ `ConvertTo(DateTime)`

Calcula el **timestamp Unix** correspondiente a un valor `DateTime` dado.

### Parámetros

* dateTime: valor `DateTime` desde el cual se calculará el timestamp.

### Resultado

Número de segundos desde la fecha de inicio de la era Unix (`1970-01-01`).

### Ejemplo

```csharp
double timestamp = UnixTimestamp.ConvertTo(new DateTime(2025, 11, 3)); // 1762262400
```

---

▶ `ConvertFrom(double)`

Calcula un valor `DateTime` correspondiente a un **timestamp Unix** dado.

### Parámetros

* timeStamp: valor en segundos desde la era Unix.

### Resultado

Instancia `DateTime` equivalente al timestamp.

### Ejemplo

```csharp
DateTime date = UnixTimestamp.ConvertFrom(1762262400); // 2025-11-03 00:00:00
```