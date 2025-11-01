# RijndaelMode

Representa los **modos de operación válidos** para el algoritmo **Rijndael**.

| Valor | Descripción                                                                            |
| :---- | :------------------------------------------------------------------------------------- |
| `CBC` | Cipher Block Chaining. Cada bloque depende del anterior.                               |
| `CFB` | Cipher Feedback. Permite cifrar bloques de tamaño menor que el bloque de la llave.     |
| `ECB` | Electronic Codebook. Bloques independientes (menos seguro).                            |
| `OFB` | Output Feedback. Convierte el cifrado en un flujo de bits.                             |
| `SIC` | Segmented Integer Counter (similar a CTR). Convierte el cifrado en un contador seguro. |