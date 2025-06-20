# 🌸 Blossom SDK

Uma coleção de bibliotecas modulares desenvolvida em **C# (.NET 9+)** por **FranZ**. Estas bibliotecas foram concebidas para lidar com ficheiros utilizados em jogos e aplicações, especialmente de **EA** e **PopCap Games**. O conjunto de ferramentas também é acessível a não-programadores que queiram começar a criar os seus próprios **MODS**.

O pacote também inclui aplicações multiplataforma (Android, iOS, Windows) que utilizam diretamente estas bibliotecas.

---

## Bibliotecas disponíveis


### BlossomLib

- Biblioteca principal do projeto.
- Todas as outras DLLs fazem referência a ela como uma dependência.

---

### SexyCalculator

Utilitários de manipulação matemática e de números inteiros:

- `BinaryConverter`: operações bit a bit em números inteiros decimais e hexadecimais.
- `VarInt`: inteiros codificados em 7 bits (usado em RTON, PvZ2).
- `UnixTimestamp`: conversão entre `DateTime` e `Unix Time`.
- `IntGuard`: ofuscação de inteiros (usado nos nós `pp.dat` do PvZ2 chinês).

---

### TextHandler

- Lida com arquivos de texto puro: `LawnStrings`, `PopCapCompiledText`

---

### SexyParsers

- Analisadores de formato binário: `CFW2`, `RTON`, `NEWTON`

---

### SexyCryptor

- Suporta vários formatos de encriptação: `CDAT`, `XXLUA`, `C-RTON`, `Solicitações/Respostas de rede`, `Cargas brutas`

---

### SexyCompressor

- Manipulação de compressão para pacotes de dados de jogos: `PAK`, `XPR`, `RSB`, `RSGP`, `SMF`, `SOE`

---

### TextureDrawer

- Suporte a formatos de imagem: `U-TEX`, `SEXYTEX`, `TXZ`, `XNB`, `PTX`

---

### AudioPlayer

- Suporte a reprodução de formatos de áudio: `MP3`, `BNK`, `WAV`, `OGG`

---

### AnimViewer

- Visualizador e analisador de formatos de animação: `PAM`, `XFL`, `Flash`, `BBONE`, `ReAnim`, `Particles`

---

### SexyUtils

- Utilitários gerais que combinam caraterísticas de todas as bibliotecas acima.

---

## 📜 Licença e Uso

Este projeto foi criado como um hobby e é completamente **livre e open-source**. Pode ser usado em qualquer outro projeto **sem atribuição**.

---

> Feito com ❤️ por FranZ, divirta-se!