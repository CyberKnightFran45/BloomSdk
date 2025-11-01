# TripleDESCryptor

Proporciona funciones para **cifrar y descifrar bytes** usando el algoritmo **Triple DES (3-DES)**, con soporte para distintos modos de operación y padding.

## Métodos

▶ `Encrypt(byte[], byte[], byte[], CipherMode, PaddingMode)`

Cifra un arreglo de bytes usando **3-DES**.

### Parámetros

* data: Los bytes a cifrar.
* key: Clave de cifrado.
* iv: Vector de inicialización (requerido si el modo no es ECB). Opcional.
* mode: Modo de operación del algoritmo. Por defecto `CipherMode.ECB`.
* padding: Modo de relleno para el bloque de datos. Por defecto `PaddingMode.None`.

### Resultado

Un arreglo de bytes cifrados.

### Ejemplo

```csharp
byte[] key = RandomNumberGenerator.GetBytes(24);
byte[] iv = RandomNumberGenerator.GetBytes(8);
byte[] plaintext = System.Text.Encoding.UTF8.GetBytes("Mensaje secreto");
byte[] encrypted = TripleDESCryptor.Encrypt(plaintext, key, iv, CipherMode.CBC, PaddingMode.PKCS7);
```

---

▶ `Decrypt(byte[], byte[], byte[], CipherMode, PaddingMode)`

Descifra un arreglo de bytes previamente cifrado con **3-DES**.

### Parámetros

* data: Los bytes cifrados.
* key: Clave de descifrado.
* iv: Vector de inicialización (requerido si el modo no es ECB). Opcional.
* mode: Modo de operación del algoritmo. Por defecto `CipherMode.ECB`.
* padding: Modo de relleno usado durante el cifrado. Por defecto `PaddingMode.None`.

### Resultado

Un arreglo de bytes descifrados.

### Ejemplo

```csharp
byte[] key = RandomNumberGenerator.GetBytes(24);
byte[] iv = RandomNumberGenerator.GetBytes(8);
byte[] encrypted = ...; // resultado de Encrypt
byte[] decrypted = TripleDESCryptor.Decrypt(encrypted, key, iv, CipherMode.CBC, PaddingMode.PKCS7);
```