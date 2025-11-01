# X3Des

Cifra y descifra datos usando el algoritmo **Triple DES (3-DES)**, convirtiendo los resultados a formato **Hexadecimal** en mayúsculas. Permite definir modo de operación y tipo de padding.

## Métodos

▶ `Encrypt(byte[], byte[], byte[], CipherMode, PaddingMode)`

Cifra los datos especificados con 3-DES y devuelve su representación en texto hexadecimal.

### Parámetros

* data: Datos en bytes a cifrar.
* key: Clave simétrica utilizada para el cifrado (24 bytes recomendados).
* iv: Vector de inicialización (opcional, depende del modo).
* mode: Modo de operación del cifrado (por defecto ECB).
* padding: Tipo de relleno usado en el cifrado (por defecto None).

### Resultado

Cadena en formato hexadecimal (en mayúsculas) que representa los datos cifrados.

### Ejemplo

```csharp
byte[] key = new byte[24];
byte[] iv = new byte[8];
byte[] data = System.Text.Encoding.UTF8.GetBytes("Mensaje secreto");

using var hexCipher = X3Des.Encrypt(data, key, iv);
string encryptedHex = hexCipher.ToString();
```

---

▶ `Decrypt(ReadOnlySpan<char>, byte[], byte[], CipherMode, PaddingMode)`

Descifra una cadena en formato hexadecimal que representa datos cifrados con 3-DES.

### Parámetros

* data: Texto hexadecimal que contiene los datos cifrados.
* key: Clave simétrica utilizada para el descifrado.
* iv: Vector de inicialización (opcional, depende del modo).
* mode: Modo de operación del descifrado (por defecto ECB).
* padding: Tipo de relleno usado en el descifrado (por defecto None).

### Resultado

Arreglo de bytes con los datos descifrados en su forma original.

### Ejemplo

```csharp
byte[] key = new byte[24];
byte[] iv = new byte[8];
string encryptedHex = "A1B2C3D4E5F6";

byte[] decrypted = X3Des.Decrypt(encryptedHex, key, iv);
string plainText = System.Text.Encoding.UTF8.GetString(decrypted);
```