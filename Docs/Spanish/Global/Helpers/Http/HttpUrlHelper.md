# HttpUrlHelper

Proporciona utilidades para leer y escribir contenido de formularios codificados en formato URL (`application/x-www-form-urlencoded`) dentro de flujos (`Stream`).

## Métodos

▶ `ReadContent(Stream)`

Lee el contenido de un formulario desde un flujo y devuelve todas las coincidencias de pares `nombre=valor` utilizando una expresión regular precompilada.

### Parámetros

* `reader`: flujo de entrada que contiene los datos del formulario en formato URL.

### Resultado

Colección `MatchCollection` con las coincidencias encontradas, donde cada coincidencia contiene los grupos `name` y `value`.

### Ejemplo

```csharp
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using var stream = new MemoryStream(Encoding.UTF8.GetBytes("user=Zeta&role=Admin"));
var matches = HttpUrlHelper.ReadContent(stream);

foreach (Match match in matches)
{
    Console.WriteLine($"{match.Groups["name"].Value} = {match.Groups["value"].Value}");
}
```

---

▶ `WriteContent(Stream, string, object, bool)`

Escribe un par `nombre=valor` en un flujo de salida en formato URL, escapando correctamente los valores según las reglas de codificación de URI.

### Parámetros

* `writer`: flujo donde se escribirá el contenido.
* `name`: nombre del campo de formulario.
* `val`: valor asociado al campo. Si es `null`, se escribe vacío.
* `isFirst` (false): indica si es el primer elemento del formulario. Si es falso, se antepone un carácter `&`.

### Ejemplo

```csharp
using System.IO;
using System.Text;

using var stream = new MemoryStream();
HttpUrlHelper.WriteContent(stream, "user", "Zeta", isFirst: true);
HttpUrlHelper.WriteContent(stream, "role", "Admin");

string result = Encoding.UTF8.GetString(stream.ToArray());
Console.WriteLine(result); // user=Zeta&role=Admin
```

---

▶ `FormRegex()`

Devuelve la expresión regular precompilada que identifica los pares `nombre=valor` dentro de un texto con formato de formulario.

### Ejemplo

```csharp
var regex = typeof(HttpUrlHelper)
    .GetMethod("FormRegex", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)!
    .Invoke(null, null) as Regex;

Console.WriteLine(regex!.ToString()); // (?<name>[^=&]+)=(?<value>[^&]*)
```