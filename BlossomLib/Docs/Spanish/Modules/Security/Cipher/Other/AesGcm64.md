# AesGcm64

Cifra y descifra datos combinando **AES-GCM** con codificación **Base64** en formato web-safe.

## Métodos

▶ `Encrypt(ReadOnlySpan<byte>, byte[], AesGcmTagSize, ReadOnlySpan<byte>)`

Cifra datos con AES-GCM utilizando un *nonce* generado aleatoriamente y los codifica en Base64.

### Parámetros

* data: Datos a cifrar.
* key: Clave simétrica utilizada para el cifrado.
* tagSize: Tamaño de la etiqueta de autenticación (por defecto 16 bytes).
* associatedData: Datos adicionales asociados (opcional).

### Resultado

Cadena Base64 segura para web que contiene el mensaje cifrado junto con el *nonce* y la etiqueta de autenticación.

### Ejemplo

```csharp
byte[] key = ...;
byte[] data = System.Text.Encoding.UTF8.GetBytes("Mensaje seguro");

NativeString result = AesGcm64.Encrypt(data, key);
```

---

▶ `Encrypt(ReadOnlySpan<byte>, byte[], ReadOnlySpan<byte>, AesGcmTagSize, ReadOnlySpan<byte>)`

Cifra datos con AES-GCM usando un *nonce* específico y los codifica en Base64.

### Parámetros

* data: Datos a cifrar.
* key: Clave simétrica utilizada para el cifrado.
* nonce: Vector de inicialización (nonce) proporcionado por el usuario.
* tagSize: Tamaño de la etiqueta de autenticación (por defecto 16 bytes).
* associatedData: Datos adicionales asociados (opcional).

### Resultado

Cadena Base64 segura para web que contiene el mensaje cifrado junto con el *nonce* y la etiqueta de autenticación.

### Ejemplo

```csharp
byte[] key = ...;
byte[] nonce = new byte[12]; // Debe ser de 12 bytes
RandomNumberGenerator.Fill(nonce);
byte[] data = System.Text.Encoding.UTF8.GetBytes("Texto confidencial");

NativeString encoded = AesGcm64.Encrypt(data, key, nonce);
```

---

▶ `Decrypt(ReadOnlySpan<char>, byte[], AesGcmTagSize, ReadOnlySpan<byte>)`

Descifra una cadena codificada en Base64 previamente cifrada con AES-GCM.

### Parámetros

* data: Texto codificado en Base64 que contiene los datos cifrados.
* key: Clave simétrica utilizada para el descifrado.
* tagSize: Tamaño de la etiqueta de autenticación (por defecto 16 bytes).
* associatedData: Datos adicionales asociados (opcional).

### Resultado

Arreglo de bytes con los datos descifrados correctamente.

### Ejemplo

```csharp
byte[] key = ...;
string encoded = encodedResult.ToString();

using var decrypted = AesGcm64.Decrypt(encoded, key);
byte[] plain = decrypted.ToArray();
```