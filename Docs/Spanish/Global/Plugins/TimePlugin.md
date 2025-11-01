# TimePlugin

Proporciona extensiones para medir y formatear tiempos de manera precisa con `Stopwatch` y comparar segundos de `DateTime`.

## Métodos

▶ `GetExactTime(Stopwatch)`

Devuelve un string con la duración transcurrida en un formato legible, adaptando la unidad según la magnitud del tiempo.

### Parámetros

* `timer`: instancia de `Stopwatch` cuyo tiempo transcurrido se desea formatear. Puede ser `null`.

### Resultado

* `string`: representación legible del tiempo transcurrido, usando unidades desde nanosegundos hasta días.

### Ejemplo

```csharp
Stopwatch sw = Stopwatch.StartNew();
// ... código a medir
string tiempo = sw.GetExactTime();
Console.WriteLine(tiempo);
```

---

▶ `SecondEquals(DateTime, DateTime)`

Comprueba si dos instancias de `DateTime` coinciden en el segundo exacto, ignorando fracciones de segundo.

### Parámetros

* `a`: primer `DateTime`.
* `b`: segundo `DateTime`.

### Resultado

* `bool`: `true` si ambos tiempos tienen el mismo segundo; `false` de lo contrario.

### Ejemplo

```csharp
DateTime t1 = DateTime.Now;
DateTime t2 = t1.AddMilliseconds(500);
bool iguales = t1.SecondEquals(t2); // true
```