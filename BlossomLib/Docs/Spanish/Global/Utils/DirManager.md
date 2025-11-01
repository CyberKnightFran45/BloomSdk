# DirManager

Esta clase provee funciones adicionales para **gestión de directorios**, incluyendo obtención de rutas, nombres y tamaños de carpetas.

## Métodos

---

▶ `GetContainerPath(string, string)`

Obtiene la ruta de un contenedor dentro de un directorio, creando la estructura si no existe.

### Parámetros

* `targetPath`: Ruta donde se creará o buscará el contenedor.
* `namePrefix`: Prefijo para agregar al inicio del nombre del contenedor. Valor por defecto: `"FilesContainer"`.

### Resultado

Un `string` con la ruta completa del contenedor.

### Ejemplo

```csharp
string container = DirManager.GetContainerPath("C:\\Proyectos\\MiArchivo.txt");
// "C:\Proyectos\FilesContainer\MiArchivo"
```

---

▶ `GetFolderName(string)`

Obtiene el nombre de un directorio a partir de su ruta.

### Parámetros

* `targetPath`: Ruta del directorio cuyo nombre se quiere obtener.

### Resultado

Un `string` con el nombre de la carpeta.

### Ejemplo

```csharp
string folderName = DirManager.GetFolderName("C:\\Proyectos\\MiCarpeta");
// "MiCarpeta"
```

---

▶ `GetFolderSize(string)`

Calcula el tamaño total de un directorio incluyendo todos sus archivos y subdirectorios.

### Parámetros

* `targetPath`: Ruta del directorio a analizar.

### Resultado

Un `long` con el tamaño total en bytes de la carpeta.

### Ejemplo

```csharp
long size = DirManager.GetFolderSize("C:\\Proyectos\\MiCarpeta");
```

---

▶ `FolderIsEmpty(string)`

Verifica si un directorio está vacío analizando su contenido.

### Parámetros

* `targetPath`: Ruta del directorio a comprobar.

### Resultado

Un `bool`: `<b>true</b>` si la carpeta está vacía, `<b>false</b>` en caso contrario.

### Ejemplo

```csharp
bool isEmpty = DirManager.FolderIsEmpty("C:\\Proyectos\\MiCarpeta");
```