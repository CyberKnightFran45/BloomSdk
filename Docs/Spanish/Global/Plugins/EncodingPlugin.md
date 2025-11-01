# EncodingPlugin

Proporciona extensiones para obtener instancias de `Encoding` a partir de un `EncodingType`, con caching y soporte para proveedores de páginas de código.

## Métodos

▶ `GetEncoding(EncodingType)`

Obtiene la instancia de `Encoding` correspondiente a un valor de `EncodingType`. Si la codificación no es soportada, retorna `ASCII`.

### Parámetros

* `type`: tipo de codificación basado en `EncodingType`.

### Resultado

* `Encoding`: instancia de `System.Text.Encoding` asociada al tipo solicitado.

### Ejemplo

```csharp
Encoding enc = EncodingType.ISO_8859_1.GetEncoding();
byte[] bytes = enc.GetBytes("Hola mundo");
```