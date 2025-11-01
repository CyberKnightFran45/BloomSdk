# Aes64

Proporciona utilidades para cifrar y descifrar datos combinando **AES** con codificación **Base64** en formato web-safe.

## Métodos

▶ `Encrypt(byte[], byte[], byte[], CipherMode, PaddingMode)`

Cifra un arreglo de bytes utilizando el algoritmo AES y codifica el resultado en Base64 compatible con entornos web.

### Parámetros

* data: Datos a cifrar.
* key: Clave simétrica utilizada para el cifrado.
* iv: Vector de inicialización (opcional).
* mode: Modo de cifrado a utilizar (por defecto ECB).
* padding: Esquema de relleno (por defecto PKCS7).

### Resultado

Cadena Base64 segura para web que contiene los datos cifrados.

### Ejemplo

```csharp
byte[] key = ...;
byte[] iv = ...;
byte[] data = System.Text.Encoding.UTF8.GetBytes("Mensaje secreto");

NativeString result = Aes64.Encrypt(data, key, iv);
```

---

▶ `Decrypt(ReadOnlySpan<char>, byte[], byte[], CipherMode, PaddingMode)`

Descifra una cadena codificada en Base64 que fue previamente cifrada con AES.

### Parámetros

* data: Texto codificado en Base64 que contiene los datos cifrados.
* key: Clave simétrica usada para el descifrado.
* iv: Vector de inicialización (opcional).
* mode: Modo de cifrado a utilizar (por defecto ECB).
* padding: Esquema de relleno (por defecto PKCS7).

### Resultado

Arreglo de bytes con los datos descifrados.

### Ejemplo

```csharp
byte[] key = ...;
byte[] iv = ...;
string encoded = result.ToString();

byte[] decrypted = Aes64.Decrypt(encoded, key, iv);
```