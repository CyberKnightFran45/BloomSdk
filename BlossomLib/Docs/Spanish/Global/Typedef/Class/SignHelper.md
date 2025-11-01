# SignHelper<T>

Helper usado para generar firmas a partir de los campos de clases heredadas.

## Campos

▶ `_targetFields`

Diccionario que mapea índices a funciones que devuelven strings de los campos.

▶ `_getSignature`

Función para obtener la firma actual.

▶ `_setSignature`

Acción para establecer la firma.

## Métodos

▶ `AddTarget(int, Func<string>)`

Registra un campo objetivo para la firma.

### Parámetros

- `index`: índice del campo.  
- `getter`: función que devuelve el valor del campo como string.

### Ejemplo

```csharp
signHelper.AddTarget(0, () => "valorCampo");
```

--------------------------------------------------------

▶ `InitGetter(Func<string>)`

Inicializa la función que obtiene la firma actual.

### Parámetros

- `getter`: función que devuelve la firma.

### Ejemplo

```csharp
signHelper.InitGetter(() => currentSign);
```

--------------------------------------------------------

▶ `InitSetter(Action<string>)`

Inicializa la acción que establece la firma.

### Parámetros

- `setter`: acción que recibe el nuevo valor de la firma.

### Ejemplo

```csharp
signHelper.InitSetter(newSign => currentSign = newSign);
```

--------------------------------------------------------

▶ `GetContent()`

Concatena todos los campos registrados para producir el contenido bruto para firmar.

### Resultado

`NativeMemoryOwner<char>` con la concatenación de los strings de los campos.

### Ejemplo

```csharp
using var content = signHelper.GetContent();
```

--------------------------------------------------------

▶ `Sign()`

Concatena los getters de los campos para producir el contenido listo para firmar.

### Resultado

`NativeMemoryOwner<char>` con el contenido para firmar.

### Ejemplo

```csharp
using var signature = signHelper.Sign();
```

--------------------------------------------------------

▶ `CheckSign()`

Verifica la firma actual contra el contenido bruto y actualiza la firma si es necesario.

### Ejemplo

```csharp
signHelper.CheckSign();
```