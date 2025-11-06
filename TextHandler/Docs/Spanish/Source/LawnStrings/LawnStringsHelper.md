# LawnStringsHelper

Realiza tareas auxiliares para manejar colecciones de cadenas LawnStrings.

## Métodos

▶ `GetEncoding(LawnStringsEncoding)`

Obtiene la codificación adecuada según el tipo de `LawnStringsEncoding` especificado.

### Parámetros

* flags: tipo de codificación a utilizar.

### Resultado

Objeto `Encoding` correspondiente al tipo solicitado.

### Ejemplo

```csharp
var encoding = LawnStringsHelper.GetEncoding(LawnStringsEncoding.UTF16);
```

---

▶ `CleanLine(ReadOnlySpan<char>)`

Limpia una línea de texto reemplazando secuencias de escape (`\n`) por saltos de línea reales y eliminando retornos de carro innecesarios.

### Parámetros

* src: texto fuente que se desea limpiar.

### Resultado

Cadena limpia en una instancia de `NativeMemoryOwner<char>`.

### Ejemplo

```csharp
using var clean = LawnStringsHelper.CleanLine("Hello\\nWorld");
```

---

▶ `GetExtension(LawnStringsFormat)`

Obtiene la extensión de archivo asociada a un formato LawnStrings determinado.

### Parámetros

* sourceFormat: formato de origen del archivo LawnStrings.

### Resultado

Cadena con la extensión adecuada (por ejemplo, `.json`, `.rton` o `.txt`).

### Ejemplo

```csharp
string ext = LawnStringsHelper.GetExtension(LawnStringsFormat.JsonMap);
```

---

▶ `BuildPath(string, string, LawnStringsFormat)`

Construye una nueva ruta de archivo para LawnStrings aplicando un sufijo y el formato de salida indicado.

Si existe un archivo duplicado, el método ajusta el nombre automáticamente.

### Parámetros

* sourcePath: ruta original del archivo.
* suffix: sufijo a agregar al nombre.
* destFormat: formato de destino del archivo.

### Resultado

Ruta completa del nuevo archivo a generar.

### Ejemplo

```csharp
string newPath = LawnStringsHelper.BuildPath("data.txt", "converted", LawnStringsFormat.JsonList);
```

---

▶ `AlphanumCompare(ReadOnlySpan<char>, ReadOnlySpan<char>)`

Compara dos cadenas de forma alfanumérica, considerando secuencias numéricas como valores enteros en lugar de caracteres individuales.

### Parámetros

* x: primera cadena a comparar.
* y: segunda cadena a comparar.

### Resultado

Entero que indica el resultado de la comparación:

* `-1` si `x` es menor que `y`.
* `0` si son iguales.
* `1` si `x` es mayor que `y`.

### Ejemplo

```csharp
int cmp = LawnStringsHelper.AlphanumCompare("file2", "file10"); // -1
```