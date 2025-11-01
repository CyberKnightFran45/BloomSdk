# HttpUrlDoc<T>

Representa una clase que puede serializarse como un documento HTTP codificado en URL.

## Métodos

▶ `ReadForm(Stream)`

Lee los datos del formulario desde un flujo de entrada y los procesa como campos HTTP codificados en URL.

### Parámetros

- `reader`: flujo desde el cual se leen los datos

### Ejemplo

```csharp
ReadForm(request.Body);
```

--------------------------------------------------------

▶ `WriteForm(Stream)`

Escribe el formulario HTTP codificado en URL en un flujo de salida, usando la función interna de escritura.

### Parámetros

- `writer`: flujo de salida

### Ejemplo

```csharp
WriteForm(responseStream);
```