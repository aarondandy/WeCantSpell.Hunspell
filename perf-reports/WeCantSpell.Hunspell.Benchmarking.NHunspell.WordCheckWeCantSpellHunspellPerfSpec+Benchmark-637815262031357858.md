# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_02/27/2022 02:36:43_
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
|TotalBytesAllocated |           bytes |    5,424,800.00 |    5,424,800.00 |    5,424,800.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           76.00 |           76.00 |           76.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,050.00 |        1,034.33 |        1,020.00 |           15.04 |
|[Counter] _wordsChecked |      operations |      663,040.00 |      663,040.00 |      663,040.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,316,735.75 |    5,245,414.79 |    5,166,303.00 |       75,518.38 |
|TotalCollections [Gen0] |     collections |           74.49 |           73.49 |           72.38 |            1.06 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.32 |          999.99 |          999.68 |            0.32 |
|[Counter] _wordsChecked |      operations |      649,831.97 |      641,114.85 |      631,445.50 |        9,230.15 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    5,424,800.00 |    5,166,303.00 |          193.56 |
|               2 |    5,424,800.00 |    5,253,205.61 |          190.36 |
|               3 |    5,424,800.00 |    5,316,735.75 |          188.09 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           76.00 |           72.38 |   13,816,252.63 |
|               2 |           76.00 |           73.60 |   13,587,693.42 |
|               3 |           76.00 |           74.49 |   13,425,332.89 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,050,035,200.00 |
|               2 |            0.00 |            0.00 |1,032,664,700.00 |
|               3 |            0.00 |            0.00 |1,020,325,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,050,035,200.00 |
|               2 |            0.00 |            0.00 |1,032,664,700.00 |
|               3 |            0.00 |            0.00 |1,020,325,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,050.00 |          999.97 |    1,000,033.52 |
|               2 |        1,033.00 |        1,000.32 |      999,675.41 |
|               3 |        1,020.00 |          999.68 |    1,000,318.92 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      663,040.00 |      631,445.50 |        1,583.67 |
|               2 |      663,040.00 |      642,067.07 |        1,557.47 |
|               3 |      663,040.00 |      649,831.97 |        1,538.86 |


