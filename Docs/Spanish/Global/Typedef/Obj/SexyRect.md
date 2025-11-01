# SexyRect

Representa un rectángulo 2D en el SexyFramework.

## Propiedades

▶ `X`

Coordenada X del rectángulo.

▶ `Y`

Coordenada Y del rectángulo.

▶ `Width`

Ancho del rectángulo.

▶ `Height`

Alto del rectángulo.

## Constructores

▶ `SexyRect()`

Inicializa un rectángulo con todas las coordenadas y dimensiones por defecto `(0,0,0,0)`.

### Ejemplo

```csharp
var rect = new SexyRect();
```

--------------------------------------------------------

▶ `SexyRect(int, int, int, int)`

Inicializa un rectángulo con coordenadas y dimensiones especificadas.

### Parámetros

- `x`: coordenada X
- `y`: coordenada Y
- `width`: ancho del rectángulo
- `height`: alto del rectángulo

### Ejemplo

```csharp
var rect = new SexyRect(10, 20, 100, 50);
```

## Métodos

▶ `Read(Stream)`

Lee un rectángulo desde un flujo binario y devuelve una instancia de `SexyRect`.

### Parámetros

- `reader`: flujo desde el cual se leen las coordenadas y dimensiones

### Resultado

- `SexyRect`: rectángulo leído desde el flujo

### Ejemplo

```csharp
var rect = SexyRect.Read(stream);
```

--------------------------------------------------------

▶ `Write(Stream)`

Escribe las coordenadas y dimensiones del rectángulo en un flujo binario.

### Parámetros

- `writer`: flujo donde se escriben los datos del rectángulo

### Ejemplo

```csharp
rect.Write(stream);
```