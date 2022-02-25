# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_02/25/2022 03:56:58_
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
|TotalBytesAllocated |           bytes |    3,656,800.00 |    3,656,800.00 |    3,656,800.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           79.00 |           79.00 |           79.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,010.00 |        1,007.67 |        1,006.00 |            2.08 |
|[Counter] _wordsChecked |      operations |      687,904.00 |      687,904.00 |      687,904.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,633,165.17 |    3,629,447.95 |    3,622,306.23 |        6,186.64 |
|TotalCollections [Gen0] |     collections |           78.49 |           78.41 |           78.25 |            0.13 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.49 |        1,000.13 |          999.42 |            0.61 |
|[Counter] _wordsChecked |      operations |      683,457.90 |      682,758.63 |      681,415.16 |        1,163.81 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,656,800.00 |    3,633,165.17 |          275.24 |
|               2 |    3,656,800.00 |    3,622,306.23 |          276.07 |
|               3 |    3,656,800.00 |    3,632,872.45 |          275.26 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           79.00 |           78.49 |   12,740,573.42 |
|               2 |           79.00 |           78.25 |   12,778,767.09 |
|               3 |           79.00 |           78.48 |   12,741,600.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,006,505,300.00 |
|               2 |            0.00 |            0.00 |1,009,522,600.00 |
|               3 |            0.00 |            0.00 |1,006,586,400.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,006,505,300.00 |
|               2 |            0.00 |            0.00 |1,009,522,600.00 |
|               3 |            0.00 |            0.00 |1,006,586,400.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,007.00 |        1,000.49 |      999,508.74 |
|               2 |        1,010.00 |        1,000.47 |      999,527.33 |
|               3 |        1,006.00 |          999.42 |    1,000,582.90 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      687,904.00 |      683,457.90 |        1,463.15 |
|               2 |      687,904.00 |      681,415.16 |        1,467.53 |
|               3 |      687,904.00 |      683,402.84 |        1,463.27 |


