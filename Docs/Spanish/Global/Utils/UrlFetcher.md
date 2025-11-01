# UrlFetcher

Permite la comunicación con un servidor a través de una URL utilizando protocolos HTTP. 

Provee funciones para construir solicitudes (GET, POST), procesar flujos, parsear peticiones, y manejar respuestas tanto en texto como en flujo de datos.

## Métodos

▶ BuildGetRequest(string)

Construye una solicitud **GET** en formato HTTP crudo y la devuelve como una cadena de caracteres.

### Parámetros

* `url`: dirección URL a la que se enviará la solicitud.

### Resultado

Instancia de `NativeMemoryOwner<char>` que contiene la solicitud formateada.

---

▶ BuildGetRequest(string, Stream)

Construye una solicitud **GET** y la escribe directamente en un flujo.

### Parámetros

* `url`: dirección URL a la que se enviará la solicitud.
* `writer`: flujo donde se escribirá la solicitud.

---

▶ BuildPostRequest(string, string, string)

Construye una solicitud **POST** en formato HTTP crudo y la devuelve como una cadena de caracteres.

### Parámetros

* `url`: dirección URL de destino.
* `content`: contenido que se enviará en el cuerpo del mensaje.
* `contentType`: tipo MIME del contenido. Valor por defecto: `"application/json"`.

### Resultado

Instancia de `NativeMemoryOwner<char>` con la solicitud POST formateada.

---

▶ BuildPostRequest(string, string, Stream, string)

Construye una solicitud **POST** y la escribe directamente en un flujo.

### Parámetros

* `url`: dirección URL de destino.
* `content`: cuerpo del mensaje.
* `writer`: flujo donde se escribirá la solicitud.
* `contentType`: tipo MIME del contenido. Valor por defecto: `"application/json"`.

---

▶ BuildQuery<T>(string, T)

Construye una cadena de consulta (`query string`) a partir de un documento HTTP.

### Parámetros

* `baseUrl`: URL base sin parámetros.
* `doc`: documento HTTP que contiene los parámetros.

### Resultado

Cadena con la URL y sus parámetros codificados.

---

▶ BuildQuery(string, Dictionary<string, string>)

Construye una cadena de consulta (`query string`) a partir de un diccionario.

### Parámetros

* `baseUrl`: URL base.
* `parameters`: pares clave-valor que serán codificados.

### Resultado

Cadena con la URL y los parámetros combinados.

---

▶ ParseGetRequest(Stream)

Lee una solicitud **GET** desde un flujo y devuelve la URL base.

### Parámetros

* `reader`: flujo que contiene la solicitud HTTP.

### Resultado

Cadena con la URL base o `null` si la solicitud no es válida.

---

▶ ParsePostBody(Stream)

Extrae el cuerpo (`body`) de una solicitud **POST** desde un flujo.

### Parámetros

* `reader`: flujo que contiene la solicitud HTTP.

### Resultado

Instancia de `NativeString` con el contenido del cuerpo.

---

▶ ParseQuery(string)

Analiza una cadena de consulta (`query string`) y devuelve sus parámetros en un diccionario.

### Parámetros

* `query`: cadena de consulta a analizar.

### Resultado

`Dictionary<string, string>` con los parámetros y sus valores.

---

▶ ParseQuery<T>(string)

Analiza una cadena de consulta y la convierte en un documento HTTP del tipo especificado.

### Parámetros

* `query`: cadena de consulta a analizar.

### Resultado

Instancia de `T` que representa el documento HTTP.

---

▶ GetResponseAsync(string)

Envía una solicitud **GET** asíncrona al servidor y devuelve la respuesta como texto.

### Parámetros

* `url`: dirección URL de destino.

### Resultado

Tarea que representa la operación asíncrona, cuyo resultado es el cuerpo de la respuesta como cadena.

---

▶ GetResponseStreamAsync(string)

Envía una solicitud **GET** asíncrona y devuelve la respuesta como flujo.

### Parámetros

* `url`: dirección URL de destino.

### Resultado

Tarea que devuelve un `Stream` con los datos recibidos.

---

▶ PostRequestAsync(string, string, string)

Envía una solicitud **POST** asíncrona con contenido textual.

### Parámetros

* `url`: dirección del servidor.
* `content`: cuerpo del mensaje.
* `contentType`: tipo MIME del contenido. Valor por defecto: `"application/json"`.

### Resultado

Tarea que devuelve la respuesta del servidor como texto.

---

▶ PostRequestStreamAsync(string, Stream, string)

Envía una solicitud **POST** asíncrona con un flujo como contenido y devuelve la respuesta como flujo.

### Parámetros

* `url`: dirección del servidor.
* `content`: flujo que contiene los datos a enviar.
* `contentType`: tipo MIME del contenido. Valor por defecto: `"application/json"`.

### Resultado

Tarea que devuelve un `Stream` con los datos de respuesta.

---

▶ DownloadFileAsync(string, string)

Descarga un archivo desde una URL y lo guarda en la ruta indicada.

### Parámetros

* `url`: dirección del archivo a descargar.
* `filePath`: ruta local donde se almacenará el archivo.

---

▶ UploadFileAsync(string, string, string, string)

Sube un archivo a un servidor y, opcionalmente, guarda la respuesta en una ubicación local.

### Parámetros

* `sourcePath`: ruta del archivo a subir.
* `url`: dirección del servidor.
* `contentType`: tipo MIME del contenido. Valor por defecto: `"application/json"`.
* `responsePath`: ruta opcional para guardar la respuesta.