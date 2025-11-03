# VarInt

Proporciona funciones para convertir valores enteros a su representación **VarInt** (entero de longitud variable) y viceversa.

Este formato se utiliza comúnmente en protocolos binarios para optimizar el tamaño de datos según su magnitud.

## Métodos

▶ `ConvertTo(int)`

Calcula el valor **VarInt** correspondiente a un entero dado.

### Parámetros

* v: valor entero desde el cual se calculará el VarInt.

### Resultado

Entero resultante de la conversión a VarInt.

### Ejemplo

```csharp
int varInt = VarInt.ConvertTo(300);
```

---

▶ `ConvertFrom(int)`

Calcula el valor entero correspondiente a un **VarInt** dado.

### Parámetros

* v: valor VarInt desde el cual se calculará el entero.

### Resultado

Entero resultante de la conversión desde VarInt.

### Ejemplo

```csharp
int value = VarInt.ConvertFrom(300);
```