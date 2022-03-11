# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/11/2022 01:50:09_
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
|TotalBytesAllocated |           bytes |      771,336.00 |      771,336.00 |      771,336.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           78.00 |           78.00 |           78.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,007.00 |        1,004.33 |        1,002.00 |            2.52 |
|[Counter] _wordsChecked |      operations |      663,040.00 |      663,040.00 |      663,040.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      770,206.57 |      768,205.28 |      766,153.43 |        2,027.04 |
|TotalCollections [Gen0] |     collections |           77.89 |           77.68 |           77.48 |            0.20 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.53 |        1,000.25 |          999.99 |            0.27 |
|[Counter] _wordsChecked |      operations |      662,069.14 |      660,348.83 |      658,585.07 |        1,742.44 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      771,336.00 |      766,153.43 |        1,305.22 |
|               2 |      771,336.00 |      768,255.83 |        1,301.65 |
|               3 |      771,336.00 |      770,206.57 |        1,298.35 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           78.00 |           77.48 |   12,907,235.90 |
|               2 |           78.00 |           77.69 |   12,871,914.10 |
|               3 |           78.00 |           77.89 |   12,839,312.82 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,006,764,400.00 |
|               2 |            0.00 |            0.00 |1,004,009,300.00 |
|               3 |            0.00 |            0.00 |1,001,466,400.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,006,764,400.00 |
|               2 |            0.00 |            0.00 |1,004,009,300.00 |
|               3 |            0.00 |            0.00 |1,001,466,400.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,007.00 |        1,000.23 |      999,766.04 |
|               2 |        1,004.00 |          999.99 |    1,000,009.26 |
|               3 |        1,002.00 |        1,000.53 |      999,467.47 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      663,040.00 |      658,585.07 |        1,518.41 |
|               2 |      663,040.00 |      660,392.29 |        1,514.25 |
|               3 |      663,040.00 |      662,069.14 |        1,510.42 |


