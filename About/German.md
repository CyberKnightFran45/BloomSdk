# 🌸 Blossom SDK

Eine modulare Bibliothekssammlung, entwickelt in **C# (.NET 9+)** von **FranZ**. Diese Bibliotheken sind für den Umgang mit Dateien in Spielen und Anwendungen, insbesondere von **EA** und **PopCap Games**, konzipiert. Das Toolkit ist auch für Nicht-Programmierer zugänglich, die ihre eigenen **MODS** erstellen möchten.

Das Paket enthält auch plattformübergreifende Anwendungen (Android, iOS, Windows), die diese Bibliotheken direkt nutzen.

---

## 📦 Verfügbare Bibliotheken


### BlossomLib

- Kernbibliothek des Projekts.
- Alle anderen DLLs referenzieren diese als Abhängigkeit.

---

### SexyCalculator

Mathematische und Integer-Manipulations-Hilfsmittel:

- `BinaryConverter`: Bitweise Operationen auf dezimalen und hexadezimalen Ganzzahlen.
- `VarInt`: 7-bit kodierte Ganzzahlen (verwendet in RTON, PvZ2).
- UnixTimestamp`: Konvertierung zwischen `DateTime` und `Unix Time`.
- IntGuard„: Integer-Verschleierung (wird in den “p.dat"-Knoten von PvZ2 Chinese verwendet).

---

### TextHandler

- Verarbeitet rohe Textdateien: `LawnStrings`, `PopCapCompiledText`

---

### SexyParsers

- Binärformat-Parser: `CFW2`, `RTON`, `NEWTON`

---

### SexyCryptor

- Unterstützt verschiedene Verschlüsselungsformate: `CDAT`, `XXLUA`, `C-RTON`, `Network Requests/Responses`, `Raw Payloads`

---

### SexyCompressor

- Kompressionsbehandlung für Spieldatenpakete: `PAK`, `XPR`, `RSB`, `RSGP`, `SMF`, `SOE`

---

### TextureDrawer

- Unterstützung von Bildformaten: `U-TEX`, `SEXYTEX`, `TXZ`, `XNB`, `PTX`

---

### AudioPlayer

- Unterstützung für die Wiedergabe von Audioformaten: `MP3`, `BNK`, `WAV`, `OGG`

---

### AnimViewer

- Betrachter und Parser für Animationsformate: `PAM`, `XFL`, `Flash`, `BBONE`, `ReAnim`, `Particles`

---

### SexyUtils

- Allgemeine Dienstprogramme, die Funktionen aus allen oben genannten Bibliotheken kombinieren.

---

## 📜 Lizenz & Verwendung

Dieses Projekt wurde als Hobby erstellt und ist komplett **frei und Open-Source**. Es kann in jedem anderen Projekt **ohne Namensnennung** verwendet werden.

---

> Erstellt mit ❤️ von FranZ, viel Spaß!