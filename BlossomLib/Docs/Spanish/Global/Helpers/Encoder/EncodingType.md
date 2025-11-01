# EncodingType

Representa los distintos tipos de codificación de texto admitidos para operaciones de transformación y lectura de datos. Cada valor corresponde a un identificador numérico reconocido por el sistema.

## Constantes

| Constante                 | Valor | Descripción                                       |
| :------------------------ | :---: | :------------------------------------------------ |
| `ANSI`                    |   0   | Codificación ANSI de Windows.                     |
| `ASMO_708`                |  708  | Codificación árabe estándar.                      |
| `DOS_720`                 |  720  | Codificación árabe DOS.                           |
| `CP866`                   |  866  | Codificación cirílica DOS.                        |
| `CP875`                   |  875  | Codificación IBM griega EBCDIC.                   |
| `IBM037`                  |   37  | Codificación IBM EBCDIC US-Canada.                |
| `IBM273`                  | 20273 | Codificación IBM Alemania.                        |
| `IBM277`                  | 20277 | Codificación IBM Dinamarca/Noruega.               |
| `IBM278`                  | 20278 | Codificación IBM Finlandia/Suecia.                |
| `IBM280`                  | 20280 | Codificación IBM Italia.                          |
| `IBM284`                  | 20284 | Codificación IBM España.                          |
| `IBM285`                  | 20285 | Codificación IBM Reino Unido.                     |
| `IBM290`                  | 20290 | Codificación IBM Japón Katakana.                  |
| `IBM297`                  | 20297 | Codificación IBM Francia.                         |
| `IBM420`                  | 20420 | Codificación IBM árabe.                           |
| `IBM423`                  | 20423 | Codificación IBM griega.                          |
| `IBM424`                  | 20424 | Codificación IBM hebrea.                          |
| `IBM500`                  |  500  | Codificación IBM EBCDIC internacional.            |
| `IBM850`                  |  850  | Codificación multilingüe DOS.                     |
| `IBM852`                  |  852  | Codificación DOS Europa Central.                  |
| `IBM855`                  |  855  | Codificación DOS cirílica.                        |
| `IBM857`                  |  857  | Codificación DOS turca.                           |
| `IBM860`                  |  860  | Codificación DOS portuguesa.                      |
| `IBM861`                  |  861  | Codificación DOS islandesa.                       |
| `DOS_862`                 |  862  | Codificación DOS hebrea.                          |
| `IBM863`                  |  863  | Codificación DOS Canadá-Francés.                  |
| `IBM864`                  |  864  | Codificación DOS árabe.                           |
| `IBM865`                  |  865  | Codificación DOS nórdica.                         |
| `IBM869`                  |  869  | Codificación DOS griega.                          |
| `IBM870`                  |  870  | Codificación IBM multilingüe (EBCDIC).            |
| `IBM871`                  | 20871 | Codificación IBM Islandia.                        |
| `IBM880`                  | 20880 | Codificación IBM cirílica.                        |
| `IBM905`                  | 20905 | Codificación IBM turca.                           |
| `IBM00858`                |  858  | Codificación EBCDIC con euro.                     |
| `IBM00924`                | 20924 | Codificación EBCDIC Latin-1/Open.                 |
| `IBM01047`                |  1047 | Codificación Latin-1/Open.                        |
| `IBM01140`                |  1140 | EBCDIC US/Canada con euro.                        |
| `IBM01141`                |  1141 | EBCDIC Alemania con euro.                         |
| `IBM01142`                |  1142 | EBCDIC Dinamarca/Noruega con euro.                |
| `IBM01143`                |  1143 | EBCDIC Finlandia/Suecia con euro.                 |
| `IBM01144`                |  1144 | EBCDIC Italia con euro.                           |
| `IBM01145`                |  1145 | EBCDIC España con euro.                           |
| `IBM01146`                |  1146 | EBCDIC Reino Unido con euro.                      |
| `IBM01147`                |  1147 | EBCDIC Francia con euro.                          |
| `IBM01148`                |  1148 | EBCDIC internacional con euro.                    |
| `IBM01149`                |  1149 | EBCDIC Islandia con euro.                         |
| `IBM1026`                 |  1026 | Codificación IBM turca.                           |
| `CP1025`                  | 21025 | Codificación IBM cirílica (variante).             |
| `IBM_Thai`                | 20838 | Codificación IBM tailandesa.                      |
| `EUC_JP`                  | 20932 | Codificación japonesa extendida.                  |
| `BIG5`                    |  950  | Codificación china tradicional.                   |
| `GB2312`                  |  936  | Codificación china simplificada.                  |
| `SHIFT_JIS`               |  932  | Codificación japonesa estándar.                   |
| `JOHAB`                   |  1361 | Codificación coreana JOHAB.                       |
| `KS_C_5601_1987`          |  949  | Codificación coreana estándar.                    |
| `UTF16`                   |  1200 | Codificación Unicode UTF-16 (Little Endian).      |
| `UTF16_BE`                |  1201 | Codificación Unicode UTF-16 (Big Endian).         |
| `UTF32`                   | 12000 | Codificación Unicode UTF-32 (Little Endian).      |
| `UTF32_BE`                | 12001 | Codificación Unicode UTF-32 (Big Endian).         |
| `UTF8`                    | 65001 | Codificación Unicode UTF-8.                       |
| `WINDOWS_874`             |  874  | Codificación tailandesa de Windows.               |
| `WINDOWS_1250`            |  1250 | Codificación Europa Central de Windows.           |
| `WINDOWS_1251`            |  1251 | Codificación cirílica de Windows.                 |
| `WINDOWS_1252`            |  1252 | Codificación Latin-1 de Windows.                  |
| `WINDOWS_1253`            |  1253 | Codificación griega de Windows.                   |
| `WINDOWS_1254`            |  1254 | Codificación turca de Windows.                    |
| `WINDOWS_1255`            |  1255 | Codificación hebrea de Windows.                   |
| `WINDOWS_1256`            |  1256 | Codificación árabe de Windows.                    |
| `WINDOWS_1257`            |  1257 | Codificación báltica de Windows.                  |
| `WINDOWS_1258`            |  1258 | Codificación vietnamita de Windows.               |
| `MACINTOSH`               | 10000 | Codificación Macintosh Latin.                     |
| `X_MAC_JAPANESE`          | 10001 | Codificación japonesa Macintosh.                  |
| `X_MAC_CHINESETRAD`       | 10002 | Codificación china tradicional Macintosh.         |
| `X_MAC_ARABIC`            | 10004 | Codificación árabe Macintosh.                     |
| `X_MAC_HEBREW`            | 10005 | Codificación hebrea Macintosh.                    |
| `X_MAC_GREEK`             | 10006 | Codificación griega Macintosh.                    |
| `X_MAC_CYRILLIC`          | 10007 | Codificación cirílica Macintosh.                  |
| `X_MAC_ROMANIAN`          | 10010 | Codificación rumana Macintosh.                    |
| `X_MAC_UKRAINIAN`         | 10017 | Codificación ucraniana Macintosh.                 |
| `X_MAC_THAI`              | 10021 | Codificación tailandesa Macintosh.                |
| `X_MAC_CE`                | 10029 | Codificación Europa Central Macintosh.            |
| `X_MAC_ICELANDIC`         | 10079 | Codificación islandesa Macintosh.                 |
| `X_MAC_TURKISH`           | 10081 | Codificación turca Macintosh.                     |
| `X_MAC_CROATIAN`          | 10082 | Codificación croata Macintosh.                    |
| `KOI8_R`                  | 20866 | Codificación cirílica KOI8-R.                     |
| `KOI8_U`                  | 21866 | Codificación cirílica ucraniana KOI8-U.           |
| `IBM737`                  |  737  | Codificación DOS griega alternativa.              |
| `IBM775`                  |  775  | Codificación báltica DOS.                         |
| `ISO_8859_1`              | 28591 | ISO Latin-1 Occidental.                           |
| `ISO_8859_2`              | 28592 | ISO Latin-2 Europa Central.                       |
| `ISO_8859_3`              | 28593 | ISO Latin-3 Sur de Europa.                        |
| `ISO_8859_4`              | 28594 | ISO Latin-4 Norte de Europa.                      |
| `ISO_8859_5`              | 28595 | ISO Latin/Cirílico.                               |
| `ISO_8859_6`              | 28596 | ISO Latin/Árabe.                                  |
| `ISO_8859_7`              | 28597 | ISO Latin/Griego.                                 |
| `ISO_8859_8`              | 28598 | ISO Latin/Hebreo.                                 |
| `ISO_8859_9`              | 28599 | ISO Latin/Turco.                                  |
| `ISO_8859_13`             | 28603 | ISO Latin/Báltico.                                |
| `ISO_8859_15`             | 28605 | ISO Latin Occidental con símbolo del euro.        |
| `US_ASCII`                | 20127 | Codificación ASCII estándar.                      |
| `X_CP20001`               | 20001 | Codificación china IBM (Taiwán).                  |
| `X_CP20003`               | 20003 | Codificación IBM china variante 3.                |
| `X_CP20004`               | 20004 | Codificación IBM china variante 4.                |
| `X_CP20005`               | 20005 | Codificación IBM china variante 5.                |
| `X_CP20261`               | 20261 | Codificación T.61 alemana.                        |
| `X_CP20269`               | 20269 | Codificación T.61 noruega.                        |
| `X_CP20936`               | 20936 | Codificación china GB2312 (obsoleta).             |
| `X_CP20949`               | 20949 | Codificación coreana (EUC-KR extendida).          |
| `X_CHINESE_CNS`           | 20000 | Codificación china CNS.                           |
| `X_CHINESE_ETEN`          | 20002 | Codificación china ETEN.                          |
| `X_EBCDIC_KOREANEXTENDED` | 20833 | Codificación coreana EBCDIC.                      |
| `X_EUROPA`                | 29001 | Codificación Europa extendida.                    |
| `X_IA5`                   | 20105 | Codificación internacional IA5 (ASCII extendido). |
| `X_IA5_GERMAN`            | 20106 | Variante IA5 alemana.                             |
| `X_IA5_SWEDISH`           | 20107 | Variante IA5 sueca.                               |
| `X_IA5_NORWEGIAN`         | 20108 | Variante IA5 noruega.                             |
