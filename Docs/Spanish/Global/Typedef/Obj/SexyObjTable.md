# SexyObjTable

Representa una tabla de diferentes objetos dentro del SexyFramework.

## Propiedades

▶ `Comment`

Comentario JSON asociado a la tabla.

▶ `Version`

Versión de la tabla (por defecto es 1).

▶ `Objects`

Lista de objetos contenidos en la tabla.

## Constructores

▶ `SexyObjTable()`

Inicializa una nueva instancia de la tabla sin datos.

### Ejemplo

```csharp
var table = new SexyObjTable();
```

--------------------------------------------------------

▶ `SexyObjTable(List<SexyObj>)`

Inicializa una nueva instancia con una lista de objetos predefinida.

### Parámetros

- `objs`: lista de objetos que conformarán la tabla

### Ejemplo

```csharp
var table = new SexyObjTable(new List<SexyObj>{ obj1, obj2 });
```

--------------------------------------------------------

▶ `SexyObjTable(string, uint, List<SexyObj>)`

Inicializa una nueva instancia con comentario, versión y lista de objetos.

### Parámetros

- `comment`: comentario de la tabla
- `ver`: versión de la tabla
- `objs`: lista de objetos asociados

### Ejemplo

```csharp
var table = new SexyObjTable("Mi tabla", 2, new List<SexyObj>{ obj1, obj2 });
```

## Métodos

▶ `CheckForNullFields()`

Verifica si existen campos nulos y los inicializa en caso necesario.

### Ejemplo

```csharp
table.CheckForNullFields();
```

--------------------------------------------------------

▶ `CheckObjs()`

Ejecuta la verificación de campos nulos en la instancia actual.

### Ejemplo

```csharp
table.CheckObjs();
```

--------------------------------------------------------

▶ `Read(string)`

Lee un archivo JSON y genera una instancia de `SexyObjTable`.

### Parámetros

- `sourcePath`: ruta del archivo JSON que contiene la tabla

### Resultado

- `SexyObjTable`: objeto leído desde el archivo o `null` si no existe o está vacío

### Ejemplo

```csharp
var table = SexyObjTable.Read("C:\\ruta\\tabla.json");
```

******************************************************

# SexyObjTable<T>

Representa una tabla de objetos del mismo tipo dentro del SexyFramework.

## Propiedades

▶ `Comment`

Comentario JSON asociado a la tabla.

▶ `Version`

Versión de la tabla (por defecto es 1).

▶ `Objects`

Lista genérica de objetos del mismo tipo.

## Métodos

▶ `CheckObjs()`

Método abstracto que valida la integridad o nulidad de los objetos de la tabla.

### Ejemplo

```csharp
table.CheckObjs();
```
