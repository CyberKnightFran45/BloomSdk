# FieldReader<T>

Representa una interfaz que permite leer campos sin usar Reflection.

## Campos

▶ `_fieldGetters`

Diccionario que almacena la lógica para obtener los valores de los campos.

## Métodos

▶ `RegisterGetter(int, Func<string>)`

Registra un getter para un campo específico.

### Parámetros

- `index`: índice del campo.  
- `getter`: función que devuelve el valor del campo como string.

### Ejemplo

```csharp
RegisterGetter(0, () => obj.Campo);
````

--------------------------------------------------------

▶ `RegisterGetter(int)`

Registra un getter por defecto que devuelve un string vacío.

### Parámetros

- `index`: índice del campo.

### Ejemplo

```csharp
RegisterGetter(0); // devuelve string.Empty
```

--------------------------------------------------------

▶ `RegisterGetterRnd(int)`

Registra un getter que devuelve un valor aleatorio generado.

### Parámetros

- `index`: índice del campo.

### Ejemplo

```csharp
RegisterGetterRnd(0); // devuelve un string aleatorio
```

---

▶ `InitGetters()`

Inicializa todos los getters registrados.

### Ejemplo

```csharp
InitGetters();
```