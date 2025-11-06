# LawnStringsServer

Permite acceder al servidor de LawnStrings usado por *Plants vs. Zombies 2 China* (desde la versión **v3.3.5** en adelante).

## Métodos

▶ `DownloadFileAsync(string, LawnStringsResType, LawnStringsServerType)`

Descarga archivos LawnStrings desde el servidor remoto según el tipo de recurso solicitado.

### Parámetros

* baseDir: ruta base donde se guardarán los archivos descargados.
* res: tipo de recurso a descargar (`Md5`, `Res`, `All`).
* serverType: tipo de servidor.

### Ejemplo

```csharp
await LawnStringsServer.DownloadFileAsync(
    @"C:\PvZ2Data",
    LawnStringsResType.All,
    LawnStringsServerType.Release);
```

---

▶ `GetUpdate(Stream, Stream, LawnStringsServerType, HashSet<string>)`

Obtiene las nuevas cadenas agregadas en el servidor remoto comparando un archivo local con la versión más reciente.

### Parámetros

* target: flujo local que contiene las cadenas actuales.
* diff: flujo donde se escribirán las nuevas cadenas encontradas.
* serverType: servidor desde el cual obtener los datos actualizados.
* excludeList: conjunto opcional de claves que deben omitirse.

### Ejemplo

```csharp
using var local = File.OpenRead("local_strings.txt");
using var diff = File.Create("update_diff.txt");

await LawnStringsServer.GetUpdate(local, diff, LawnStringsServerType.Release);
```

---

▶ `GetUpdate(string, LawnStringsServerType, HashSet<string>)`

Compara automáticamente un archivo LawnStrings local con la versión en el servidor.

### Parámetros

* inputPath: ruta del archivo local a comparar.
* serverType: tipo de servidor remoto (`Release` o `Shipping`).
* excludeList: lista opcional de claves a excluir de la comparación.

### Ejemplo

```csharp
await LawnStringsServer.GetUpdate(
    @"C:\PvZ2Data\local_strings.txt",
    LawnStringsServerType.Shipping);
```