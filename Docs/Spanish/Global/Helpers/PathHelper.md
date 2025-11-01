# PathHelper

Proporciona funciones auxiliares para construir, filtrar y modificar rutas de acceso en el sistema de archivos.

## Métodos

▶ `AddExtension(ref string, string)`

Agrega una extensión al final de una ruta, evitando duplicados.

### Parámetros

* sourcePath: referencia a la ruta que será modificada.
* ext: extensión que se agregará.

### Ejemplo

```csharp
string ruta = "C:\\temp\\archivo";
PathHelper.AddExtension(ref ruta, ".txt");
```

---

▶ `AlignPathWithAppDir(ref string)`

Alinea una ruta relativa con el directorio base de la aplicación.

### Parámetros

* targetPath: referencia a la ruta que será alineada.

### Ejemplo

```csharp
string ruta = "data\\archivo.txt";
PathHelper.AlignPathWithAppDir(ref ruta);
```

---

▶ `BuildPathFromDir(string, string, string, string)`

Construye una nueva ruta de archivo a partir de un directorio base y parámetros de nombre, extensión y sufijo opcional.

### Parámetros

* parentPath: ruta base donde se creará el nuevo archivo.
* filePath: nombre o ruta de referencia.
* ext: extensión que se agregará.
* suffix: sufijo opcional a añadir al nombre.

### Resultado

Ruta completa construida.

### Ejemplo

```csharp
string ruta = PathHelper.BuildPathFromDir("C:\\Logs", "app.log", ".bak", "old");
```

---

▶ `ChangeExtension(ref string, string)`

Cambia la extensión de una ruta de archivo.

### Parámetros

* sourcePath: referencia a la ruta a modificar.
* ext: nueva extensión.

### Ejemplo

```csharp
string archivo = "data.txt";
PathHelper.ChangeExtension(ref archivo, ".csv");
```

---

▶ `CheckDuplicatedPath(ref string)`

Verifica si una ruta ya existe en el sistema de archivos y genera una nueva variante con índice incremental en caso de duplicado.

### Parámetros

* targetPath: referencia a la ruta que será validada.

### Ejemplo

```csharp
string archivo = "C:\\temp\\reporte.txt";
PathHelper.CheckDuplicatedPath(ref archivo);
```

---

▶ `EnsurePathExists(string, bool?)`

Verifica que una ruta exista y, si no, crea el archivo o directorio correspondiente.

### Parámetros

* sourcePath: ruta a verificar.
* forFiles: indica si la ruta representa un archivo (`true`), un directorio (`false`) o se detecta automáticamente (`null`).

### Ejemplo

```csharp
PathHelper.EnsurePathExists("C:\\data", false);
```

---

▶ `CreateFileSystem(string, bool?)`

Crea un archivo o carpeta según el tipo de ruta indicado.

### Parámetros

* targetPath: ruta donde se creará el sistema de archivos.
* isFile: determina si se creará un archivo (`true`) o una carpeta (`false`).

### Ejemplo

```csharp
PathHelper.CreateFileSystem("C:\\temp\\nuevo.txt", true);
```

---

▶ `DeleteEndPathSeparator(ref string)`

Elimina el separador final (`/` o `\`) de una ruta, si existe.

### Parámetros

* str: referencia a la ruta a limpiar.

### Ejemplo

```csharp
string ruta = "C:\\temp\\";
PathHelper.DeleteEndPathSeparator(ref ruta);
```

---

▶ `FilterFiles(ref IEnumerable<string>, HashSet<string>, HashSet<string>, HashSet<string>, HashSet<string>)`

Filtra una lista de archivos por nombre y extensión, permitiendo listas de inclusión y exclusión.

### Parámetros

* sourceFiles: referencia a la lista de archivos a filtrar.
* names: nombres permitidos.
* extensions: extensiones permitidas.
* namesToExclude: nombres a excluir (opcional).
* extToExclude: extensiones a excluir (opcional).

### Ejemplo

```csharp
var archivos = Directory.EnumerateFiles("C:\\temp");
PathHelper.FilterFiles(ref archivos, new() { "log" }, new() { ".txt" });
```

---

▶ `FilterDirs(ref IEnumerable<string>, HashSet<string>, int, HashSet<string>)`

Filtra una lista de carpetas por nombre y longitud de contenido.

### Parámetros

* sourceDirs: referencia a la lista de directorios a filtrar.
* names: nombres permitidos.
* maxLength: cantidad máxima de elementos dentro del directorio.
* namesToExclude: nombres a excluir (opcional).

### Ejemplo

```csharp
var carpetas = Directory.EnumerateDirectories("C:\\");
PathHelper.FilterDirs(ref carpetas, new() { "Temp" }, 5);
```

---

▶ `FilterPath(ref string)`

Filtra una ruta de entrada, eliminando caracteres inválidos del sistema operativo.

### Parámetros

* targetPath: referencia a la ruta a filtrar.

### Ejemplo

```csharp
string ruta = "C:\\Inv?alid|Path";
PathHelper.FilterPath(ref ruta);
```

---

▶ `NormalizePath(ref string)`

Normaliza los separadores de una ruta según el sistema operativo actual.

### Parámetros

* targetPath: referencia a la ruta a normalizar.

### Ejemplo

```csharp
string ruta = "C:/data\\test";
PathHelper.NormalizePath(ref ruta);
```

---

▶ `DenormalizePath(ref string)`

Reemplaza todos los separadores de una ruta por `/`.

### Parámetros

* targetPath: referencia a la ruta a denormalizar.

### Ejemplo

```csharp
string ruta = "C:\\data\\test";
PathHelper.DenormalizePath(ref ruta);
```

---

▶ `SafeCombine(params string[])`

Combina rutas de forma segura y compatible entre sistemas operativos.

### Parámetros

* paths: secuencia de rutas a combinar.

### Resultado

Ruta combinada y normalizada.

### Ejemplo

```csharp
string ruta = PathHelper.SafeCombine("C:\\Users", "Public", "Docs");
```

---

▶ `GetDownloadsFolder()`

Obtiene la ruta completa a la carpeta de descargas del usuario actual.

### Resultado

Ruta a la carpeta “Downloads”.

### Ejemplo

```csharp
string descargas = PathHelper.GetDownloadsFolder();
```

---

▶ `RemoveExtension(ref string)`

Elimina la extensión de una ruta de archivo existente.

### Parámetros

* sourcePath: referencia a la ruta a modificar.

### Ejemplo

```csharp
string ruta = "C:\\temp\\archivo.txt";
PathHelper.RemoveExtension(ref ruta);
```