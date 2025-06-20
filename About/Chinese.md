# 🌸 Blossom SDK

由**FranZ**用**C#（.NET 9+）**开发的模块化库集合。这些库旨在处理游戏和应用程序中使用的文件，尤其是来自**EA**和**PopCap Games**的文件。非程序员也可以使用该工具包制作自己的**MODS**。

该工具包还包括可直接使用这些库的跨平台应用程序（Android、iOS、Windows）。

---

## 📦 可用库


### BlossomLib

- 项目的核心库。
- 所有其他 DLL 都将其作为依赖库。

---

### SexyCalculator

数学和整数操作工具：

- `BinaryConverter`: 十进制和十六进制整数的位操作。
- `VarInt`: 7 位编码整数（在 RTON 和 PvZ2 中使用）。
- `UnixTimestamp`：`DateTime` 和 `Unix Time` 之间的转换。
- IntGuard：整数混淆（在 PvZ2 中文版的 `pp.dat` 节点中使用）。

---

### TextHandler

- 处理原始文本文件： `LawnStrings`、`PopCapCompiledText`

---

### SexyParsers

- 二进制格式解析器： `cfw2`、`rton`、`newton`

---

### SexyCryptor

- 支持多种加密格式： `cdat`、`xxlua`、 `c-rton`、 `网络请求/响应`、 `原始有效载荷`

---

#### SexyCompressor

- 游戏数据包的压缩处理： `pak`,`xpr`,`rsb`,`rsgp`,`smf`,`soe`

---

### TextureDrawer

- 图像格式支持： `u-tex`, `sexytex`, `txz`, `xnb`, `ptx`

---

### AudioPlayer

- 支持音频格式播放： `mp3`, `bnk`, `wav`, `ogg`

---

### AnimViewer

- 动画格式查看器和解析器： `PAM`, `XFL`, `Flash`, `BBONE`, `ReAnim`, `Particles`

---

### SexyUtils

- 综合上述所有库功能的通用工具。

---

## 📜 许可与使用

本项目是作为业余爱好创建的，完全**自由和开源**。可在任何其他项目中使用，**无需注明出处**。

---

> 由 FranZ 使用 ❤️ 制作，敬请欣赏！