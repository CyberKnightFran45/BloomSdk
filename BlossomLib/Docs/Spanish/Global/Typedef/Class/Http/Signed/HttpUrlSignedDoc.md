# HttpUrlSignedDoc<T>

Representa un documento HTTP codificado en URL con soporte para verificación de firma y digest.

--------------------------------------------------------

## Campos

▶ `_signer`

Helper utilizado para realizar la firma del documento.

## Métodos

▶ `Init()`

Inicializa el documento, los campos y la configuración interna, incluyendo la preparación del digest.

### Ejemplo

```csharp
Init();
```

--------------------------------------------------------

▶ `WriteForm(Stream)`

Escribe el formulario HTTP codificado en URL en un flujo de salida, verificando la firma antes de la escritura.

### Parámetros

- `writer`: flujo de salida

### Ejemplo

```csharp
WriteForm(responseStream);
```

--------------------------------------------------------

▶ `CheckSign()`

Verifica la validez de la firma del documento usando el helper `_signer`.

### Ejemplo

```csharp
CheckSign();
```

--------------------------------------------------------

▶ `SetupDigest()`

Configura la lógica del digest para la firma.
(Debe ser implementado por las clases derivadas).

### Ejemplo

```csharp
SetupDigest();
```