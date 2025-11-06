# LawnStrings

Representa una lista de cadenas utilizadas en *PvZ 2* (versión **8.9.1 y posteriores**).

## Constructores

▶ `LawnStrings()`

Crea una nueva instancia de la clase `LawnStrings`, inicializando los objetos internos y sus valores de localización.

### Ejemplo

```csharp
LawnStrings strings = new LawnStrings();
```

## Métodos

▶ `CheckObjs()`

Verifica e inicializa los campos internos en caso de ser nulos, garantizando la estructura mínima requerida por el objeto.

### Ejemplo

```csharp
strings.CheckObjs();
```

---

▶ `FromPlainText(Stream, LawnStringsEncoding)`

Crea una nueva instancia de `LawnStrings` a partir de un flujo con contenido en texto plano.

### Parámetros

* source: flujo de entrada con los datos en texto plano.
* encoding: codificación utilizada para interpretar los datos.

### Resultado

Una nueva instancia de `LawnStrings` con los datos cargados.

### Ejemplo

```csharp
using FileStream fs = File.OpenRead("LawnStrings.txt");
LawnStrings list = LawnStrings.FromPlainText(fs, LawnStringsEncoding.UTF8);
```

---

▶ `ToPlainText(Stream, LawnStringsEncoding)`

Convierte la instancia actual en texto plano y la escribe en un flujo de salida.

### Parámetros

* target: flujo de salida donde se escribirán los datos.
* encodeFlags: codificación utilizada para la conversión.

### Ejemplo

```csharp
using FileStream fs = File.Create("output.txt");
strings.ToPlainText(fs, LawnStringsEncoding.UTF8);
```

---

▶ `ToMap()`

Convierte esta instancia en un objeto `LawnStringsMap`, transformando los pares clave-valor de la lista a un diccionario.

### Resultado

Un objeto `LawnStringsMap` que contiene los pares clave-valor representados en la lista actual.

### Ejemplo

```csharp
LawnStringsMap map = strings.ToMap();
```

---

▶ `Sort()`

Ordena alfabéticamente los pares clave-valor dentro de la lista utilizando comparaciones culturales invariantes.

### Ejemplo

```csharp
strings.Sort();
```

---

▶ `FindAdded(LawnStrings, LawnStrings, HashSet<string>)`

Obtiene las cadenas nuevas que aparecen en la segunda instancia de `LawnStrings` pero no en la primera.

### Parámetros

* a: lista base utilizada como referencia.
* b: lista que se compara contra la base.
* excludeList: conjunto opcional de claves a excluir.

### Resultado

Una lista de cadenas que fueron añadidas en `b` respecto a `a`

### Ejemplo

```csharp
var added = LawnStrings.FindAdded(oldList, newList);
```

---

▶ `FindChanged(LawnStrings, LawnStrings, HashSet<string>)`

Obtiene las cadenas cuyo valor ha cambiado entre dos instancias de `LawnStrings`.

### Parámetros

* a: lista base.
* b: lista comparada.
* excludeList: conjunto opcional de claves a excluir.

### Resultado

Una lista de cadenas que fueron modificadas en `b` respecto a `a`

### Ejemplo

```csharp
var changed = LawnStrings.FindChanged(oldList, newList);
```

---

▶ `FindFullDiff(LawnStrings, LawnStrings, HashSet<string>)`

Obtiene las cadenas que fueron agregadas o modificadas entre dos instancias de `LawnStrings`.

### Parámetros

* a: lista base.
* b: lista comparada.
* excludeList: conjunto opcional de claves a excluir.

### Resultado

Una lista de cadenas con todas las diferencias.

### Ejemplo

```csharp
var diff = LawnStrings.FindFullDiff(oldList, newList);
```