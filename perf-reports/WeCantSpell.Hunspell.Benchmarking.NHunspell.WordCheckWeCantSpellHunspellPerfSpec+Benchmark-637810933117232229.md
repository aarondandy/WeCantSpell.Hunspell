# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_02/22/2022 02:21:51_
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
|TotalBytesAllocated |           bytes |    3,656,976.00 |    3,656,976.00 |    3,656,976.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           79.00 |           79.00 |           79.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,014.00 |        1,011.33 |        1,010.00 |            2.31 |
|[Counter] _wordsChecked |      operations |      687,904.00 |      687,904.00 |      687,904.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,620,989.52 |    3,615,883.55 |    3,605,927.96 |        8,622.75 |
|TotalCollections [Gen0] |     collections |           78.22 |           78.11 |           77.90 |            0.19 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.06 |          999.97 |          999.85 |            0.11 |
|[Counter] _wordsChecked |      operations |      681,134.68 |      680,174.21 |      678,301.49 |        1,622.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,656,976.00 |    3,605,927.96 |          277.32 |
|               2 |    3,656,976.00 |    3,620,989.52 |          276.17 |
|               3 |    3,656,976.00 |    3,620,733.18 |          276.19 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           79.00 |           77.90 |   12,837,426.58 |
|               2 |           79.00 |           78.22 |   12,784,029.11 |
|               3 |           79.00 |           78.22 |   12,784,934.18 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,014,156,700.00 |
|               2 |            0.00 |            0.00 |1,009,938,300.00 |
|               3 |            0.00 |            0.00 |1,010,009,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,014,156,700.00 |
|               2 |            0.00 |            0.00 |1,009,938,300.00 |
|               3 |            0.00 |            0.00 |1,010,009,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,014.00 |          999.85 |    1,000,154.54 |
|               2 |        1,010.00 |        1,000.06 |      999,938.91 |
|               3 |        1,010.00 |          999.99 |    1,000,009.70 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      687,904.00 |      678,301.49 |        1,474.27 |
|               2 |      687,904.00 |      681,134.68 |        1,468.14 |
|               3 |      687,904.00 |      681,086.46 |        1,468.24 |


