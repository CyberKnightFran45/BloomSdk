# FieldUpdater<T>

Representa una interfaz que permite actualizar campos sin usar Reflection.

## Campos

▶ `_fieldSetters`

Diccionario que almacena la lógica para asignar valores a los campos.

## Métodos

▶ `RegisterSetter(int, Action<string>)`

Registra un setter para un campo específico.

### Parámetros

- `index`: índice del campo.
- `setter`: delegado que asigna el valor del campo.

### Ejemplo

```csharp
RegisterSetter(0, value => obj.Campo = value);
```

--------------------------------------------------------

▶ `InitSetters()`

Inicializa todos los setters registrados.
(Debe ser implementado por las clases derivadas.)

### Ejemplo

```csharp
InitSetters();
```