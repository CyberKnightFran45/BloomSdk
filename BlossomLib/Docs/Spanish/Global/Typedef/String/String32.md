# String32

Proporciona utilidades para convertir entre `uint` y `string` usando codificación ISO-8859-1.

## Métodos

▶ `ToInt(ReadOnlySpan<char>)`

Convierte una cadena en un valor `uint`.

### Parámetros

* `v`: cadena a convertir (se usan máximo los primeros 8 caracteres; si está vacía, devuelve `0`)

### Resultado

* `uint`: valor entero correspondiente a la cadena.

### Ejemplo

```csharp
uint value = String32.ToInt("ABCD");
```

---

▶ `FromInt(uint)`

Convierte un valor `uint` en una cadena.

### Parámetros

* `v`: valor entero a convertir (si es `0`, devuelve `null`)

### Resultado

* `string`: cadena correspondiente al valor entero.

### Ejemplo

```csharp
string text = String32.FromInt(1094861636); // "ABCD"
```