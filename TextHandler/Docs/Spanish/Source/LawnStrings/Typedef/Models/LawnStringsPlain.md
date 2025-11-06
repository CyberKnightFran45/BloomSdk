# LawnStringsPlain

Procesa LawnStrings en formato **PlainText** utilizado en PvZ Free y PvZ 2 (v1.0.0 - v6.7.1)

## Métodos

▶ `ReadList(Stream, List<string>, LawnStringsEncoding)`

Lee líneas desde un `Stream` y las agrega a una lista, manejando encabezados de secciones y codificación.

### Parámetros

* reader: Stream de entrada.
* lst: lista donde se almacenarán las líneas.
* encodeFlags: codificación utilizada.

### Ejemplo

```csharp
List<string> lines = new();
using FileStream fs = File.OpenRead("file.txt");
LawnStringsPlain.ReadList(fs, lines, LawnStringsEncoding.UTF8);
```

---

▶ `ReadDict(Stream, Dictionary<string, string>, LawnStringsEncoding)`

Lee líneas desde un `Stream` y las agrega a un diccionario, usando encabezados como claves y el contenido como valores.

### Parámetros

* reader: Stream de entrada.
* dict: diccionario donde se almacenarán las claves y valores.
* encodeFlags: codificación utilizada.

### Ejemplo

```csharp
Dictionary<string, string> dict = new();
using FileStream fs = File.OpenRead("file.txt");
LawnStringsPlain.ReadDict(fs, dict, LawnStringsEncoding.UTF8);
```

---

▶ `WriteList(Stream, List<string>, LawnStringsEncoding)`

Escribe una lista de strings en un `Stream`, insertando encabezados de sección según corresponda.

### Parámetros

* writer: Stream de salida.
* lst: lista de strings a escribir.
* encodeFlags: codificación utilizada.

### Ejemplo

```csharp
using FileStream fs = File.Create("output.txt");
LawnStringsPlain.WriteList(fs, lines, LawnStringsEncoding.UTF8);
```

---

▶ `WriteDict(Stream, Dictionary<string, string>, LawnStringsEncoding)`

Escribe un diccionario en un `Stream`, usando las claves como encabezados y los valores como contenido.

### Parámetros

* writer: Stream de salida.
* dict: Diccionario de strings a escribir.
* encodeFlags: Codificación utilizada.

### Ejemplo

```csharp
using FileStream fs = File.Create("output.txt");
LawnStringsPlain.WriteDict(fs, dict, LawnStringsEncoding.UTF8);
```

---

▶ `WriteKvp(Stream, IEnumerable<KeyValuePair<string, string>>, LawnStringsEncoding)`

Escribe una colección de pares clave-valor en un `Stream`, usando las claves como encabezados.

### Parámetros

* writer: Stream de salida.
* map: colección de pares clave-valor.
* encodeFlags: codificación utilizada.

### Ejemplo

```csharp
using FileStream fs = File.Create("output.txt");
var map = dict.ToList();
LawnStringsPlain.WriteKvp(fs, map, LawnStringsEncoding.UTF8);
```

---

▶ `Sort(Stream, Stream, LawnStringsEncoding)`

Ordena el contenido de un texto plano alfabéticamente por clave y lo escribe en un `Stream` de salida.

### Parámetros

* input: Stream de entrada.
* output: Stream de salida ordenado.
* encodeFlags: codificación utilizada.

### Ejemplo

```csharp
using FileStream input = File.OpenRead("unsorted.txt");
using FileStream output = File.Create("sorted.txt");
LawnStringsPlain.Sort(input, output, LawnStringsEncoding.UTF8);
```

---

▶ `FindAdded(Stream, Stream, LawnStringsEncoding, HashSet<string>)`

Obtiene los pares clave-valor que existen en el segundo `Stream` pero no en el primero, excluyendo claves opcionales.

### Parámetros

* a: Stream de la primera versión.
* b: Stream de la segunda versión.
* encodeFlags: codificación utilizada.
* excludeList: lista de claves a excluir (opcional).

### Resultado

Colección de pares clave-valor que se añadieron en `b` respecto a `a`.

### Ejemplo

```csharp
var added = LawnStringsPlain.FindAdded(fs1, fs2, LawnStringsEncoding.UTF8);
```

---

▶ `FindChanged(Stream, Stream, LawnStringsEncoding, HashSet<string>)`

Obtiene los pares clave-valor cuyo valor cambió entre dos `Stream`, excluyendo claves opcionales.

### Parámetros

* a: Stream de la primera versión.
* b: Stream de la segunda versión.
* encodeFlags: codificación utilizada.
* excludeList: lista de claves a excluir (opcional).

### Resultado

Colección de pares clave-valor que cambiaron en `b` respecto a `a`.

### Ejemplo

```csharp
var changed = LawnStringsPlain.FindChanged(fs1, fs2, LawnStringsEncoding.UTF8);
```

---

▶ `FindFullDiff(Stream, Stream, LawnStringsEncoding, HashSet<string>)`

Obtiene los pares clave-valor que fueron añadidos o cambiados entre dos `Stream`, excluyendo claves opcionales.

### Parámetros

* a: Stream de la primera versión.
* b: Stream de la segunda versión.
* encodeFlags: codificación utilizada.
* excludeList: lista de claves a excluir (opcional).

### Resultado

Colección de pares clave-valor que se añadieron o cambiaron en `b` respecto a `a`.

### Ejemplo

```csharp
var diff = LawnStringsPlain.FindFullDiff(fs1, fs2, LawnStringsEncoding.UTF8);
```