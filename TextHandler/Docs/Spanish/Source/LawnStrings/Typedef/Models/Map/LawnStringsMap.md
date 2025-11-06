# LawnStringsMap

Representa un diccionario de strings usado en PvZ 2 (v6.8.1 - 8.8.1)

## Constructores

▶ `LawnStringsMap()`

Crea una nueva instancia de la clase `LawnStringsMap`, inicializando la estructura de objetos y los diccionarios internos.

### Ejemplo

```csharp
LawnStringsMap map = new LawnStringsMap();
```

## Métodos

▶ `CheckObjs()`

Verifica y asegura que todos los campos internos no sean nulos, inicializando objetos y diccionarios según sea necesario.

### Ejemplo

```csharp
map.CheckObjs();
```

---

▶ `FromPlainText(Stream, LawnStringsEncoding)`

Crea una nueva instancia de `LawnStringsMap` a partir de un `Stream` de PlainText.

### Parámetros

* source: Stream de entrada.
* encodeFlags: codificación utilizada.

### Resultado

Una nueva instancia de `LawnStringsMap` con los datos cargados desde PlainText.

### Ejemplo

```csharp
using FileStream fs = File.OpenRead("strings.txt");
LawnStringsMap map = LawnStringsMap.FromPlainText(fs, LawnStringsEncoding.UTF8);
```

---

▶ `ToPlainText(Stream, LawnStringsEncoding)`

Convierte la instancia actual en PlainText y la escribe en un `Stream`.

### Parámetros

* target: Stream de salida.
* encodeFlags: codificación utilizada.

### Ejemplo

```csharp
using FileStream fs = File.Create("output.txt");
map.ToPlainText(fs, LawnStringsEncoding.UTF8);
```

---

▶ `ToList()`

Convierte la instancia actual en un objeto `LawnStrings` representado como lista de strings (pares clave-valor).

### Resultado

Un objeto `LawnStrings` con los pares clave-valor de esta instancia.

### Ejemplo

```csharp
LawnStrings list = map.ToList();
```

---

▶ `Sort()`

Ordena los pares clave-valor alfabéticamente por clave dentro del diccionario interno.

### Ejemplo

```csharp
map.Sort();
```

---

▶ `FindAdded(LawnStringsMap, LawnStringsMap, HashSet<string>)`

Obtiene los pares clave-valor que existen en la segunda instancia pero no en la primera, excluyendo claves opcionales.

### Parámetros

* a: primera instancia.
* b: segunda instancia.
* excludeList: lista de claves a excluir (opcional).

### Resultado

Colección de pares clave-valor que se añadieron en `b` respecto a `a`.

### Ejemplo

```csharp
var added = LawnStringsMap.FindAdded(mapA, mapB);
```

---

▶ `FindChanged(LawnStringsMap, LawnStringsMap, HashSet<string>)`

Obtiene los pares clave-valor que cambiaron entre dos instancias de `LawnStringsMap`, excluyendo claves opcionales.

### Parámetros

* a: primera instancia.
* b: segunda instancia.
* excludeList: lista de claves a excluir (opcional).

### Resultado

Colección de pares clave-valor que cambiaron en `b` respecto a `a`.

### Ejemplo

```csharp
var changed = LawnStringsMap.FindChanged(mapA, mapB);
```

---

▶ `FindFullDiff(LawnStringsMap, LawnStringsMap, HashSet<string>)`

Obtiene los pares clave-valor que fueron añadidos o cambiados entre dos instancias de `LawnStringsMap`, excluyendo claves opcionales.

### Parámetros

* a: primera instancia
* b: segunda instancia
* excludeList: lista de claves a excluir (opcional).

### Resultado

Colección de pares clave-valor que se añadieron o cambiaron en `b` respecto a `a`

### Ejemplo

```csharp
var diff = LawnStringsMap.FindFullDiff(mapA, mapB);
```