# SexyObj

Representa un objeto genérico en el SexyFramework.

## Propiedades

▶ `Comment`

Comentario asociado al objeto.

▶ `Aliases`

Lista de alias del objeto o `null` si no tiene.

▶ `ObjClass`

Nombre de la clase del objeto.

▶ `ObjData`

Instancia del objeto, almacenada como `ExpandoObject`.

## Constructores

▶ `SexyObj()`

Inicializa un objeto con la clase por defecto `"MyClassTemplate"`.

### Ejemplo

```csharp
var obj = new SexyObj();
```

--------------------------------------------------------

▶ `SexyObj(string, List<string>, string)`

Inicializa un objeto con comentario, alias y nombre de clase.

### Parámetros

- `comment`: comentario del objeto
- `aliases`: lista de alias
- `objClass`: nombre de la clase del objeto

### Ejemplo

```csharp
var obj = new SexyObj("Comentario", new List<string>{"alias1"}, "MiClase");
```

--------------------------------------------------------

▶ `SexyObj(string, List<string>, string, ExpandoObject)`

Inicializa un objeto con comentario, alias, clase y datos de instancia.

### Parámetros

- `comment`: comentario del objeto
- `aliases`: lista de alias
- `objClass`: nombre de la clase del objeto
- `objData`: datos del objeto como `ExpandoObject`

### Ejemplo

```csharp
var data = new ExpandoObject();
var obj = new SexyObj("Comentario", new List<string>{"alias1"}, "MiClase", data);
```

## Métodos

▶ `Read(string)`

Lee un objeto desde un archivo JSON y lo devuelve como instancia de `SexyObj`.

### Parámetros

- `sourcePath`: ruta del archivo JSON

### Resultado

- `SexyObj`: objeto leído o `null` si el archivo no existe o está vacío

### Ejemplo

```csharp
var obj = SexyObj.Read("C:\\ruta\\obj.json");
```

*********************************************

# SexyObj<T>

Representa un objeto tipado en el SexyFramework.

## Propiedades

▶ `Comment`

Comentario asociado al objeto.

▶ `Aliases`

Lista de alias del objeto.

▶ `ObjClass`

Nombre de la clase del objeto.

▶ `ObjData`

Instancia del objeto tipada como `T`.