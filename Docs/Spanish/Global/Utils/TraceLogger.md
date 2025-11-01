# TraceLogger

Proporciona funcionalidades para registrar eventos, errores y trazas en archivos de log, con soporte de temporización, encabezados y manejo de memoria nativa.

## Métodos

▶ `Enable()`

Habilita el logger para permitir la escritura de registros.

---

▶ `Disable()`

Deshabilita el logger, deteniendo la escritura de registros.

---

▶ `Init()`

Inicializa el sistema de registro y crea el buffer interno.
Si ya está inicializado, reinicia el temporizador y gestiona la separación de logs por fecha.

---

▶ `Write(ReadOnlySpan<char>)`

Escribe un mensaje en el buffer de log.

### Parámetros

* `msg`: Mensaje que se desea escribir.

---

▶ `WriteActionStart(ReadOnlySpan<char>)`

Marca el inicio de una acción y registra su encabezado con etiqueta `TRACE`.

### Parámetros

* `msg`: Descripción de la acción iniciada.

---

▶ `WriteActionEnd()`

Detiene el temporizador y escribe el tiempo transcurrido desde la última acción iniciada.

---

▶ `WriteLine()`

Inserta un salto de línea en el log.

---

▶ `WriteLine(ReadOnlySpan<char>)`

Escribe un mensaje seguido de un salto de línea.

### Parámetros

* `msg`: Mensaje a registrar.

---

▶ `WriteDebug(ReadOnlySpan<char>, bool)`

Escribe un mensaje con etiqueta `DEBUG`.

### Parámetros

* `msg`: Contenido del mensaje.
* `appendLine`: Indica si debe agregarse un salto de línea. Valor por defecto: `true`.

---

▶ `WriteInfo(ReadOnlySpan<char>, bool)`

Escribe un mensaje con etiqueta `INFO`.

### Parámetros

* `msg`: Contenido del mensaje.
* `appendLine`: Indica si debe agregarse un salto de línea. Valor por defecto: `true`.

---

▶ `WriteWarn(ReadOnlySpan<char>, bool)`

Escribe un mensaje con etiqueta `WARNING`.

### Parámetros

* `msg`: Contenido del mensaje.
* `appendLine`: Indica si debe agregarse un salto de línea. Valor por defecto: `true`.

---

▶ `WriteError(ReadOnlySpan<char>, bool)`

Escribe un mensaje con etiqueta `ERROR`.

### Parámetros

* `msg`: Contenido del mensaje.
* `appendLine`: Indica si debe agregarse un salto de línea. Valor por defecto: `true`.

---

▶ `WriteError(Exception, ReadOnlySpan<char>)`

Registra un error crítico con los detalles de una excepción.

### Parámetros

* `error`: Excepción capturada.
* `msg`: Mensaje opcional que describe el contexto del error.

---

▶ `SaveLogs(string)`

Guarda el contenido del buffer en un archivo de log ordenado por fecha y hora.

### Parámetros

* `outputDir`: Directorio donde se almacenará el archivo. Valor por defecto: `Logs` dentro del directorio actual de la librería.

---

▶ `Dispose()`

Libera la memoria nativa usada por el buffer y desactiva el logger.
