# Binary Converter

Proporciona operaciones bit a bit entre enteros y métodos para convertir valores numéricos a sus representaciones binarias y hexadecimales.

## Métodos

▶ `And(int, int)`

Realiza una operación lógica **AND** entre dos valores enteros.

### Parámetros

* a: primer valor.
* b: segundo valor.

### Resultado

Entero resultante de aplicar `a & b`.

### Ejemplo

```csharp
int result = BinaryConverter.And(6, 3); // 2
```

---

▶ `Or(int, int)`

Realiza una operación lógica **OR** entre dos valores enteros.

### Parámetros

* a: primer valor.
* b: segundo valor.

### Resultado

Entero resultante de aplicar `a | b`.

### Ejemplo

```csharp
int result = BinaryConverter.Or(6, 3); // 7
```

---

▶ `Xor(int, int)`

Realiza una operación lógica **XOR** entre dos valores enteros.

### Parámetros

* a: primer valor.
* b: segundo valor.

### Resultado

Entero resultante de aplicar `a ^ b`.

### Ejemplo

```csharp
int result = BinaryConverter.Xor(6, 3); // 5
```

---

▶ `Nor(int)`
Devuelve el complemento lógico (**NOT**) del valor dado.

### Parámetros

* a: valor a invertir.

### Resultado

Entero resultante de aplicar `~a`.

### Ejemplo

```csharp
int result = BinaryConverter.Nor(6); // -7
```

---

▶ `Shift(int, int, bool)`

Desplaza los bits del valor hacia la izquierda o la derecha según la dirección especificada.

### Parámetros

* a: valor base a desplazar.
* b: cantidad de bits a desplazar.
* toRight: si es `true`, desplaza a la derecha; si es `false`, a la izquierda.

### Resultado

Entero resultante del desplazamiento.

### Ejemplo

```csharp
int left = BinaryConverter.Shift(4, 1, false); // 8
int right = BinaryConverter.Shift(4, 1, true); // 2
```

---

▶ `ToHex(int)`
Convierte un número entero en su representación hexadecimal.

### Parámetros

* v: valor a convertir.

### Resultado

Cadena con el número en formato hexadecimal.

### Ejemplo

```csharp
string hex = BinaryConverter.ToHex(255); // "FF"
```

---

▶ `ToBin(int)`
Convierte un número entero en su representación binaria.

### Parámetros

* v: valor a convertir.

### Resultado

Cadena con el número en formato binario.

### Ejemplo

```csharp
string bin = BinaryConverter.ToBin(10); // "1010"
```

---

▶ `FromHex(string)`

Convierte una cadena en formato hexadecimal a su valor entero.

### Parámetros

* v: cadena con el número hexadecimal.

### Resultado

Entero equivalente al valor hexadecimal.

### Ejemplo

```csharp
int value = BinaryConverter.FromHex("FF"); // 255
```

---

▶ `FromBin(string)`

Convierte una cadena en formato binario a su valor entero.

### Parámetros

* v: cadena con el número binario.

### Resultado

Entero equivalente al valor binario.

### Ejemplo

```csharp
int value = BinaryConverter.FromBin("1010"); // 10
```