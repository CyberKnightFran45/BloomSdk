# AesCryptor

Proporciona funciones para cifrar y descifrar datos usando **AES (Advanced Encryption Standard)** con soporte para distintos modos de operación y esquemas de padding.

---

## Métodos

▶ `Encrypt(byte[], byte[], byte[] iv, CipherMode, PaddingMode)`

Cifra un array de bytes usando AES.

### Parámetros

* data: datos a cifrar.
* key: clave AES.
* iv: vector de inicialización (requerido para modos distintos de ECB).
* mode: modo de operación de AES (`ECB` por default).
* padding: esquema de padding (`PKCS7` por default).

### Resultado

* Retorna un `byte[]` con los datos cifrados.

### Ejemplo

```csharp
byte[] plain = System.Text.Encoding.UTF8.GetBytes("Hola Mundo");
byte[] key = new byte[16]; // AES-128
byte[] iv = new byte[16];
var ciphered = AesCryptor.Encrypt(plain, key, iv, CipherMode.CBC);
```

---

▶ `Decrypt(byte[], byte[], byte[], CipherMode, PaddingMode)`

Descifra datos previamente cifrados con `Encrypt`.

### Parámetros

* data: datos cifrados.
* key: misma clave usada para cifrar.
* iv: vector de inicialización usado durante el cifrado.
* mode: mismo modo de operación usado durante el cifrado.
* padding: mismo esquema de padding usado durante el cifrado.

### Resultado

* Retorna un `byte[]` con los datos descifrados.

### Ejemplo

```csharp
var decrypted = AesCryptor.Decrypt(ciphered, key, iv, CipherMode.CBC);
string original = System.Text.Encoding.UTF8.GetString(decrypted);
```