# Base64Stream

Representa un stream que codifica o decodifica datos en Base64 sobre un flujo base, con soporte opcional de Base64 “web safe”.

## Propiedades

▶ `CanRead`

Indica si el stream base permite lectura.

▶ `CanWrite`

Indica si el stream base permite escritura.

▶ `CanSeek`

Indica si el stream base permite búsqueda (`Seek`).

▶ `Length`

Longitud del stream.

*Lanza `IOException` si excede `long.MaxValue`.*

▶ `Position`

Posición actual dentro del stream.

* Al establecerla, actualiza internamente `Position64`.
* Lanza `IOException` si excede `long.MaxValue`.

▶ `Position64`

Posición actual como `ulong`.

* Requiere que el stream base permita `Seek`.

## Constructores

▶ `Base64Stream(Stream, bool, ulong)`

Inicializa un `Base64Stream` sobre un flujo base, con opción de Base64 web safe y tamaño de buffer.

### Parámetros

* `baseStream`: flujo base donde se aplicará la codificación Base64
* `webSafe`: indica si se debe usar Base64 web safe
* `bufferSize`: tamaño del buffer interno (por defecto 8192 bytes)

### Ejemplo

```csharp
var fileStream = new FileStream("data.bin", FileMode.OpenOrCreate);
var base64Stream = new Base64Stream(fileStream, webSafe: true);
```

## Métodos

▶ `Flush()`

Vuelca cualquier dato pendiente codificado al flujo base.

### Ejemplo

```csharp
base64Stream.Flush();
```

---

▶ `FlushFinalBlock()`

Vuelca el buffer final y asegura que todos los datos se escriban en el flujo base.

### Ejemplo

```csharp
base64Stream.FlushFinalBlock();
```

---

▶ `Write(byte[], int, int)`

Escribe datos en el stream codificándolos en Base64.

### Parámetros

* `buffer`: array de bytes a escribir
* `offset`: índice inicial en el array
* `count`: cantidad de bytes a escribir

### Ejemplo

```csharp
byte[] data = {1,2,3,4};
base64Stream.Write(data, 0, data.Length);
```

---

▶ `Write(ReadOnlySpan<byte>)`

Escribe datos desde un buffer `ReadOnlySpan<byte>` codificando en Base64.

### Parámetros

* `buffer`: bytes a escribir

### Ejemplo

```csharp
ReadOnlySpan<byte> data = stackalloc byte[] {1,2,3};
base64Stream.Write(data);
```

---

▶ `Read(byte[], int, int)`

Lee datos decodificados desde el stream Base64 en un array.

### Parámetros

* `buffer`: array destino
* `offset`: índice inicial
* `count`: cantidad de bytes a leer

### Resultado

* `int`: número de bytes leídos

### Ejemplo

```csharp
byte[] buffer = new byte[10];
int read = base64Stream.Read(buffer, 0, buffer.Length);
```

---

▶ `Read(Span<byte>)`

Lee datos decodificados desde el stream Base64 en un `Span<byte>`.

### Parámetros

* `buffer`: destino de los bytes leídos

### Resultado

* `int`: número de bytes leídos

### Ejemplo

```csharp
Span<byte> buffer = stackalloc byte[10];
int read = base64Stream.Read(buffer);
```

---

▶ `Seek(long, SeekOrigin)`

Mueve la posición dentro del `Base64Stream`.

### Parámetros

* `offset`: desplazamiento
* `origin`: punto de referencia (`Begin`, `Current`, `End`)

### Resultado

* `long`: nueva posición

### Ejemplo

```csharp
base64Stream.Seek(100, SeekOrigin.Begin);
```

---

▶ `SetLength(long)`

Lanza `NotSupportedException` ya que no se puede cambiar la longitud del stream.

### Ejemplo

```csharp
base64Stream.SetLength(1000); // Excepción
```

---

▶ `Dispose(bool)`

Libera todos los buffers y cierra el flujo base.

### Ejemplo

```csharp
base64Stream.Dispose();
```

---

▶ `GetView(ulong, int)`

Devuelve una vista `ReadOnlySpan<byte>` del buffer interno sin codificar.

### Parámetros

* `offset`: posición inicial (por defecto 0)
* `size`: cantidad de bytes a incluir (por defecto -1 para todo el buffer)

### Resultado

* `ReadOnlySpan<byte>`: vista del buffer

### Ejemplo

```csharp
var span = base64Stream.GetView(0, 100);
```

---

▶ `GetView64(ulong, int)`

Devuelve una vista `ReadOnlySpan<byte>` del buffer codificado en Base64.

### Parámetros

* `offset`: posición inicial (por defecto 0)
* `size`: cantidad de bytes a incluir (por defecto -1)

### Resultado

* `ReadOnlySpan<byte>`: vista del buffer codificado

### Ejemplo

```csharp
var span64 = base64Stream.GetView64(0, 100);
```