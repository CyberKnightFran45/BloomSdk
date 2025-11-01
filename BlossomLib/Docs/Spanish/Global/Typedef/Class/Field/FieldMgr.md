# FieldMgr<T>

Representa una interfaz que permite leer y actualizar campos sin usar Reflection.

## Campos

▶ `_fieldSetters`

Diccionario que almacena la lógica para establecer valores de los campos.

## Métodos

▶ `RegisterSetter(int, Action<string>)`

Registra un setter para un campo específico.

### Parámetros

- `index`: índice del campo.  
- `setter`: acción que recibe el valor a asignar al campo.

### Ejemplo

```csharp
RegisterSetter(0, val => obj.Campo = val);
````

--------------------------------------------------------

▶ `InitSetters()`

Inicializa todos los setters registrados.

### Ejemplo

```csharp
InitSetters();
```

--------------------------------------------------------

▶ `Init()`

Inicializa la lógica de getters y setters de la clase.

### Ejemplo

```csharp
Init();
```