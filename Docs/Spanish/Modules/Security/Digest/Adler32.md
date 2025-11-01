# Adler32

Calcula el **checksum Adler-32** de un arreglo de bytes o de un flujo.

## Constantes

▶ `MOD_ADLER`

Valor máximo para el checksum Adler32 (`65521`).

## Métodos

▶ `Calculate(ReadOnlySpan<byte>)`

Obtiene el checksum Adler-32 de un arreglo de bytes.

### Parámetros

* data: arreglo de bytes a procesar.

### Resultado

* Retorna el checksum Adler-32 como `uint`.

### Ejemplo

```csharp
byte[] data = System.Text.Encoding.UTF8.GetBytes("Hola Mundo");
uint checksum = Adler32.Calculate(data);
Console.WriteLine(checksum);
```

---

▶ `Calculate(Stream)`

Obtiene el checksum Adler-32 de un flujo de datos.

### Parámetros

* input: flujo desde el cual se obtendrá el checksum.

### Resultado

* Retorna el checksum Adler-32 como `uint`.

### Ejemplo

```csharp
using var fs = File.OpenRead("archivo.txt");
uint checksum = Adler32.Calculate(fs);
Console.WriteLine(checksum);
```