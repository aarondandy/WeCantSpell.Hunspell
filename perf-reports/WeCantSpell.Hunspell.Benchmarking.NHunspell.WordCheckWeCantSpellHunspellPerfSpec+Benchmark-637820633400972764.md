# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/05/2022 07:49:00_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 6.2.9200.0
ProcessorCount=16
CLR=4.0.30319.42000,IsMono=False,MaxGcGeneration=2
```

### NBench Settings
```ini
RunMode=Throughput, TestMode=Measurement
NumberOfIterations=3, MaximumRunTime=00:00:01
Concurrent=False
Tracing=False
```

## Data
-------------------

### Totals
|          Metric |           Units |             Max |         Average |             Min |          StdDev |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,645,816.00 |    5,645,816.00 |    5,645,816.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           75.00 |           75.00 |           75.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,027.00 |        1,024.00 |        1,020.00 |            3.61 |
|[Counter] _wordsChecked |      operations |      654,752.00 |      654,752.00 |      654,752.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,534,615.07 |    5,514,928.12 |    5,496,812.26 |       18,950.31 |
|TotalCollections [Gen0] |     collections |           73.52 |           73.26 |           73.02 |            0.25 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.95 |        1,000.25 |          999.90 |            0.61 |
|[Counter] _wordsChecked |      operations |      641,855.90 |      639,572.78 |      637,471.86 |        2,197.69 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    5,645,816.00 |    5,534,615.07 |          180.68 |
|               2 |    5,645,816.00 |    5,513,357.05 |          181.38 |
|               3 |    5,645,816.00 |    5,496,812.26 |          181.92 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           75.00 |           73.52 |   13,601,225.33 |
|               2 |           75.00 |           73.24 |   13,653,668.00 |
|               3 |           75.00 |           73.02 |   13,694,764.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,020,091,900.00 |
|               2 |            0.00 |            0.00 |1,024,025,100.00 |
|               3 |            0.00 |            0.00 |1,027,107,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,020,091,900.00 |
|               2 |            0.00 |            0.00 |1,024,025,100.00 |
|               3 |            0.00 |            0.00 |1,027,107,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,020.00 |          999.91 |    1,000,090.10 |
|               2 |        1,025.00 |        1,000.95 |      999,048.88 |
|               3 |        1,027.00 |          999.90 |    1,000,104.48 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      654,752.00 |      641,855.90 |        1,557.98 |
|               2 |      654,752.00 |      639,390.58 |        1,563.99 |
|               3 |      654,752.00 |      637,471.86 |        1,568.70 |


