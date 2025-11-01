# Crc32

Calcula el **checksum CRC-32** de un arreglo de bytes o de un flujo usando la biblioteca `ICSharpCode.SharpZipLib`

## Métodos

▶ `Calculate(byte[])`

Obtiene el checksum CRC-32 de un arreglo de bytes.

### Parámetros

* data: arreglo de bytes a procesar.

### Resultado

* Retorna el checksum CRC-32 como `long`.

### Ejemplo

```csharp
byte[] data = System.Text.Encoding.UTF8.GetBytes("Hola Mundo");
long checksum = Crc32.Calculate(data);
Console.WriteLine(checksum);
```

---

▶ `Calculate(Stream)`

Obtiene el checksum CRC-32 de un flujo de datos.

### Parámetros

* input: flujo desde el cual se obtendrá el checksum.

### Resultado

* Retorna el checksum CRC-32 como `long`.

### Ejemplo

```csharp
using var fs = File.OpenRead("archivo.txt");
long checksum = Crc32.Calculate(fs);
Console.WriteLine(checksum);
```