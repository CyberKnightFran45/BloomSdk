# 🌸 Blossom SDK

Una raccolta di librerie modulari sviluppata in **C# (.NET 9+)** da **FranZ**. Queste librerie sono progettate per gestire i file utilizzati nei giochi e nelle applicazioni, in particolare da **EA** e **PopCap Games**. Il toolkit è accessibile anche ai non programmatori che vogliono iniziare a creare i propri **MODS**.

Il pacchetto comprende anche applicazioni multipiattaforma (Android, iOS, Windows) che fanno uso diretto di queste librerie.

---

## 📦 Librerie disponibili


### BlossomLib

- Libreria principale del progetto.
- Tutte le altre DLL fanno riferimento a questa come dipendenza.

---

### SexyCalculator

Utilità per la manipolazione di numeri interi e matematica:

- `BinaryConverter`: operazioni bitwise su interi decimali ed esadecimali.
- `VarInt`: interi codificati a 7 bit (usati in RTON, PvZ2).
- `UnixTimestamp`: conversione tra `DateTime` e `Unix Time`.
- `IntGuard`: offuscamento degli interi (usato nei nodi `pp.dat` di PvZ2 Chinese).

---

### TextHandler

- Gestisce i file di testo grezzo: `LawnStrings`, `PopCapCompiledText`.

---

### SexyParser

- Analizzatori di formati binari: `CFW2`, `RTON`, `NEWTON`

---

### SexyCryptor

- Supporta vari formati di crittografia: `CDAT`, `XXLUA`, `C-RTON`, `Richieste/Risposte di rete`, `Carichi di pagamento grezzi`.

---

### SexyCompressor

- Gestione della compressione per i pacchetti di dati di gioco: `PAK`, `XPR`, `RSB`, `RSGP`, `SMF`, `SOE`

---

### TextureDrawer

- Supporto del formato immagine: `U-TEX`, `SEXYTEX`, `TXZ`, `XNB`, `PTX`

---

### AudioPlayer

- Supporto per la riproduzione di formati audio: `MP3`, `BNK`, `WAV`, `OGG`

---

### AnimViewer

- Visualizzatore e analizzatore di formati di animazione: `PAM`, `XFL`, `Flash`, `BBONE`, `ReAnim`, `Particles`.

---

### SexyUtils

- Utilità generali che combinano le caratteristiche di tutte le librerie precedenti.

---

## 📜 Licenza e uso

Questo progetto è stato creato per hobby ed è completamente **libero e open-source**. Può essere utilizzato in qualsiasi altro progetto **senza attribuzione**.

---

> Realizzato con ❤️ da FranZ, buon divertimento!