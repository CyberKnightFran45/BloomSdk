# RijndaelPadding

Representa los **tipos de relleno (padding)** disponibles para el algoritmo **Rijndael**.

| Valor         | Descripción                                                                     |
| :------------ | :------------------------------------------------------------------------------ |
| `ZeroPadding` | Rellena con ceros hasta completar el bloque.                                    |
| `ISO_7816d4`  | Relleno según la norma ISO/IEC 7816-4.                                          |
| `Pkcs7`       | Relleno PKCS#7 estándar.                                                        |
| `Tbc`         | Trailing Bit Complement, rellena con bits complementarios.                      |
| `X923`        | Relleno ANSI X.923, con ceros y el último byte indicando el tamaño del relleno. |
