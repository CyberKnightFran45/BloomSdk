# BaseStreamHandler

Clase base para manejar operaciones comunes sobre streams.

## Propiedades

▶ `BaseStream`

Stream subyacente que se gestiona.

▶ `Length`

Longitud del stream.

▶ `Position`

Posición actual dentro del stream.

▶ `IsClosed`

Indica si el stream está cerrado o no permite lectura/escritura.

▶ `IsMemoryStream`

Indica si el stream subyacente es un `MemoryStream`.

## Constructores

▶ `BaseStreamHandler(bool)`

Inicializa un handler con un `ChunkedMemoryStream` por defecto.

### Parámetros

* `leaveOpen`: indica si el stream subyacente debe permanecer abierto al cerrar el handler (por defecto `false`)

### Ejemplo

```csharp
var handler = new BaseStreamHandler(true);
```

---

▶ `BaseStreamHandler(Stream, bool)`

Inicializa un handler con un stream existente.

### Parámetros

* `source`: stream a manejar
* `leaveOpen`: indica si el stream subyacente debe permanecer abierto al cerrar el handler

### Ejemplo

```csharp
var fileStream = new FileStream("data.bin", FileMode.Open);
var handler = new BaseStreamHandler(fileStream, true);
```

## Métodos

▶ `Close()`

Cierra el stream y libera los recursos asociados.

### Ejemplo

```csharp
handler.Close();
```

---

▶ `Dispose()`

Libera todos los recursos consumidos por el handler y su stream.

### Ejemplo

```csharp
handler.Dispose();
```

---

▶ `Dispose(bool)`

Libera recursos del stream según el parámetro `disposing`.

### Parámetros

* `disposing`: determina si se deben descartar todos los recursos

### Ejemplo

```csharp
handler.Dispose(true);
```

---

▶ `Flush()`

Limpia todos los buffers y escribe cualquier dato pendiente en el dispositivo subyacente.

### Ejemplo

```csharp
handler.Flush();
```

---

▶ `Seek(long, SeekOrigin)`

Cambia la posición actual del stream.

### Parámetros

* `offset`: desplazamiento a mover
* `origin`: punto de referencia (`Begin`, `Current`, `End`)

### Resultado

* `long`: nueva posición dentro del stream

### Ejemplo

```csharp
handler.Seek(100, SeekOrigin.Begin);
```

---

▶ `SetLength(long)`

Cambia la longitud del stream subyacente.

### Parámetros

* `length`: nueva longitud del stream

### Ejemplo

```csharp
handler.SetLength(1024);
```

---

▶ `implicit operator Stream(BaseStreamHandler)`

Permite usar el handler como un `Stream` implícitamente.

### Ejemplo

```csharp
Stream s = handler;
```