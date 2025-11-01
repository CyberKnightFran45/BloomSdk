# JsonSerializer

Esta clase contiene funciones para serializar y deserializar objetos y trabajar con datos JSON.

## Propiedades

▶ `Options`

Opciones predeterminadas para la serialización y deserialización JSON (`JsonSerializerOptions`).

### Resultado

`JsonSerializerOptions` con la configuración definida en la clase.

## Métodos

▶ `CleanJsonString(string)`

Limpia un string JSON eliminando caracteres innecesarios y ajustando el formato.

### Parámetros

* `sourceStr`: El string que será limpiado.

---

▶ `ParseExpando(string)`

Parsea un string JSON y lo convierte en un objeto dinámico (expando).

### Parámetros

* `str`: El string JSON a parsear.

### Resultado

Un objeto dinámico con los datos del JSON.

---

▶ `ParseJson(string)`

Convierte un string JSON en un `Dictionary<string, object>`.

### Parámetros

* `jsonStr`: El string JSON a parsear.

### Resultado

Un diccionario con los datos del JSON.

---

▶ `ParseArray(string)`

Convierte un string JSON en un `List<object>`.

### Parámetros

* `jsonStr`: El string JSON a parsear.

### Resultado

Una lista con los elementos del JSON.

---

▶ `IsJson(string)`

Verifica si un string es un JSON válido de tipo objeto.

### Parámetros

* `str`: El string a verificar.

### Resultado

`true` si el string es un JSON objeto válido, `false` en caso contrario.

---

▶ `IsSpecialString(string)`

Verifica si un string es un JSON especial (ej. "[name]").

### Parámetros

* `str`: El string a verificar.

### Resultado

`true` si el string es especial, `false` en caso contrario.

---

▶ `IsJsonArray(string)`

Verifica si un string es un JSON válido de tipo arreglo.

### Parámetros

* `str`: El string a verificar.

### Resultado

`true` si el string es un JSON array válido, `false` en caso contrario.

---

▶ `SerializeObject<T>(T, JsonSerializerContext)`

Convierte un objeto en un string JSON.

### Parámetros

* `obj`: El objeto a serializar.
* `context`: Opcional, contexto de serialización (`JsonSerializerContext`).

### Resultado

El objeto serializado como string.

---

▶ `SerializeObject<T>(T, Stream, JsonSerializerContext)`

Convierte un objeto en JSON y lo escribe en un stream.

### Parámetros

* `obj`: El objeto a serializar.
* `writer`: Stream donde se escribirá el JSON.
* `context`: Opcional, contexto de serialización (`JsonSerializerContext`).

---

▶ `DeserializeObject<T>(string, JsonSerializerContext)`

Convierte un string JSON en un objeto del tipo `T`.

### Parámetros

* `json`: El string JSON a deserializar.
* `context`: Opcional, contexto de serialización (`JsonSerializerContext`).

### Resultado

Objeto deserializado del tipo `T`.

---

▶ `DeserializeObject<T>(Stream, JsonSerializerContext)`

Convierte un stream con JSON en un objeto del tipo `T`.

### Parámetros

* `reader`: Stream que contiene el JSON.
* `context`: Opcional, contexto de serialización (`JsonSerializerContext`).

### Resultado

Objeto deserializado del tipo `T`.