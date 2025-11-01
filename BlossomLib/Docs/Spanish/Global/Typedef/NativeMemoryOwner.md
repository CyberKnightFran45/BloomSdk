# NativeMemoryOwner<T>

Representa un propietario de memoria para tipos no gestionados (`unmanaged`) usando memoria nativa. Permite asignar, liberar, y manipular bloques de memoria de manera segura.

## Propiedades

▶ `Size`

Obtiene el tamaño de la memoria asignada, en elementos del tipo `T`.

## Constructores

▶ `NativeMemoryOwner()`

Crea una instancia sin memoria asignada.

### Ejemplo

```csharp
var mem = new NativeMemoryOwner<int>();
```

--------------------------------------------------------

▶ `NativeMemoryOwner(ulong)`

Crea una instancia asignando `len` elementos de tipo `T`.

### Parámetros

- `len`: número de elementos a asignar.

### Ejemplo

```csharp
var mem = new NativeMemoryOwner<int>(100); // 100 elementos de int
```

--------------------------------------------------------

▶ `NativeMemoryOwner(long)`

Crea una instancia asignando `len` bytes de memoria (si `len < 0`, asigna 0).

### Parámetros

- `len`: número de bytes a asignar.

### Ejemplo

```csharp
var mem = new NativeMemoryOwner<byte>(1024); // 1024 bytes
```

## Métodos

▶ `AsSpan(ulong, int)`

Crea un `Span<T>` sobre la memoria asignada, a partir del offset indicado.

### Parámetros

- `offset`: posición inicial en elementos (0 por default).
- `length`: cantidad de elementos a incluir; si es -1, incluye hasta el final.

### Resultado

Un `Span<T>` sobre la memoria especificada.

### Ejemplo

```csharp
var span = mem.AsSpan(10, 20); // toma 20 elementos desde el offset 10
```

--------------------------------------------------------

▶ `Move(ulong, ulong, ulong)`

Copia elementos dentro de la memoria desde una posición a otra.

### Parámetros

- `sourceOffset`: offset de inicio del origen.
- `destinationOffset`: offset de inicio del destino.
- `count`: número de elementos a copiar.

### Ejemplo

```csharp
mem.Move(0, 50, 10); // mueve 10 elementos desde 0 a 50
```

--------------------------------------------------------

▶ `ToArray(ulong, int)`

Convierte la memoria asignada a un array de tipo `T`.

### Parámetros

- `offset`: posición inicial en elementos (0 por default).
- `length`: cantidad de elementos a copiar; -1 copia hasta el final.

### Resultado

Un array `T[]` con los datos de la memoria.

### Ejemplo

```csharp
T[] array = mem.ToArray(); // copia todo
```

--------------------------------------------------------

▶ `Fill(T v)`

Llena toda la memoria con un valor.

### Parámetros

- `v`: valor a asignar a cada elemento.

### Ejemplo

```csharp
mem.Fill(0); // llena toda la memoria con ceros
```

--------------------------------------------------------

▶ `Clear()`

Limpia la memoria asignada, estableciendo todos los bytes a cero.

### Ejemplo

```csharp
mem.Clear();
```

--------------------------------------------------------

▶ `Realloc(ulong)`

Reasigna la memoria a un nuevo tamaño,  si `_ptr` era null, asigna memoria nueva.

### Parámetros

- `n`: nueva cantidad de elementos.

### Ejemplo

```csharp
mem.Realloc(200);
```

--------------------------------------------------------

▶ `Dispose()`

Libera la memoria asignada si no se ha liberado aún.

### Ejemplo

```csharp
mem.Dispose();
```