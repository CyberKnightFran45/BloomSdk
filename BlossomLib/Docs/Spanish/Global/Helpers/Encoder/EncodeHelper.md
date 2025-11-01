# EncodeHelper

Proporciona funciones auxiliares para la manipulación de cadenas y detección o selección de codificaciones de texto.

## Métodos

---

▶ `GetEncoding(EncodingType)`

Obtiene una instancia de `Encoding` según las banderas especificadas.

### Parámetros

* flags: valor del tipo `EncodingType` que determina la codificación a usar.

### Resultado

Instancia de `Encoding` correspondiente a las banderas especificadas.

### Ejemplo

```csharp
Encoding utf8 = EncodeHelper.GetEncoding(EncodingType.UTF8);
```

---

▶ `IsASCII(ReadOnlySpan<char>)`

Verifica si una cadena contiene únicamente caracteres ASCII.

### Parámetros

* str: cadena a verificar.

### Resultado

`true` si todos los caracteres son ASCII; de lo contrario, `false`.

### Ejemplo

```csharp
bool esAscii = EncodeHelper.IsASCII("Hello World");
```