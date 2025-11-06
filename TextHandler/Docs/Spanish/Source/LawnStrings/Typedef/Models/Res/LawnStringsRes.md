# LawnStringsRes

Representa una entrada de servidor para el archivo *LawnStrings*.

## Constructores

▶ `LawnStringsRes()`

Crea una nueva instancia de la clase `LawnStringsRes`, inicializando la propiedad `File` con una nueva entrada.

### Ejemplo

```csharp
LawnStringsRes res = new LawnStringsRes();
```

## Propiedades

▶ `File`

Entrada del archivo *LawnStrings* que contiene los datos del recurso.

## Métodos

▶ `Init(Stream)`

Inicializa una nueva instancia de `LawnStringsRes` a partir de un flujo de datos que contiene el archivo *LawnStrings*.

### Parámetros

* source: flujo de datos de origen que contiene la información del archivo.

### Resultado

Una nueva instancia de `LawnStringsRes` con la entrada cargada desde el flujo.

### Ejemplo

```csharp
using FileStream fs = File.OpenRead("LawnStrings.res");
LawnStringsRes res = LawnStringsRes.Init(fs);
```