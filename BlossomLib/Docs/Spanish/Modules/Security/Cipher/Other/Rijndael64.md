# Rijndael64

Cifra y descifra datos usando el algoritmo **Rijndael (AES)** combinado con codificación **Base64** en formato web-safe.
Permite personalizar tamaño de bloque, modo de operación y tipo de relleno.

## Métodos

▶ `Encrypt(ReadOnlySpan<byte>, ReadOnlySpan<byte>, ReadOnlySpan<byte>, RijndaelBlockSize, RijndaelMode, RijndaelPadding)`

Cifra los datos proporcionados usando Rijndael y devuelve el resultado codificado en Base64.

### Parámetros

* data: Datos a cifrar.
* key: Clave simétrica utilizada para el cifrado.
* iv: Vector de inicialización (IV) usado en los modos que lo requieran (opcional).
* blockSize: Tamaño de bloque de cifrado (por defecto 16 bytes).
* mode: Modo de operación del cifrado (por defecto CBC).
* padding: Esquema de relleno a utilizar (por defecto ZeroPadding).

### Resultado

Cadena Base64 segura para web que contiene los datos cifrados.

### Ejemplo

```csharp
byte[] key = new byte[32];
byte[] iv = new byte[16];
byte[] data = System.Text.Encoding.UTF8.GetBytes("Mensaje secreto");

NativeString encoded = Rijndael64.Encrypt(data, key, iv);
```

---

▶ `Decrypt(ReadOnlySpan<char>, ReadOnlySpan<byte>, ReadOnlySpan<byte>, RijndaelBlockSize, RijndaelMode, RijndaelPadding)`

Descifra una cadena codificada en Base64 que fue cifrada usando Rijndael.

### Parámetros

* data: Texto codificado en Base64 que contiene los datos cifrados.
* key: Clave simétrica utilizada para el descifrado.
* iv: Vector de inicialización (IV) usado en los modos que lo requieran (opcional).
* blockSize: Tamaño de bloque de cifrado (por defecto 16 bytes).
* mode: Modo de operación del cifrado (por defecto CBC).
* padding: Esquema de relleno utilizado durante el cifrado (por defecto ZeroPadding).

### Resultado

Arreglo de bytes con los datos descifrados correctamente.

### Ejemplo

```csharp
byte[] key = new byte[32];
byte[] iv = new byte[16];
string encoded = encodedResult.ToString();

using var decrypted = Rijndael64.Decrypt(encoded, key, iv);
byte[] plain = decrypted.ToArray();
```