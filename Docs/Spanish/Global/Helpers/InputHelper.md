# InputHelper

Ofrece herramientas comunes para el procesamiento seguro y controlado de datos introducidos por el usuario.

## Métodos

▶ `ApplyStringCase(Span<char>, StringCase)`

Aplica el tipo de capitalización indicado a una secuencia de caracteres mutable.

### Parámetros

* target: secuencia de caracteres a modificar.
* strCase: tipo de capitalización a aplicar (mayúsculas o minúsculas).

### Ejemplo

```csharp
Span<char> texto = "Hola".ToCharArray();
InputHelper.ApplyStringCase(texto, StringCase.Upper);
// texto ahora contiene "HOLA"
```

---

▶ `ConvertHexBytes(ReadOnlySpan<char>, char)`

Convierte una cadena hexadecimal en una secuencia de bytes.

### Parámetros

* hexStr: cadena hexadecimal a convertir.
* separator: carácter separador entre bytes (por defecto `' '`).

### Resultado

Instancia de `NativeMemoryOwner<byte>` con los bytes convertidos.

### Ejemplo

```csharp
var bytes = InputHelper.ConvertHexBytes("4A 6F 79", ' ');
```

---

▶ `ConvertHexString(ReadOnlySpan<byte>, StringCase, char)`

Convierte una secuencia de bytes en una cadena hexadecimal.

### Parámetros

* input: bytes a convertir.
* strCase: tipo de capitalización (mayúsculas o minúsculas).
* separator: carácter separador entre bytes (opcional, por defecto `'\0'`).

### Resultado

Instancia de `NativeMemoryOwner<char>` que contiene la cadena hexadecimal.

### Ejemplo

```csharp
var hex = InputHelper.ConvertHexString(stackalloc byte[] { 0x4A, 0x6F, 0x79 }, StringCase.Upper);
```

---

▶ `FilterDateTime(ReadOnlySpan<char>)`

Filtra un valor de tipo `DateTime` a partir de una cadena de entrada.

### Parámetros

* input: texto que representa una fecha.

### Resultado

Valor `DateTime` válido o la fecha actual si no se puede analizar.

### Ejemplo

```csharp
var fecha = InputHelper.FilterDateTime("2025-10-30");
```

---

▶ `FilterName(ReadOnlySpan<char>)`

Filtra un nombre eliminando caracteres inválidos del texto de entrada.

### Parámetros

* source: texto a filtrar.

### Resultado

Instancia de `NativeMemoryOwner<char>` que contiene el nombre filtrado.

### Ejemplo

```csharp
var nombre = InputHelper.FilterName("Andr3s$#");
```

---

▶ `FilterNumber<T>(ReadOnlySpan<char>)`

Filtra y convierte una entrada textual en un valor numérico del tipo especificado.

### Parámetros

* input: texto a analizar.

### Resultado

Valor numérico de tipo `T` dentro del rango válido.

### Ejemplo

```csharp
int numero = InputHelper.FilterNumber<int>("Edad: 35 años");
```

---

▶ `GenRandomStr(int)`

Genera una cadena aleatoria alfanumérica de longitud determinada.

### Parámetros

* length: longitud de la cadena.

### Resultado

Cadena aleatoria generada.

### Ejemplo

```csharp
string aleatoria = InputHelper.GenRandomStr(10);
```

---

▶ `GetInvalidChars(bool)`

Obtiene los caracteres inválidos según el tipo de nombre de archivo o ruta.

### Parámetros

* isShortName: indica si se trata de un nombre corto (archivo o carpeta) o una ruta completa.

### Resultado

Arreglo de caracteres inválidos.

### Ejemplo

```csharp
char[] invalidos = InputHelper.GetInvalidChars(true);
```

---

▶ `GetString(ReadOnlySpan<byte>, Encoding)`

Convierte una secuencia de bytes en una cadena utilizando la codificación indicada.

### Parámetros

* bytes: bytes a convertir.
* encoding: codificación (por defecto UTF-8).

### Resultado

Cadena representada por los bytes.

### Ejemplo

```csharp
string texto = InputHelper.GetString(stackalloc byte[] { 72, 105 });
```

---

▶ `GetNativeString(ReadOnlySpan<byte>, Encoding)`

Convierte bytes en una cadena nativa gestionada mediante memoria no administrada.

### Parámetros

* bytes: bytes a convertir.
* encoding: codificación (por defecto UTF-8).

### Resultado

Instancia de `NativeString` con la cadena convertida.

### Ejemplo

```csharp
var nativa = InputHelper.GetNativeString(stackalloc byte[] { 72, 105 });
```

---

▶ `GetBytes(ReadOnlySpan<char>, Encoding)`

Convierte una secuencia de caracteres en bytes según la codificación especificada.

### Parámetros

* str: caracteres a convertir.
* encoding: codificación (por defecto UTF-8).

### Resultado

Arreglo de bytes resultante.

### Ejemplo

```csharp
byte[] bytes = InputHelper.GetBytes("Hola");
```

---

▶ `GetNativeBytes(ReadOnlySpan<char>, Encoding)`

Convierte caracteres en una estructura de memoria nativa de bytes.

### Parámetros

* str: caracteres a convertir.
* encoding: codificación (por defecto UTF-8).

### Resultado

Instancia de `NativeMemoryOwner<byte>` que contiene los bytes resultantes.

### Ejemplo

```csharp
var memBytes = InputHelper.GetNativeBytes("Hola");
```

---

▶ `RemoveLiteralChars(ref string)`

Elimina caracteres literales como `\r`, `\n` o `\t` de una cadena.

### Parámetros

* targetStr: referencia a la cadena a limpiar.

### Ejemplo

```csharp
string texto = "Hola\\r\\nMundo";
InputHelper.RemoveLiteralChars(ref texto);
```