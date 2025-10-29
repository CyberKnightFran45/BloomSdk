# SizeT

Esta clase se encarga de manejar tamaños de archivos y operaciones de padding.  

## Constantes

| Constante       | Valor   | Descripción                                 |
|:----------------|:-------:|--------------------------------------------|
| `ONE_KILOBYTE`  | 1024 b  | Cantidad de bytes en un kilobyte           |
| `ONE_MEGABYTE`  | 1024 KB | Cantidad de kilobytes en un megabyte       |
| `ONE_GIGABYTE`  | 1024 MB | Cantidad de megabytes en un gigabyte       |
| `ONE_TERABYTE`  | 1024 GB | Cantidad de gigabytes en un terabyte       |
| `ONE_PETABYTE`  | 1024 TB | Cantidad de terabytes en un petabyte       |
| `MAX_STACK`     | 2 KB    | Capacidad máxima de una pila o stack       |

## Métodos

▶ `string FormatSize(long)`

Convierte un tamaño en bytes a un formato legible (KB, MB, GB, etc.)

### Parámetros

- `size`: cantidad de bytes a formatear.

### Resultado

Un `string` con el tamaño formateado seguido de su unidad.

### Ejemplo

```csharp
long size = 150000;
string readable = SizeT.FormatSize(size); // "1.5 MB"
```

--------------------------------------------------------

▶ `GetPaddedLen(long, int)`

Calcula la longitud alineada al siguiente múltiplo de `n` bytes.

### Parámetros

- `length`: la longitud original a alinear.
- `n`: el múltiplo deseado (por ejemplo, 16, 64, ó 1024).

### Resultado

Un `long` que representa la longitud alineada (igual o superior a la original).

### Ejemplo

```csharp
long padded = SizeT.GetPaddedLen(30, 16); // 32
```

-----------------------------------------------------

▶ `GetPadding(long, int)`

Obtiene la cantidad de bytes de padding necesarios para alinear la longitud al múltiplo especificado.

### Parámetros

- `length`: la longitud original.
- `n`: el múltiplo deseado (por ejemplo, 16, 64, ó 1024).

### Resultado

Un `long` que representa la cantidad de bytes necesarios para llegar al múltiplo exacto.

### Ejemplo

```csharp
long padding = SizeT.GetPadding(30, 16); // 2
```

---------------------------------------------------

▶ `GetBlockCount(long, int)`

Calcula cuántos bloques del tamaño especificado son necesarios para contener la longitud dada (considerando padding si es necesario).

### Parámetros

- `length`: la longitud original.
- `n`: tamaño del bloque en bytes.

### Resultado

Un `long` que representa la cantidad total de bloques requeridos.

### Ejemplo

```csharp
long blocks = SizeT.GetBlockCount(30, 16); // 2
```

-----------------------------------------------------

▶ `GetOriginaLen(long, int)`

Obtiene la longitud original a partir de un tamaño padded y el tamaño de bloque.

### Parámetros

- `paddedLen`: la longitud ya alineada o padded.
- `n`: tamaño del bloque en bytes.

### Resultado

Un `long` que representa la longitud original antes de aplicar padding.

### Ejemplo

```csharp
long original = SizeT.GetOriginaLen(32, 16); // 30
```
