# LawnStringsResEntry

Representa un archivo dentro del servidor *LawnStrings* (utilizado en PvZ 2 China).

## Constructores

▶ `LawnStringsResEntry()`

Crea una nueva instancia de la clase `LawnStringsResEntry`

### Ejemplo

```csharp
LawnStringsResEntry entry = new LawnStringsResEntry();
```

## Propiedades

▶ `Name`

Nombre del archivo del recurso.

▶ `Hash`

Hash MD5 calculado a partir del archivo.

## Métodos

▶ `Create(Stream)`

Crea una nueva instancia de `LawnStringsResEntry` a partir de un flujo de datos, calculando su hash MD5.

### Parámetros

* source: flujo de datos del archivo cuyo hash se desea calcular.

### Resultado

Una nueva instancia de `LawnStringsResEntry` con el hash MD5 generado.

### Ejemplo

```csharp
using FileStream fs = File.OpenRead("pvz2_l.txt");
LawnStringsResEntry entry = LawnStringsResEntry.Create(fs);
```