# NativeJsonReader

Permite leer flujos JSON en bloques (`Stream`) para mejorar el rendimiento en archivos grandes, soportando lectura token a token sin cargar todo en memoria.

## Propiedades

▶ `CurrentTokenType`

Tipo del token actualmente leído (`JsonTokenType`).

▶ `CurrentPropertyName`

Nombre de la propiedad JSON actual si el token es `PropertyName`.

▶ `IsJsonEnd`

Indica si se alcanzó el final del objeto raíz JSON.

▶ `IsInsideObject`

Indica si el lector está dentro de un objeto JSON.

▶ `IsInsideArray`

Indica si el lector está dentro de un array JSON.

## Constructores

▶ `NativeJsonReader(Stream, bool)`

Inicializa un lector de JSON sobre un stream.

### Parámetros

* `baseStream`: stream de origen
* `leaveOpen`: indica si se deja el stream abierto al cerrar el lector (por defecto `false`)

### Ejemplo

```csharp
using var stream = FileManager.OpenRead("data.json");
var reader = new NativeJsonReader(stream, true);
```

## Métodos

▶ `ReadToken()`

Lee el siguiente token JSON y actualiza el estado interno.

### Resultado

* `bool`: `true` si se leyó un token, `false` si se alcanzó el final

### Ejemplo

```csharp
while(reader.ReadToken())
{
    Console.WriteLine(reader.CurrentTokenType);
}
```

---

▶ `GetString()`

Obtiene el valor actual como `string`.

### Resultado

* `string`: valor de token actual

### Ejemplo

```csharp
string val = reader.GetString();
```

---

▶ `GetInt32()`

Convierte el token actual en `int`.

### Resultado

* `int`: valor numérico

### Ejemplo

```csharp
int num = reader.GetInt32();
```

---

▶ `GetInt64()`

Convierte el token actual en `long`.

### Resultado

* `long`: valor numérico

### Ejemplo

```csharp
long num = reader.GetInt64();
```

---

▶ `GetDouble()`

Convierte el token actual en `double`.

### Resultado

* `double`: valor numérico

### Ejemplo

```csharp
double num = reader.GetDouble();
```

---

▶ `GetBoolean()`

Convierte el token actual en `bool`.

### Resultado

* `bool`: valor booleano

### Ejemplo

```csharp
bool flag = reader.GetBoolean();
```

---

▶ `IsNull()`

Verifica si el token actual es `null`.

### Resultado

* `bool`: `true` si el token es nulo

### Ejemplo

```csharp
bool isNull = reader.IsNull();
```

---

▶ `CountArrayElements()`

Cuenta los elementos de un array JSON sin mover el estado real del lector.

### Resultado

* `int`: cantidad de elementos en el array

### Ejemplo

```csharp
int count = reader.CountArrayElements();
```

---

▶ `ValidateStructure()`

Verifica que la estructura JSON esté balanceada (sin objetos o arrays abiertos).

### Ejemplo

```csharp
reader.ValidateStructure();
```

---

▶ `Open(string)`

Abre un archivo JSON desde disco y retorna un lector.

### Parámetros

* `path`: ruta al archivo JSON

### Resultado

* `NativeJsonReader`: lector inicializado

### Ejemplo

```csharp
using var reader = NativeJsonReader.Open("data.json");
```