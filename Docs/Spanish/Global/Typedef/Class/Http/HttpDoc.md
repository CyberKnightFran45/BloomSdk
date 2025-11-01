# HttpDoc<T>

Representa una clase que puede serializarse como un documento HTTP multipart.

## Campos

▶ `_httpMap`

Diccionario que asocia los nombres de los campos HTTP con su índice interno.

## Métodos

▶ `RegisterField(string, int)`

Registra un campo HTTP con su índice correspondiente.

### Parámetros

- `name`: nombre del campo HTTP.
- `index`: índice asociado al campo.

### Ejemplo

```csharp
RegisterField("username", 0);
```

--------------------------------------------------------

▶ `SetupFields()`

Configura los campos HTTP que se usarán para la serialización.
(Debe ser implementado por las clases derivadas.)

### Ejemplo

```csharp
SetupFields();
```

--------------------------------------------------------

▶ `Read(MatchCollection)`

Lee los valores de los campos desde una colección de coincidencias de expresiones regulares.

### Parámetros

- `fields`: colección de coincidencias que contiene los nombres y valores de los campos.

### Ejemplo

```csharp
Read(regex.Matches(inputString));
```

--------------------------------------------------------

▶ `ReadForm(Stream)`

Lee los datos del formulario desde un flujo de entrada.
(Debe ser implementado por las clases derivadas.)

### Parámetros

- `reader`: flujo desde el cual se leen los datos.

### Ejemplo

```csharp
ReadForm(request.Body);
```

--------------------------------------------------------

▶ `Write(Stream, Action<Stream, string, object, bool>)`

Escribe los campos en un flujo utilizando una función personalizada.

### Parámetros

- `writer`: flujo de salida donde se escribirán los datos.
- `writeFunc`: delegado que maneja la lógica de escritura de cada campo.

### Ejemplo

```csharp
Write(stream, (w, n, v, f) => WritePart(w, n, v, f));
```

--------------------------------------------------------

▶ `WriteForm(Stream)`

Escribe el formulario HTTP en un flujo.
(Debe ser implementado por las clases derivadas.)

### Parámetros

- `writer`: flujo de salida.

### Ejemplo

```csharp
WriteForm(responseStream);
```

--------------------------------------------------------

▶ `Init()`

Inicializa el documento, los campos y la configuración interna.

### Ejemplo

```csharp
Init();
```