# LawnStringsCompareMode

Define los modos de comparación utilizados al analizar diferencias entre dos archivos LawnStrings.

## Valores

| Valor      | Descripción                                                                               |
| :--------- | :---------------------------------------------------------------------------------------- |
| `Added`    | Compara y devuelve únicamente las cadenas nuevas encontradas en el archivo remoto.        |
| `Changed`  | Compara y devuelve solo las cadenas cuyo valor fue modificado.                            |
| `FullDiff` | Realiza una comparación completa, incluyendo cadenas agregadas, modificadas y eliminadas. |