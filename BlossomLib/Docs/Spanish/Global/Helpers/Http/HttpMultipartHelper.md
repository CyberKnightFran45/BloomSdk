# HttpMultipartHelper

Proporciona funciones auxiliares para procesar contenido de formularios en formato **multipart/form-data**, comúnmente utilizado en solicitudes HTTP con archivos o campos complejos.

## Constantes

| Constante    |                                  Valor                                 | Descripción                                                   |
| :----------- | :--------------------------------------------------------------------: | :------------------------------------------------------------ |
| `BOUNDARY`   |                              `"--_{{}}_"`                              | Cadena usada como delimitador entre secciones del formulario. |
| `FORM_FIELD` | `"{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}\r\n"` | Plantilla base para representar cada campo del formulario.    |

## Métodos

▶ `ReadContent(Stream)`

Lee el contenido de un formulario en formato **multipart** desde un flujo y devuelve las coincidencias de cada campo.

### Parámetros

* `reader`: flujo de entrada que contiene el contenido del formulario multipart.

### Resultado

Colección `MatchCollection` con las coincidencias encontradas, cada una con los grupos `name` y `value`.

### Ejemplo

```csharp
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

string formData = "--_{{}}_\r\nContent-Disposition: form-data; name=\"user\"\r\n\r\nZeta\r\n--_{{}}_";
using var stream = new MemoryStream(Encoding.UTF8.GetBytes(formData));

var matches = HttpMultipartHelper.ReadContent(stream);

foreach (Match match in matches)
{
    Console.WriteLine($"{match.Groups["name"].Value}: {match.Groups["value"].Value}");
}
```

---

▶ `WriteContent(Stream, string, object)`

Escribe un campo de formulario en formato **multipart/form-data**, utilizando la plantilla definida por `FORM_FIELD`.

### Parámetros

* `writer`: flujo de salida donde se escribirá el contenido.
* `name`: nombre del campo del formulario.
* `val`: valor del campo.

### Ejemplo

```csharp
using System.IO;
using System.Text;

using var stream = new MemoryStream();
HttpMultipartHelper.WriteContent(stream, "username", "Zeta");
string result = Encoding.UTF8.GetString(stream.ToArray());

Console.WriteLine(result);
```

---

▶ `WriteFooter(Stream)`

Escribe el pie final (`boundary` de cierre) que marca el final del contenido del formulario multipart.

### Parámetros

* `writer`: flujo donde se escribirá el delimitador final.

### Ejemplo

```csharp
using System.IO;
using System.Text;

using var stream = new MemoryStream();
HttpMultipartHelper.WriteFooter(stream);
string result = Encoding.UTF8.GetString(stream.ToArray());

Console.WriteLine(result); // --_{{}}_--
```

---

▶ `FormRegex()`

Devuelve la expresión regular precompilada que identifica los campos `form-data` y extrae sus nombres y valores.

### Ejemplo

```csharp
var regex = typeof(HttpMultipartHelper)
    .GetMethod("FormRegex", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)!
    .Invoke(null, null) as Regex;

Console.WriteLine(regex!.ToString());

// Content-Disposition:\s*form-data;\s*name="(?<name>[^"]+)"\s*\n\s*\n(?<value>.*?)(?=\n--|\Z)
```