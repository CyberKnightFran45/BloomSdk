# XDes

Cifra y descifra datos usando el algoritmo **DES**, convirtiendo los resultados a formato **Hexadecimal** en mayúsculas.
Permite definir modo de operación y tipo de padding.

## Métodos

▶ `Encrypt(byte[], byte[], byte[], CipherMode, PaddingMode)`

Cifra los datos especificados con DES y devuelve su representación en texto hexadecimal.

### Parámetros

* data: Datos en bytes a cifrar.
* key: Clave simétrica utilizada para el cifrado (8 bytes).
* iv: Vector de inicialización (8 bytes).
* mode: Modo de operación del cifrado (por defecto CBC).
* padding: Tipo de relleno usado en el cifrado (por defecto PKCS7).

### Resultado

Cadena en formato hexadecimal (en mayúsculas) que representa los datos cifrados.

### Ejemplo

```csharp
byte[] key = new byte[8];
byte[] iv = new byte[8];
byte[] data = System.Text.Encoding.UTF8.GetBytes("Mensaje secreto");

using var hexCipher = XDes.Encrypt(data, key, iv);
string encryptedHex = hexCipher.ToString();
```

---

▶ `Decrypt(ReadOnlySpan<char>, byte[], byte[], CipherMode, PaddingMode)`

Descifra una cadena en formato hexadecimal que representa datos cifrados con DES.

### Parámetros

* data: Texto hexadecimal que contiene los datos cifrados.
* key: Clave simétrica utilizada para el descifrado.
* iv: Vector de inicialización (8 bytes).
* mode: Modo de operación del descifrado (por defecto CBC).
* padding: Tipo de relleno usado en el descifrado (por defecto PKCS7).

### Resultado

Arreglo de bytes con los datos descifrados en su forma original.

### Ejemplo

```csharp
byte[] key = new byte[8];
byte[] iv = new byte[8];
string encryptedHex = "A1B2C3D4E5F6";

byte[] decrypted = XDes.Decrypt(encryptedHex, key, iv);
string plainText = System.Text.Encoding.UTF8.GetString(decrypted);
```
