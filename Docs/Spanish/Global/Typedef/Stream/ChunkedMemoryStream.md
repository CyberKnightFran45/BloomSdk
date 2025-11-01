# ChunkedMemoryStream

Stream en memoria que gestiona los datos en **chunks** de tamaño configurable, útil para manejar grandes cantidades de datos sin fragmentar la memoria contigua.

## Propiedades

▶ `CanRead`

Indica si el stream puede leerse (no está cerrado).

▶ `CanSeek`

Indica si el stream soporta búsqueda (`Seek`).

▶ `CanWrite`

Indica si el stream puede escribirse (no está cerrado).

▶ `Length`

Longitud actual del stream en bytes.

▶ `Position`

Posición actual dentro del stream.

## Constructores

▶ `ChunkedMemoryStream(int)`

Inicializa un stream en memoria segmentado en chunks.

### Parámetros

* `chunkSize`: tamaño de cada chunk en bytes (por defecto 8 MB)

### Ejemplo

```csharp
var cms = new ChunkedMemoryStream();
var cms2 = new ChunkedMemoryStream(4 * 1024 * 1024); // chunks de 4 MB
```

## Métodos

▶ `Flush()`

No realiza ninguna operación ya que es un stream en memoria.

### Ejemplo

```csharp
cms.Flush();
```

---

▶ `Read(byte[], int, int)`

Lee datos del stream en un array de bytes.

### Parámetros

* `buffer`: array destino
* `offset`: índice inicial
* `count`: cantidad de bytes a leer

### Resultado

* `int`: cantidad de bytes leídos

### Ejemplo

```csharp
byte[] buffer = new byte[1024];
int read = cms.Read(buffer, 0, buffer.Length);
```

---

▶ `Read(Span<byte>)`

Lee datos del stream en un `Span<byte>`.

### Parámetros

* `buffer`: destino de los bytes leídos

### Resultado

* `int`: cantidad de bytes leídos

### Ejemplo

```csharp
Span<byte> buffer = stackalloc byte[512];
int read = cms.Read(buffer);
```

---

▶ `Write(byte[], int, int)`

Escribe datos en el stream desde un array de bytes.

### Parámetros

* `buffer`: array fuente
* `offset`: índice inicial
* `count`: cantidad de bytes a escribir

### Ejemplo

```csharp
byte[] data = new byte[1000];
cms.Write(data, 0, data.Length);
```

---

▶ `Write(ReadOnlySpan<byte>)`

Escribe datos en el stream desde un `ReadOnlySpan<byte>`.

### Parámetros

* `buffer`: bytes a escribir

### Ejemplo

```csharp
ReadOnlySpan<byte> span = stackalloc byte[256];
cms.Write(span);
```

---

▶ `Seek(long, SeekOrigin)`

Mueve la posición dentro del stream.

### Parámetros

* `offset`: desplazamiento
* `origin`: punto de referencia (`Begin`, `Current`, `End`)

### Resultado

* `long`: nueva posición

### Ejemplo

```csharp
cms.Seek(1024, SeekOrigin.Begin);
```

---

▶ `SetLength(long)`

Cambia la longitud del stream. Puede expandir los chunks si es necesario.

### Parámetros

* `value`: nueva longitud del stream

### Ejemplo

```csharp
cms.SetLength(16 * 1024 * 1024); // 16 MB
```

---

▶ `Dispose(bool)`

Libera los buffers y marca el stream como cerrado.

### Ejemplo

```csharp
cms.Dispose();
```

---

▶ `ThrowIfDisposed()`

Lanza `ObjectDisposedException` si el stream ha sido liberado.

### Ejemplo

```csharp
cms.ThrowIfDisposed();
```