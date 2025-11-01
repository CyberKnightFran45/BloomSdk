# HttpMultipartDoc<T>

Representa una clase que puede serializarse como un documento HTTP multipart, implementando la lectura y escritura de formularios.

## Métodos

▶ `ReadForm(Stream)`

Lee los datos del formulario desde un flujo de entrada y los procesa como campos HTTP multipart.

### Parámetros

- `reader`: flujo desde el cual se leen los datos

### Ejemplo

```csharp
ReadForm(request.Body);
```

--------------------------------------------------------

▶ `WriteForm(Stream)`

Escribe el formulario HTTP multipart en un flujo de salida, incluyendo los campos y el pie de formulario.

### Parámetros

- `writer`: flujo de salida

### Ejemplo

```csharp
WriteForm(responseStream);
```