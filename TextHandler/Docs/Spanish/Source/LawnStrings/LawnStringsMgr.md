# LawnStringsMgr

Administra los archivos LawnStrings, permitiendo su conversión, ordenamiento y comparación entre diferentes formatos.

## Métodos

▶ `Convert(Stream, Stream, LawnStringsFormat, LawnStringsFormat,
                     LawnStringsEncoding, LawnStringsEncoding)`

Convierte un flujo LawnStrings de un formato a otro, aplicando la codificación de entrada y salida indicada.

### Parámetros

* input: flujo de entrada a convertir.
* output: flujo donde se escribirá el resultado.
* inFormat: formato de origen.
* outFormat: formato de salida deseado.
* plainEncodeIn: codificación usada en el texto plano de entrada (opcional).
* plainEncodeOut: codificación usada en el texto plano de salida (opcional).

### Ejemplo

```csharp
LawnStringsMgr.Convert(inStream, outStream,
                       LawnStringsFormat.JsonList,
                       LawnStringsFormat.RtonMap);
```

---

▶ `ConvertFile(string, LawnStringsFormat, LawnStringsFormat,
                            LawnStringsEncoding, LawnStringsEncoding)`

Convierte un archivo LawnStrings a otro formato.

### Parámetros

* inputPath: ruta del archivo de entrada.
* inFormat: formato de entrada.
* outFormat: formato de salida.
* plainEncodeIn: codificación de texto plano de entrada (opcional).
* plainEncodeOut: codificación de texto plano de salida (opcional).

### Ejemplo

```csharp
LawnStringsMgr.ConvertFile("dialog.txt", LawnStringsFormat.PlainText,
                            LawnStringsFormat.JsonMap);
```

---

▶ `Sort(Stream, Stream, LawnStringsFormat, LawnStringsEncoding)`

Ordena las entradas de un flujo LawnStrings según su formato, reescribiendo el resultado en orden alfabético o numérico.

### Parámetros

* input: flujo de entrada a ordenar.
* output: flujo de salida donde se escribirá el resultado.
* format: formato del archivo LawnStrings.
* plainEncode: codificación usada en el texto plano (opcional).

### Ejemplo

```csharp
LawnStringsMgr.Sort(inStream, outStream, LawnStringsFormat.JsonList);
```

---

▶ `SortFile(string, LawnStringsFormat, LawnStringsEncoding)`

Ordena las cadenas de un archivo LawnStrings.

### Parámetros

* inputPath: ruta del archivo a ordenar.
* format: formato del archivo LawnStrings.
* plainEncode: codificación usada si el formato es texto plano (opcional).

### Ejemplo

```csharp
LawnStringsMgr.SortFile("localization.json", LawnStringsFormat.JsonMap);
```

---

▶ `Compare(Stream, Stream, Stream, LawnStringsFormat,
                               LawnStringsCompareMode, HashSet<string>,
                                 LawnStringsEncoding)`

Compara dos flujos LawnStrings según el formato y modo de comparación especificado, generando un flujo con las diferencias encontradas.

### Parámetros

* a: flujo base (antiguo).
* b: flujo comparativo (nuevo).
* diff: flujo de salida donde se escribirán las diferencias.
* format: formato LawnStrings a comparar.
* compareMode: modo de comparación (Added, Changed o FullDiff).
* excludeList: lista opcional de claves a excluir.
* encodeFlags: codificación usada para archivos de texto plano (opcional).

### Ejemplo

```csharp
LawnStringsMgr.Compare(oldStream, newStream, diffStream,
                       LawnStringsFormat.JsonList,
		       LawnStringsCompareMode.Changed);
```

---

▶ `CompareFiles(string, string, LawnStringsFormat,
                                       LawnStringsCompareMode, HashSet<string>, 
                                LawnStringsEncoding)`

Compara dos archivos LawnStrings, detectando cambios, adiciones o diferencias completas, y genera un nuevo archivo con el resultado.

### Parámetros

* oldPath: ruta del archivo base (antiguo).
* newPath: ruta del archivo nuevo a comparar.
* format: formato LawnStrings de ambos archivos.
* compareMode: modo de comparación (Added, Changed o FullDiff).
* excludeList: lista opcional de claves que deben omitirse.
* plainEncode: codificación si el formato es texto plano (opcional).

### Ejemplo

```csharp
LawnStringsMgr.CompareFiles("old.json", "new.json",
                            LawnStringsFormat.JsonMap,
                            LawnStringsCompareMode.FullDiff);
```