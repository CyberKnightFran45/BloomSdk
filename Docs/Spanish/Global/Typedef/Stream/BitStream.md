# BitStream

Permite leer y escribir bits individuales o en grupos dentro de un stream.

## Propiedades

▶ `BaseStream`

Stream subyacente gestionado por `BaseStreamHandler`.

▶ `_bitsPosition`

Posición actual dentro del byte en curso (0–7).

▶ `_bitBuffer`

Buffer interno para escribir bits en bytes completos.

## Constructores

▶ `BitStream(bool)`

Inicializa un `BitStream` con un stream interno por defecto (`ChunkedMemoryStream`).

### Parámetros

* `leaveOpen`: indica si el stream subyacente debe permanecer abierto al cerrar el `BitStream`.

### Ejemplo

```csharp
var bitStream = new BitStream(true);
```

---

▶ `BitStream(Stream, bool)`

Inicializa un `BitStream` sobre un stream existente.

### Parámetros

* `source`: stream subyacente
* `leaveOpen`: indica si el stream subyacente debe permanecer abierto

### Ejemplo

```csharp
var fileStream = new FileStream("data.bin", FileMode.Open);
var bitStream = new BitStream(fileStream, true);
```

## Métodos

▶ `OpenRead(string)`

Abre un `BitStream` para lectura desde un archivo.

### Parámetros

* `path`: ruta del archivo a leer

### Resultado

* `BitStream`: instancia lista para lectura

### Ejemplo

```csharp
var reader = BitStream.OpenRead("data.bin");
```

---

▶ `OpenWrite(string)`

Abre un `BitStream` para escritura en un archivo.

### Parámetros

* `path`: ruta del archivo a escribir

### Resultado

* `BitStream`: instancia lista para escritura

### Ejemplo

```csharp
var writer = BitStream.OpenWrite("data.bin");
```

---

▶ `ReadOneBit()`

Lee un solo bit del stream.

### Resultado

* `int`: valor del bit (`0` o `1`), o `-1` si se alcanza el final del stream

### Ejemplo

```csharp
int bit = bitStream.ReadOneBit();
```

---

▶ `ReadBits(int)`

Lee varios bits consecutivos y devuelve su valor como entero.

### Parámetros

* `count`: cantidad de bits a leer

### Resultado

* `int`: valor de los bits leídos

### Ejemplo

```csharp
int value = bitStream.ReadBits(5); // lee 5 bits
```

---

▶ `WriteOneBit(int)`

Escribe un solo bit en el stream.

### Parámetros

* `bit`: valor del bit a escribir (`0` o `1`)

### Ejemplo

```csharp
bitStream.WriteOneBit(1);
```

---

▶ `WriteBits(int, int)`

Escribe varios bits consecutivos en el stream.

### Parámetros

* `val`: valor de los bits a escribir
* `count`: cantidad de bits a escribir

### Ejemplo

```csharp
bitStream.WriteBits(0b10101, 5); // escribe 5 bits
```
