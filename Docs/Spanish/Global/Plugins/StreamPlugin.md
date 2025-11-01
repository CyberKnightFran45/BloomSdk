# StreamPlugin

Extiende `Stream` con métodos de lectura/escritura de tipos primitivos, cadenas y colecciones, soportando endianess, longitud variable y tipos grandes (`Int128`/`UInt128`).

## Métodos de lectura

▶ `ReadBytes(byte[], Endianness)`

Lee bytes en un buffer y aplica endianess.

### Parámetros

* `buffer`: buffer donde se almacenan los bytes leídos.
* `endian`: endianess a aplicar (`LittleEndian` o `BigEndian`).

---

▶ `ReadPtr()`
▶ `ReadPtr(long, Endianness)`

Lee un bloque de memoria y devuelve un `NativeMemoryOwner<byte>`.

### Parámetros

* `count`: cantidad de bytes a leer.
* `endian`: endianess a aplicar. Valor por defecto `LittleEndian`.

### Resultado

* `NativeMemoryOwner<byte>`: memoria con los datos leídos.

---

▶ `ReadBool()`

Lee un byte y devuelve `true` si es distinto de 0, `false` en caso contrario.

---

▶ `ReadChar(Endianness)`

Lee un `UInt16` y lo interpreta como `char`.

### Parámetros

* `endian`: endianess a aplicar.

---

▶ `ReadUInt8()`

Lee un byte y lo devuelve como `byte`. Lanza excepción al final del stream.

---

▶ `ReadInt8()`

Lee un byte y lo interpreta como `sbyte`.

---

▶ `ReadInt16(Endianness)`
▶ `ReadUInt16(Endianness)`

▶ `ReadInt24(Endianness)`
▶ `ReadUInt24(Endianness)`

▶ `ReadInt32(Endianness)`
▶ `ReadUInt32(Endianness)`

▶ `ReadInt64(Endianness)`
▶ `ReadUInt64(Endianness)`

▶ `ReadInt128(Endianness)`
▶ `ReadUInt128(Endianness)`

Leen los respectivos tipos aplicando endianess.

---

▶ `ReadVarInt()`
▶ `ReadVarUInt()`

▶ `ReadVarInt64()`
▶ `ReadVarUInt64()`

Lectura de enteros con codificación variable (VarInt).

---

▶ `ReadZigZag32()`
▶ `ReadZigZag64()`

Lectura de enteros con codificación ZigZag.

---

▶ `ReadFloat(Endianness)`
▶ `ReadDouble(Endianness)`

Lectura de flotantes con endianess.

---

▶ `ReadString()`
▶ `ReadString(long, Encoding, Endianness)`

Lee una cadena de longitud fija. Usa `Encoding` opcional y endianess.

---

▶ `ReadStringByLen8/16/32/64/128()`

▶ `ReadStringByVarLen()`

▶ `ReadStringByVarLen64()`

Lee cadenas precedidas por un prefijo que indica la longitud (`byte`, `ushort`, `uint`, `long`, `UInt128` o VarInt).

---

▶ `ReadCString()`

Lee una cadena C terminada en `\0`.

---

▶ `ReadLine()`

Lee hasta un salto de línea (`\n` o `\r\n`). Devuelve `null` al final de stream.

---

## Métodos de escritura

▶ `WriteBool(bool)`

▶ `WriteChar(char, Endianness)`

▶ `WriteInt8(sbyte)`

▶ `WriteInt16(short, Endianness)`
▶ `WriteUInt16(ushort, Endianness)`

▶ `WriteInt24(int, Endianness)`
▶ `WriteUInt24(uint, Endianness)`

▶ `WriteInt32(int, Endianness)`
▶ `WriteUInt32(uint, Endianness)`

▶ `WriteInt64(long, Endianness)`
▶ `WriteUInt64(ulong, Endianness)`

▶ `WriteInt128(Int128, Endianness)`
▶ `WriteUInt128(UInt128, Endianness)`

Escriben los respectivos tipos aplicando endianess.

---

▶ `WriteVarInt(int)`
▶ `WriteVarUInt(uint)`

▶ `WriteVarInt64(long)`
▶ `WriteVarUInt64(ulong)`

Escriben enteros con codificación variable.

---

▶ `WriteZigZag32(int)`
▶ `WriteZigZag64(long)`

Escriben enteros con codificación ZigZag.

---

▶ `WriteFloat(float, Endianness)`
▶ `WriteDouble(double, Endianness)`

Escriben flotantes aplicando endianess.

---

▶ `WriteBytes(Span<byte>, Endianness)`

Escribe bytes aplicando endianess.

---

▶ `WriteString(ReadOnlySpan<char>, Encoding, Endianness)`

Escribe cadena usando la codificación y endianess opcional.

---

▶ `WriteStringByLen8/16/32/64/128()`

▶ `WriteStringByVarLen()`

▶ `WriteStringByVarLen64()`

Escribe cadenas precedidas de la longitud (`byte`, `ushort`, `uint`, `ulong`, `UInt128` o VarInt).

---

▶ `WriteCString()`

Escribe cadena con terminador `\0`.

---

▶ `WriteLine()`

Escribe cadena seguida de `Environment.NewLine`.

## Métodos auxiliares

▶ `Pad(int, byte)`

Agrega relleno al stream hasta el múltiplo de `alignment`.

### Parámetros

* `alignment`: tamaño de alineamiento.
* `padding`: valor de byte para rellenar (por defecto `0x0`).

---

▶ `PeekByte()`

Devuelve el siguiente byte sin mover la posición del stream. Requiere `CanSeek = true`.