# IntGuard

Proporciona funciones para **cifrar y descifrar enteros** mediante operaciones bit a bit, usando ofuscación segura de valores numéricos.

## Notas

Este método de ofuscación es usado en la versión china de PvZ 2 para proteger ciertos valores de la modificación con editores de memoria, generalmente en nodos del `pp.dat`

## Métodos

▶ `Encrypt(uint)`

Cifra un valor entero realizando operaciones bit a bit y rotaciones basadas en la clave interna.

### Parámetros

* v: valor a cifrar.

### Resultado

Entero cifrado resultante.

### Ejemplo

```csharp
int encrypted = IntGuard.Encrypt(12345);
```

---

▶ `Decrypt(uint)`

Descifra un valor entero previamente cifrado con `Encrypt`.

### Parámetros

* v: valor a descifrar.

### Resultado

Entero original descifrado.

### Ejemplo

```csharp
int decrypted = IntGuard.Decrypt(encrypted); // 12345
```