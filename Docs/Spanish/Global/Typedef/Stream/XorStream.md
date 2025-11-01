# XorStream

Representa un flujo que aplica un cifrado XOR byte a byte sobre los datos, útil para ofuscación simple de información en memoria o archivos.

---

## Constructores

▶ `XorStream(byte)`

Inicializa un flujo en memoria con la clave XOR especificada.

### Parámetros

* `key`: byte usado como clave XOR.

### Ejemplo

```csharp
var xorStream = new XorStream(0xAA);
```

---

▶ `XorStream(Stream, byte, bool)`

Inicializa un flujo a partir de un flujo existente aplicando la clave XOR. Permite decidir si se deja abierto el flujo base al cerrar.

### Parámetros

* `source`: flujo base sobre el que se aplicará XOR.
* `key`: byte usado como clave XOR.
* `leaveOpen`: indica si se deja abierto el flujo base al cerrar el XorStream.

### Ejemplo

```csharp
using var fileStream = File.OpenRead("data.bin");
var xorStream = new XorStream(fileStream, 0xAA, true);
```

---

## Métodos Públicos

▶ `OpenRead(string, byte)`

Abre un archivo para lectura y devuelve un flujo XorStream.

### Parámetros

* `path`: ruta del archivo a leer.
* `key`: clave XOR.

### Resultado

* `XorStream`: flujo listo para leer datos decodificados.

### Ejemplo

```csharp
using var xorStream = XorStream.OpenRead("secret.bin", 0xAA);
```

---

▶ `OpenWrite(string, byte)`

Abre un archivo para escritura y devuelve un flujo XorStream.

### Parámetros

* `path`: ruta del archivo a escribir.
* `key`: clave XOR.

### Resultado

* `XorStream`: flujo listo para escribir datos codificados.

### Ejemplo

```csharp
using var xorStream = XorStream.OpenWrite("secret.bin", 0xAA);
```

---

▶ `Read(Span<byte>)`

Lee bytes del flujo aplicando XOR y los coloca en el buffer.

### Parámetros

* `buffer`: buffer donde se colocarán los bytes decodificados.

### Resultado

* `int`: número de bytes leídos.

### Ejemplo

```csharp
Span<byte> buffer = stackalloc byte[1024];
int bytesRead = xorStream.Read(buffer);
```

---

▶ `Read(byte[], int, int)`

Lee bytes en un arreglo desde el flujo aplicando XOR.

### Parámetros

* `buffer`: arreglo donde se almacenarán los bytes decodificados.
* `offset`: posición inicial en el buffer.
* `count`: número máximo de bytes a leer.

### Resultado

* `int`: número de bytes leídos.

### Ejemplo

```csharp
byte[] buffer = new byte[1024];
int bytesRead = xorStream.Read(buffer, 0, buffer.Length);
```

---

▶ `ReadByte()`

Lee un solo byte del flujo aplicando XOR.

### Resultado

* `int`: byte leído o -1 si se alcanza el final del flujo.

### Ejemplo

```csharp
int b = xorStream.ReadByte();
```

---

▶ `Write(ReadOnlySpan<byte>)`

Escribe bytes en el flujo aplicando XOR.

### Parámetros

* `buffer`: bytes a escribir.

### Ejemplo

```csharp
Span<byte> data = stackalloc byte[] { 1, 2, 3 };
xorStream.Write(data);
```

---

▶ `Write(byte[], int, int)`

Escribe bytes desde un arreglo aplicando XOR.

### Parámetros

* `buffer`: arreglo de bytes a escribir.
* `offset`: posición inicial en el arreglo.
* `count`: cantidad de bytes a escribir.

### Ejemplo

```csharp
byte[] data = new byte[] { 1, 2, 3 };
xorStream.Write(data, 0, data.Length);
```

---

▶ `WriteByte(byte)`

Escribe un solo byte aplicando XOR.

### Parámetros

* `val`: byte a escribir.

### Ejemplo

```csharp
xorStream.WriteByte(0xFF);
```

---

▶ `Flush()`

Vuelca los datos pendientes al flujo base.

### Ejemplo

```csharp
xorStream.Flush();
```

---

▶ `Seek(long, SeekOrigin)`

Cambia la posición del flujo.

### Parámetros

* `offset`: desplazamiento.
* `origin`: punto de referencia (inicio, actual, fin).

### Resultado

* `long`: nueva posición del flujo.

### Ejemplo

```csharp
xorStream.Seek(0, SeekOrigin.Begin);
```

---

▶ `SetLength(long)`

Cambia la longitud del flujo base.

### Parámetros

* `length`: nueva longitud.

### Ejemplo

```csharp
xorStream.SetLength(1024);
```