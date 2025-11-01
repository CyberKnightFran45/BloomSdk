# CryptoParams

Esta clase gestiona parámetros usados en la **encriptación y desencriptación de datos**, incluyendo derivación de claves y generación de vectores de inicialización (IV).

## Métodos

---

▶ `KeySchedule(byte[], bool, int, byte[], string, uint?)`

Genera una clave derivada a partir de una existente, aplicando iteraciones de hash si se requiere.

### Parámetros

* `key`: El bloque de bytes que representa la clave a derivar.
* `deriveKeys`: Indica si se deben derivar nuevas claves a partir de la clave original.
* `expectedKeySize`: Tamaño esperado de la clave.
* `salt`: Valor de sal usado para reforzar la clave. Valor por defecto: `null`.
* `hashType`: Algoritmo hash a usar. Valor por defecto: `""`.
* `iterations`: Número de iteraciones a aplicar. Valor por defecto: `null`.

### Ejemplo

```csharp
byte[] key = Encoding.UTF8.GetBytes("MiClaveSecreta");
CryptoParams.KeySchedule(ref key, true, 32);
```

---

▶ `InitVector(ReadOnlySpan<byte>, int, int)`

Inicializa un **vector de inicialización (IV)** a partir de una clave.

### Parámetros

* `key`: Clave de bytes a usar para generar el IV.
* `length`: Longitud deseada del vector.
* `startIndex`: Posición desde la cual comenzar a copiar bytes de la clave. Valor por defecto: `0`.

### Resultado

Un arreglo de bytes (`byte[]`) representando el vector de inicialización.

### Ejemplo

```csharp
byte[] key = Encoding.UTF8.GetBytes("MiClaveSecreta");
byte[] iv = CryptoParams.InitVector(key, 16);
```

---

▶ `InitVector(ReadOnlySpan<byte>, Limit<int>, int)`

Inicializa un **vector de inicialización (IV)** a partir de una clave, respetando un rango de tamaño esperado.

### Parámetros

* `key`: Clave de bytes a usar para generar el IV.
* `expectedLength`: Rango de tamaño esperado del vector (`Limit<int>`).
* `startIndex`: Posición desde la cual comenzar a copiar bytes de la clave. Valor por defecto: `0`.

### Resultado

Un arreglo de bytes (`byte[]`) representando el vector de inicialización.

### Ejemplo

```csharp
byte[] key = Encoding.UTF8.GetBytes("MiClaveSecreta");
Limit<int> sizeRange = new(16);
byte[] iv = CryptoParams.InitVector(key, sizeRange);
```