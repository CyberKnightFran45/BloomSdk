# 🌸 Blossom SDK

Colección de librerías modulares desarrolladas en **C# (.NET 9+)** por **FranZ**. Están diseñadas para la manipulación de archivos usados en juegos y apps, especialmente títulos de **EA** y **PopCap Games**. Este toolkit también es accesible para usuarios sin experiencia en programación que deseen crear sus propios **MODS**.

También incluye apps multiplataforma (Android, iOS, Windows) que implementan estas librerías de forma directa.

---

## 📦 Librerías disponibles


### BlossomLib

- Librería base de todo el proyecto.
- Todas las demás DLLs la utilizan como dependencia.

---

### SexyCalculator

Herramientas matemáticas y de manipulado de enteros:

- `BinaryConverter`: operaciones bit a bit entre decimales y hexadecimales.
- `VarInt`: enteros codificados en 7 bits (usado en archivos RTON, PvZ2).
- `UnixTimestamp`: conversion between `DateTime` and `Unix Time`.
- `IntGuard`: ofuscación de enteros (usado en algunos nodos del `pp.dat`, PvZ2 Chino).

---

### TextHandler

- Manejo de archivos de texto plano: `LawnStrings`, `PopCapCompiledText`

---

### SexyParsers

- Codificadores de estructuras binarias: `CFW2`, `RTON`, `NEWTON`

---

### SexyCryptor

- Soporte de encriptaciones usadas en juegos: `CDAT`, `XXLUA`, `C-RTON`, `Network Requests/Responses`, `Raw Payloads`

---

### SexyCompressor

- Manejo de compresiones en paquetes de datos: `PAK`, `XPR`, `RSB`, `RSGP`, `SMF`, `SOE`

---

### TextureDrawer

- Soporte para formatos de textura: `U-TEX`, `SEXYTEX`, `TXZ`, `XNB`, `PTX`

---

### AudioPlayer

- Reproducción de formatos de audio: `MP3`, `BNK`, `WAV`, `OGG`

---

### AnimViewer

- Parser y visualizador de animaciones: `PAM`, `XFL`, `Flash`, `BBONE`, `ReAnim`, `Particles`

---

### SexyUtils

- Utilidades generales que combinan funcionalidades de todas las DLLs anteriores.

---

## 📜 Licencia y uso

Este proyecto fue creado como un hobby y es completamente **libre y gratuito**. Es de **código abierto** y puede ser utilizado en cualquier otro proyecto, **sin necesidad de atribución**

---

> Hecho con ❤️ por FranZ, ¡disfruta!