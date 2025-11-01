# SexyColor

Representa un color RGBA en el SexyFramework.

## Propiedades

▶ `Red`

Componente rojo del color.

▶ `Green`

Componente verde del color.

▶ `Blue`

Componente azul del color.

▶ `Alpha`

Opacidad del color.

## Constructores

▶ `SexyColor(int, int, int, int)`

Inicializa un color con los valores RGBA especificados.

### Parámetros

- `r`: valor del componente rojo
- `g`: valor del componente verde
- `b`: valor del componente azul
- `a`: valor del componente alfa

### Ejemplo

```csharp
var color = new SexyColor(255, 128, 64, 200);
```

--------------------------------------------------------

▶ `SexyColor(int, int, int)`

Inicializa un color con los valores RGB especificados y `Alpha` por defecto en 255

### Parámetros

- `r`: valor del componente rojo
- `g`: valor del componente verde
- `b`: valor del componente azul

### Ejemplo

```csharp
var color = new SexyColor(255, 128, 64);
```

## Métodos

▶ `Read(Stream)`

Lee un color desde un flujo binario y devuelve una instancia de `SexyColor`.

### Parámetros

- `reader`: flujo desde el cual se leen los valores del color

### Resultado

- `SexyColor`: color leído

### Ejemplo

```csharp
var color = SexyColor.Read(stream);
```

--------------------------------------------------------

▶ `Write(Stream)`

Escribe los valores del color en un flujo binario.

### Parámetros

- `writer`: flujo donde se escriben los valores del color

### Ejemplo

```csharp
color.Write(stream);
```

## Operadores

▶ `+(SexyColor, SexyColor)`

Suma componente a componente dos colores.

### Ejemplo

```csharp
var result = color1 + color2;
```

--------------------------------------------------------

▶ `-(SexyColor, SexyColor)`

Resta componente a componente dos colores.

### Ejemplo

```csharp
var result = color1 - color2;
```

--------------------------------------------------------

▶ `operator *(SexyColor, byte)`

Multiplica cada componente del color por un factor.

### Parámetros

- `factor`: valor por el cual se multiplican los componentes

### Ejemplo

```csharp
var brighter = color * 2;
```

--------------------------------------------------------

▶ `%(SexyColor, SexyColor)`

Calcula el producto escalar de dos colores (sumatoria de multiplicación de componentes).

### Ejemplo

```csharp
int dot = color1 % color2;
```