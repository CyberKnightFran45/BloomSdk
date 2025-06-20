# 🌸 Blossom SDK

Une collection de bibliothèques modulaires développées en **C# (.NET 9+)** par **FranZ**. Ces bibliothèques sont conçues pour gérer les fichiers utilisés dans les jeux et les applications, notamment de **EA** et **PopCap Games**. La boîte à outils est également accessible aux non-programmeurs qui veulent commencer à créer leurs propres **MODS**.

Le paquet comprend également des applications multiplateformes (Android, iOS, Windows) qui utilisent directement ces bibliothèques.

---

## 📦 Bibliothèques disponibles


### BlossomLib

- Bibliothèque principale du projet.
- Toutes les autres DLL y font référence en tant que dépendance.

---

### SexyCalculator

Utilitaires de manipulation de nombres entiers et de mathématiques :

- `BinaryConverter` : opérations sur les entiers décimaux et hexadécimaux.
- `VarInt` : entiers codés sur 7 bits (utilisés dans RTON, PvZ2).
- `UnixTimestamp` : conversion entre `DateTime` et `Unix Time`.
- `IntGuard` : obscurcissement des entiers (utilisé dans les noeuds `pp.dat` de PvZ2 Chinese).

---

### TextHandler

- Gère les fichiers texte bruts : `LawnStrings`, `PopCapCompiledText`

---

### SexyParsers

- Analyseurs de format binaire : `CFW2`, `RTON`, `NEWTON`

---

### SexyCryptor

- Prend en charge différents formats de cryptage : `CDAT`, `XXLUA`, `C-RTON`, `Network Requests/Responses`, `Raw Payloads` (charges utiles brutes)

---

### SexyCompressor

- Gestion de la compression pour les paquets de données de jeu : `PAK`, `XPR`, `RSB`, `RSGP`, `SMF`, `SOE`

---

### TextureDrawer

- Support des formats d'image : `U-TEX`, `SEXYTEX`, `TXZ`, `XNB`, `PTX`

---

### AudioPlayer

- Prise en charge de la lecture des formats audio : `MP3`, `BNK`, `WAV`, `OGG`

---

### AnimViewer

- Visualisateur et analyseur de formats d'animation : `PAM`, `XFL`, `Flash`, `BBONE`, `ReAnim`, `Particles`

---

### SexyUtils

- Utilitaires généraux combinant les caractéristiques de toutes les bibliothèques ci-dessus.

---

## 📜 Licence et utilisation

Ce projet a été créé en tant que hobby et est complètement **libre et open-source**. Il peut être utilisé dans n'importe quel autre projet **sans attribution**.

---

> Fabriqué avec ❤️ par FranZ, appréciez !