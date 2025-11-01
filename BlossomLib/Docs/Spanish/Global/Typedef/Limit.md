# Limit<T>

Representa un límite que un parámetro debe seguir, con un rango mínimo y máximo.

## Propiedades

▶ `MinValue`

Obtiene o establece el valor mínimo del límite.

▶ `MaxValue`

Obtiene o establece el valor máximo del límite.

## Constructores

▶ `Limit()`

Crea una nueva instancia de `Limit<T>` con valores por defecto.

### Ejemplo

```csharp
var limit = new Limit<int>();
````

--------------------------------------------------------

▶ `Limit(T)`

Crea una nueva instancia de `Limit<T>` con un único valor como rango.

### Parámetros

- `range`: el valor a usar como `MinValue` y `MaxValue`.

### Ejemplo

```csharp
var limit = new Limit<int>(5); // MinValue = 5, MaxValue = 5
```

--------------------------------------------------------

▶ `Limit(T min, T max)`

Crea una nueva instancia de `Limit<T>` con un rango mínimo y máximo específico.

### Parámetros

- `min`: valor mínimo permitido.
- `max`: valor máximo permitido.

### Ejemplo

```csharp
var limit = new Limit<int>(1, 10);
```

## Métodos

▶ `IsParamInRange(T)`

Verifica si un parámetro se encuentra dentro del rango especificado.

### Parámetros

- `target`: el valor a analizar.

### Resultado

`true` si `target` está entre `MinValue` y `MaxValue`, `false` en caso contrario.

### Ejemplo

```csharp
bool ok = limit.IsParamInRange(5); // true si 1 <= 5 <= 10
```

--------------------------------------------------------

▶ `CheckParamRange(T)`

Verifica y ajusta un parámetro para que esté dentro del rango especificado.

### Parámetros

- `target`: el valor a analizar.

### Resultado

`target` si está dentro del rango; de lo contrario, devuelve `MinValue`.

### Ejemplo

```csharp
int val = limit.CheckParamRange(0); // devuelve 1 si MinValue = 1
```

--------------------------------------------------------

▶ `GetRange()`

Obtiene un rango completo para el tipo `T` según su tipo nativo.

### Resultado

Una nueva instancia de `Limit<T>` con los valores mínimos y máximos posibles según `T`.

### Ejemplo

```csharp
var intRange = Limit<int>.GetRange(); // MinValue = int.MinValue, MaxValue = int.MaxValue
```