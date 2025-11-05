# TWSecurity

Permite **cifrar y descifrar archivos y datos del sistema TalkWeb** utilizados en *PvZ 2 China*, aplicando **cifrado DES (modo CBC)** combinado con codificación **Hexadecimal**. 

## Notas

- Se usa para cifrar el archivo `assets/twpay.xml` del APK
- Las solicitudes de pagos y login también lo usan para proteger los datos.

## Métodos

▶ `IsValidHex(ReadOnlySpan<char>)`

Verifica si una cadena contiene un formato hexadecimal válido compatible con los datos cifrados.

### Parámetros

* str: cadena a comprobar.

### Resultado

Verdadero si la cadena es hexadecimal válida, de lo contrario falso.

### Ejemplo

```csharp
bool valid = TWSecurity.IsValidHex("1A2B3C4D");
```

---

▶ `EncryptStream(Stream, Stream)`

Cifra un flujo de datos con **DES + Hex**, escribiendo el resultado directamente en el flujo de salida.

### Parámetros

* input: flujo de entrada con los datos a cifrar.
* output: flujo de salida donde se escribirá el contenido cifrado.

### Ejemplo

```csharp
using FileStream input = File.OpenRead("twpay.xml");
using FileStream output = File.Create("twpay_encrypted.xml");
TWSecurity.EncryptStream(input, output);
```

---

▶ `EncryptFile(string, string)`

Cifra un archivo de configuración TalkWeb mediante **DES**, generando una versión codificada en **Hexadecimal**.

### Parámetros

* inputPath: ruta del archivo a cifrar.
* outputPath: ubicación donde se guardará el archivo cifrado.

### Ejemplo

```csharp
TWSecurity.EncryptFile("assets/twpay.xml", "assets/twpay_enc.xml");
```

---

▶ `DecryptStream(Stream, Stream)`

Descifra un flujo cifrado con **DES + Hex**, restaurando los datos originales al flujo de salida.

### Parámetros

* input: flujo de entrada con los datos cifrados.
* output: flujo donde se escribirán los datos descifrados.

### Ejemplo

```csharp
using FileStream input = File.OpenRead("twpay_enc.xml");
using FileStream output = File.Create("twpay_dec.xml");
TWSecurity.DecryptStream(input, output);
```

---

▶ `DecryptFile(string, string)`

Descifra un archivo TalkWeb previamente cifrado con **DES**, devolviendo la versión original del contenido.

### Parámetros

* inputPath: ruta del archivo cifrado.
* outputPath: ubicación donde se guardará el archivo descifrado.

### Ejemplo

```csharp
TWSecurity.DecryptFile("assets/twpay_enc.xml", "assets/twpay.xml");
```

---

▶ `CipherData(ReadOnlySpan<char>, bool)`

Cifra o descifra directamente una cadena de texto mediante **DES** y codificación **Hex**, según el modo indicado.

### Parámetros

* data: cadena de texto a procesar.
* forEncryption: modo de operación (true = cifrar, false = descifrar).

### Resultado

Cadena resultante del proceso (cifrada o descifrada).

### Ejemplo

```csharp
string enc = TWSecurity.CipherData("Account=User123", true);
string dec = TWSecurity.CipherData(enc, false);
```