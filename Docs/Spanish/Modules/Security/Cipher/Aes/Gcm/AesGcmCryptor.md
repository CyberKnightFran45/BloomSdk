# AesGcmCryptor

Proporciona funciones para **cifrar y descifrar bytes** usando el algoritmo **AES-GCM** con soporte para nonce aleatorio, datos asociados y tamaños de etiqueta configurables.

## Constantes

| Constante      | Valor | Descripción                                          |
| :------------- | :---: | :--------------------------------------------------- |
| `NONCE_LENGTH` |   12  | Longitud del nonce (en bytes) utilizada por AES-GCM. |

## Métodos

▶ `Encrypt(ReadOnlySpan<byte>, byte[], AesGcmTagSize, ReadOnlySpan<byte>)`

Inicia el cifrado de bytes usando AES-GCM, generando un nonce aleatorio automáticamente.

### Parámetros

* data: bytes a cifrar.
* key: clave de cifrado.
* tagSize: tamaño de la etiqueta de autenticación (opcional, por defecto SIZE_16).
* associatedData: datos asociados para autenticación (opcional).

### Resultado

Bytes cifrados que incluyen nonce y tag.

### Ejemplo

```csharp
byte[] key = RandomNumberGenerator.GetBytes(16);
byte[] plaintext = System.Text.Encoding.UTF8.GetBytes("Mensaje secreto");
using var encrypted = AesGcmCryptor.Encrypt(plaintext, key);
```

---

▶ `Encrypt(ReadOnlySpan<byte>, byte[], ReadOnlySpan<byte>, AesGcmTagSize, ReadOnlySpan<byte>)`

Cifra los bytes usando AES-GCM con un nonce específico.

### Parámetros

* data: bytes a cifrar.
* key: clave de cifrado.
* nonce: nonce específico a usar.
* tagSize: tamaño de la etiqueta de autenticación (opcional, por defecto SIZE_16).
* associatedData: datos asociados para autenticación (opcional).

### Resultado

Bytes cifrados que incluyen nonce y tag.

### Ejemplo

```csharp
byte[] key = RandomNumberGenerator.GetBytes(16);
byte[] nonce = RandomNumberGenerator.GetBytes(12);
byte[] plaintext = System.Text.Encoding.UTF8.GetBytes("Mensaje secreto");
using var encrypted = AesGcmCryptor.Encrypt(plaintext, key, nonce);
```

---

▶ `Decrypt(ReadOnlySpan<byte>, byte[], AesGcmTagSize, ReadOnlySpan<byte>)`

Descifra los bytes cifrados previamente con AES-GCM. Extrae automáticamente nonce y tag del mensaje para la autenticación.

### Parámetros

* data: bytes cifrados que incluyen nonce y tag.
* key: clave de descifrado.
* tagSize: tamaño de la etiqueta de autenticación (opcional, por defecto SIZE_16).
* associatedData: datos asociados para autenticación (opcional).

### Resultado

Bytes descifrados originales.

### Ejemplo

```csharp
byte[] key = RandomNumberGenerator.GetBytes(16);
byte[] encryptedMessage = ...; // resultado de Encrypt
using var decrypted = AesGcmCryptor.Decrypt(encryptedMessage, key);
```