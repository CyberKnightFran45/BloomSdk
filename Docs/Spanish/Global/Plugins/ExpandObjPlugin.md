# ExpandObjPlugin

Proporciona métodos para trabajar con objetos dinámicos (`ExpandoObject`) y JSON, incluyendo conversión de tipos, parsing de `JObject`/`JArray` y comparación de objetos dinámicos.

## Métodos

▶ `ToExpandoObject(IDictionary<string, object>)`

Convierte un diccionario en un `ExpandoObject`.

### Parámetros

* `dictionary`: diccionario a convertir. Si es `null`, retorna `null`.

### Resultado

* `ExpandoObject`: objeto dinámico con las mismas claves y valores del diccionario.

### Ejemplo

```csharp
var dict = new Dictionary<string, object> { ["Name"] = "Juan" };
var expando = ExpandObjPlugin.ToExpandoObject(dict);
```

---

▶ `ToExpandoObject(JObject)`

Convierte un `JObject` de JSON en un `ExpandoObject`.

### Parámetros

* `jObject`: objeto JSON a convertir. Si es `null`, retorna `null`.

### Resultado

* `ExpandoObject`: objeto dinámico con los valores del `JObject`.

### Ejemplo

```csharp
JObject jObj = JObject.Parse("{\"Age\":30}");
var expando = ExpandObjPlugin.ToExpandoObject(jObj);
```

---

▶ `ConvertJArray(JArray)`

Convierte un `JArray` de JSON en una lista de objetos C#.

### Parámetros

* `array`: arreglo JSON a convertir.

### Resultado

* `List<object>`: lista de objetos que representan los elementos del `JArray`.

### Ejemplo

```csharp
JArray jArr = JArray.Parse("[1,2,3]");
List<object> list = ExpandObjPlugin.ConvertJArray(jArr);
```

---

▶ `CorrectTypes()`

Aplica conversión de tipos estricta a los campos de un `ExpandoObject`.

### Resultado

* `ExpandoObject`: objeto con los tipos corregidos.

### Ejemplo

```csharp
ExpandoObject obj = new ExpandoObject();
obj.CorrectTypes();
```

---

▶ `Compare(ExpandoObject, bool, bool, out ExpandoObject)`

Compara dos `ExpandoObject` y opcionalmente obtiene los cambios y propiedades añadidas.

### Parámetros

* `oldObj`: objeto original.
* `newObj`: objeto a comparar.
* `getObjChanges`: indica si se deben registrar cambios en valores existentes.
* `getAddedProps`: indica si se deben registrar propiedades añadidas.
* `diff`: variable de salida que contendrá las diferencias.

### Resultado

* `bool`: `true` si existen diferencias, `false` si los objetos son equivalentes.

### Ejemplo

```csharp
ExpandoObject oldObj = ExpandObjPlugin.ToExpandoObject(new Dictionary<string, object> { ["A"] = 1 });
ExpandoObject newObj = ExpandObjPlugin.ToExpandoObject(new Dictionary<string, object> { ["A"] = 2, ["B"] = 3 });
bool changed = oldObj.Compare(newObj, true, true, out var diff);
```