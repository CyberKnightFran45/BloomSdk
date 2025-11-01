# MemoryManager

Esta clase proporciona utilidades para manejar memoria, calcular tamaños de buffer y bloques óptimos para streams y archivos.

## Métodos

▶ `GetBlockSize(Stream)`

Calcula la cantidad de bytes óptima para procesar un stream en bloques.

### Parámetros

* `targetStream`: El stream para el que se calcula el tamaño del bloque.

### Resultado

Un `int` representando el tamaño del bloque óptimo en bytes.

---

▶ `GetBlockSize(string)`

Calcula la cantidad de bytes óptima para procesar un archivo en bloques.

### Parámetros

* `filePath`: Ruta del archivo para el cual se calcula el tamaño del bloque.

### Resultado

Un `int` representando el tamaño del bloque óptimo en bytes.

---

▶ `GetBufferSize(Stream)`

Calcula el tamaño óptimo de buffer para un stream sin exceder la memoria disponible.

### Parámetros

* `targetStream`: El stream para el que se calcula el tamaño del buffer.

### Resultado

Un `int` representando el tamaño del buffer óptimo en bytes.

---

▶ `GetBufferSize(string)`

Calcula el tamaño óptimo de buffer para un archivo sin exceder la memoria disponible.

### Parámetros

* `filePath`: Ruta del archivo para el que se calcula el tamaño del buffer.

### Resultado

Un `int` representando el tamaño del buffer óptimo en bytes.

---

▶ `GetJsonSize(Stream)`

Calcula el tamaño óptimo de buffer para procesar un JSON en un stream.

### Parámetros

* `targetStream`: El stream JSON para el que se calcula el tamaño del buffer.

### Resultado

Un `int` representando el tamaño del buffer óptimo en bytes.