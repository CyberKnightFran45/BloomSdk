# GenericDigest

Maneja cálculos de **hash y HMAC** para bytes y streams usando los proveedores de .NET

## Métodos

▶ `ComputeBytes(ReadOnlySpan<byte>, ReadOnlySpan<char>)`

Calcula el hash de un arreglo de bytes usando un proveedor específico.

### Parámetros

* input: bytes a procesar.
* providerName: nombre del proveedor (`SHA1`, `SHA256`, etc.).

### Resultado

* Retorna un `NativeMemoryOwner<byte>` con el digest.

### Ejemplo

```csharp
byte[] data = System.Text.Encoding.UTF8.GetBytes("Hola Mundo");
var hash = GenericDigest.ComputeBytes(data, "SHA256");
```

---

▶ `GetString(ReadOnlySpan<byte>, ReadOnlySpan<char>, StringCase)`

Obtiene el hash en forma de cadena hexadecimal.

### Parámetros

* input: bytes a procesar.
* providerName: proveedor de hash.
* strCase: opcional, define mayúsculas o minúsculas.

### Resultado

* Retorna un `NativeMemoryOwner<char>` con el hash en texto.

---

▶ `ComputeBytes(Stream, ReadOnlySpan<char>)`

Calcula el hash de un stream.

### Resultado

* Retorna un arreglo de bytes con el digest.

---

▶ `GetString(Stream, ReadOnlySpan<char>, StringCase)`

Obtiene el hash de un stream en forma de cadena hexadecimal.

---

▶ `ComputeBytes(ReadOnlySpan<byte>, ReadOnlySpan<char>, byte[])`

Calcula HMAC sobre bytes usando un auth code.

### Parámetros

* input: bytes a procesar.
* providerName: proveedor de hash.
* authCode: clave para HMAC.

### Resultado

* Retorna `NativeMemoryOwner<byte>` con el digest HMAC.

---

▶ `GetString(ReadOnlySpan<byte>, ReadOnlySpan<char>, byte[], StringCase)`

Obtiene el HMAC en forma de cadena hexadecimal.

---

▶ `ComputeBytes(Stream, ReadOnlySpan<char>, byte[])`

Calcula HMAC sobre un stream.

---

▶ `GetString(Stream, ReadOnlySpan<char>, byte[], StringCase)`

Obtiene el HMAC de un stream en texto hexadecimal.