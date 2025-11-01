# SexyPoint

Representa un punto 2D en el SexyFramework.

## Propiedades

▶ `X`

Coordenada X del punto.

▶ `Y`

Coordenada Y del punto.

## Constructores

▶ `SexyPoint()`

Inicializa un punto con coordenadas por defecto `(0,0)`.

### Ejemplo

```csharp
var point = new SexyPoint();
```

--------------------------------------------------------

▶ `SexyPoint(int, int)`

Inicializa un punto con coordenadas X e Y especificadas.

### Parámetros

- `x`: coordenada X
- `y`: coordenada Y

### Ejemplo

```csharp
var point = new SexyPoint(10, 20);
```

## Métodos

▶ `Read(Stream)`

Lee un punto desde un flujo binario y devuelve una instancia de `SexyPoint`.

### Parámetros

- `reader`: flujo desde el cual se leen las coordenadas

### Resultado

- `SexyPoint`: punto leído desde el flujo

### Ejemplo

```csharp
var point = SexyPoint.Read(stream);
```

--------------------------------------------------------

▶ `Write(Stream)`

Escribe las coordenadas del punto en un flujo binario.

### Parámetros

- `writer`: flujo donde se escriben las coordenadas

### Ejemplo

```csharp
point.Write(stream);
```