# ProcessHelper

Proporciona métodos para crear y ejecutar procesos del sistema, permitiendo configurar opciones como redirección de salida, uso de shell y visibilidad de la ventana.

## Métodos

---

▶ `CreateNew(string, string, bool, bool, bool)`

Crea un nuevo proceso con los parámetros especificados, pero no lo inicia.

### Parámetros

* fName: nombre o ruta del ejecutable.
* args: argumentos del proceso (opcional, por defecto `string.Empty`).
* redirectOutput: indica si se debe redirigir la salida estándar (opcional, por defecto `true`).
* useShell: indica si se debe usar el shell del sistema (opcional, por defecto `false`).
* noWindow: indica si el proceso se ejecuta sin ventana (opcional, por defecto `true`).

### Resultado

Instancia de `Process` configurada según los parámetros.

### Ejemplo

```csharp
var proc = ProcessHelper.CreateNew("notepad.exe");
```

---

▶ `StartNew(string, string, bool, bool, bool)`

Crea y arranca un nuevo proceso con los parámetros especificados.

### Parámetros

* fName: nombre o ruta del ejecutable.
* args: argumentos del proceso (opcional, por defecto `""`).
* redirectOutput: indica si se debe redirigir la salida estándar (opcional, por defecto `true`).
* useShell: indica si se debe usar el shell del sistema (opcional, por defecto `false`).
* noWindow: indica si el proceso se ejecuta sin ventana (opcional, por defecto `true`).

### Resultado

Instancia de `Process` ya iniciada.

### Ejemplo

```csharp
var proc = ProcessHelper.StartNew("cmd.exe", "/c echo Hola Mundo");
```