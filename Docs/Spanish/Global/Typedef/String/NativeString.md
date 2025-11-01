# NativeString

Encapsula un puntero a caracteres nativo (`char*`) y permite manipularlo como un string gestionado, útil para manejar texto en memoria nativa de manera segura y eficiente.

## Propiedades

▶ `Length`

Devuelve la longitud del buffer de caracteres.

## Constructores

▶ `NativeString(long)`

Inicializa un buffer con la longitud especificada.

### Parámetros

* `length`: longitud del buffer en caracteres.

### Ejemplo

```csharp
var ns = new NativeString(256);
```

---

▶ `NativeString(ulong)`

Inicializa un buffer con la longitud especificada (tipo `ulong`).

### Parámetros

* `length`: longitud del buffer en caracteres.

### Ejemplo

```csharp
var ns = new NativeString(1024UL);
```

## Métodos

▶ `AsSpan(ulong, int)`

Devuelve un `Span<char>` que representa el buffer o una sección del mismo.

### Parámetros

* `offset`: posición inicial dentro del buffer (por defecto `0`)
* `length`: número de caracteres a incluir (por defecto `-1` para todo el buffer)

### Resultado

* `Span<char>`: vista mutable del buffer.

### Ejemplo

```csharp
var span = ns.AsSpan(0, 100);
```

---

▶ `GetView(ulong, int)`

Devuelve un `ReadOnlySpan<char>` que representa el buffer o una sección del mismo.

### Parámetros

* `offset`: posición inicial dentro del buffer
* `length`: número de caracteres a incluir

### Resultado

* `ReadOnlySpan<char>`: vista de solo lectura del buffer.

### Ejemplo

```csharp
ReadOnlySpan<char> view = ns.GetView(0, 50);
```

---

▶ `Realloc(long)`

Redimensiona el buffer a una nueva longitud.

### Parámetros

* `n`: nueva longitud del buffer en caracteres

### Ejemplo

```csharp
ns.Realloc(512);
```

---

▶ `Substring(ulong, int)`

Obtiene un `string` a partir de una sección del buffer.

### Parámetros

* `offset`: posición inicial dentro del buffer
* `length`: número de caracteres a incluir

### Resultado

* `string`: cadena resultante

### Ejemplo

```csharp
string sub = ns.Substring(0, 10);
```

---

▶ `ToString()`

Convierte todo el buffer en un `string`.

### Resultado

* `string`: cadena que representa todo el buffer

### Ejemplo

```csharp
string full = ns.ToString();
```

---

▶ `Dispose()`

Libera los recursos del buffer.

### Ejemplo

```csharp
ns.Dispose();
```