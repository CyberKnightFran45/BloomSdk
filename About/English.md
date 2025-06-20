# 🌸 Blossom SDK

A modular library collection developed in **C# (.NET 9+)** by **FranZ**. These libraries are designed for handling files used in games and apps, especially from **EA** and **PopCap Games**. The toolkit is also accessible to non-programmers who want to start making their own **MODS**.

The package also includes cross-platform applications (Android, iOS, Windows) that make direct use of these libraries.

---

## 📦 Available Libraries


### BlossomLib

- Core library of the project.
- All other DLLs reference this as a dependency.

---

### SexyCalculator

Mathematical and integer manipulation utilities:

- `BinaryConverter`: bitwise operations on decimal and hexadecimal integers.
- `VarInt`: 7-bit encoded integers (used in RTON, PvZ2).
- `UnixTimestamp`: conversion between `DateTime` and `Unix Time`.
- `IntGuard`: integer obfuscation (used in `pp.dat` nodes of PvZ2 Chinese).

---

### TextHandler

- Handles raw text files: `LawnStrings`, `PopCapCompiledText`

---

### SexyParsers

- Binary format parsers: `CFW2`, `RTON`, `NEWTON`

---

### SexyCryptor

- Supports various encryption formats: `CDAT`, `XXLUA`, `C-RTON`, `Network Requests/Responses`, `Raw Payloads`

---

### SexyCompressor

- Compression handling for game data packages: `PAK`, `XPR`, `RSB`, `RSGP`, `SMF`, `SOE`

---

### TextureDrawer

- Image format support: `U-TEX`, `SEXYTEX`, `TXZ`, `XNB`, `PTX`

---

### AudioPlayer

- Audio format playback support: `MP3`, `BNK`, `WAV`, `OGG`

---

### AnimViewer

- Animation format viewer and parser: `PAM`, `XFL`, `Flash`, `BBONE`, `ReAnim`, `Particles`

---

### SexyUtils

- General utilities combining features from all the above libraries.

---

## 📜 License & Usage

This project was created as a hobby and is completely **free and open-source**. It can be used in any other project **without attribution**.

---

> Made with ❤️ by FranZ, enjoy!