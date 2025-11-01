# SubStream

Representa una vista o sub-sección de un `Stream` base, limitada a un rango específico.

## Propiedades

▶ `Length`

Longitud de la sub-sección del stream.

▶ `Position`

Posición actual dentro del `SubStream`.

*Al establecerla, se mueve el cursor relativo dentro del sub-stream.*

▶ `CanRead`

Indica si el stream base permite lectura.

▶ `CanSeek`

Indica si el stream base permite búsqueda (`Seek`).

▶ `CanWrite`

Indica si el stream base permite escritura.

▶ `CanTimeout`

Indica si el stream base soporta timeout.

## Constructores

▶ `SubStream(Stream, long, long)`

Inicializa un `SubStream` sobre un flujo base, comenzando en un offset específico y con longitud determinada.

### Parámetros

- `baseStream`: flujo base donde se crea la sub-sección
- `start`: posición inicial dentro del flujo base
- `length`: longitud de la sub-sección

### Ejemplo

```csharp
var baseStream = new FileStream("archivo.bin", FileMode.Open);
var sub = new SubStream(baseStream, 100, 500); // Vista de 500 bytes desde el byte 100
```

## Métodos

▶ `Flush()`

Vuelca cualquier dato pendiente al flujo base.

### Ejemplo

```csharp
sub.Flush();
```

--------------------------------------------------------

▶ `Seek(long, SeekOrigin)`

Mueve la posición dentro del `SubStream` según el offset y origen especificado.

### Parámetros

* `offset`: desplazamiento a mover
* `origin`: punto de referencia (`Begin`, `Current`, `End`)

### Resultado

* `long`: nueva posición dentro del `SubStream`

### Ejemplo

```csharp
sub.Seek(50, SeekOrigin.Begin);
```

--------------------------------------------------------

▶ `SetLength(long)`

Lanza `NotSupportedException`, ya que no se puede cambiar la longitud de un `SubStream`.

### Ejemplo

```csharp
sub.SetLength(1000); // Lanza excepción
```

--------------------------------------------------------

▶ `Read(Span<byte>)`

Lee bytes del `SubStream` en un buffer `Span<byte>`.

### Parámetros

- `buffer`: destino donde se escriben los bytes leídos

### Resultado

- `int`: cantidad de bytes leídos

### Ejemplo

```csharp
Span<byte> buf = stackalloc byte[100];
int read = sub.Read(buf);
```

--------------------------------------------------------

▶ `Read(byte[], int, int)`

Lee bytes del `SubStream` en un array, usando offset y count.

### Parámetros

- `buffer`: array destino
- `offset`: posición inicial en el array
- `count`: cantidad de bytes a leer

### Resultado

- `int`: cantidad de bytes leídos

### Ejemplo

```csharp
byte[] buf = new byte[100];
int read = sub.Read(buf, 0, 100);
```

--------------------------------------------------------

▶ `Write(ReadOnlySpan<byte>)`

Escribe bytes en el `SubStream` desde un buffer `ReadOnlySpan<byte>`.

### Parámetros

- `buffer`: bytes a escribir

### Ejemplo

```csharp
ReadOnlySpan<byte> data = new byte[]{1,2,3};
sub.Write(data);
```

--------------------------------------------------------

▶ `Write(byte[], int, int)`

Escribe bytes en el `SubStream` desde un array con offset y count.

### Parámetros

- `buffer`: array con los datos
- `offset`: posición inicial en el array
- `count`: cantidad de bytes a escribir

### Ejemplo

```csharp
byte[] data = {1,2,3};
sub.Write(data, 0, 3);
```