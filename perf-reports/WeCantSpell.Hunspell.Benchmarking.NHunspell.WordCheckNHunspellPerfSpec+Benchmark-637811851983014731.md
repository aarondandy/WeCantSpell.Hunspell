# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_02/23/2022 03:53:18_
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
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,002.00 |        1,000.00 |          998.00 |            2.00 |
|[Counter] _wordsChecked |      operations |    1,317,792.00 |    1,317,792.00 |    1,317,792.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.66 |          999.94 |          999.58 |            0.62 |
|[Counter] _wordsChecked |      operations |    1,319,894.59 |    1,317,719.40 |    1,316,025.50 |        1,978.94 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,001,342,300.00 |
|               2 |            0.00 |            0.00 |1,000,420,500.00 |
|               3 |            0.00 |            0.00 |  998,407,000.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,001,342,300.00 |
|               2 |            0.00 |            0.00 |1,000,420,500.00 |
|               3 |            0.00 |            0.00 |  998,407,000.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,001,342,300.00 |
|               2 |            0.00 |            0.00 |1,000,420,500.00 |
|               3 |            0.00 |            0.00 |  998,407,000.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,001,342,300.00 |
|               2 |            0.00 |            0.00 |1,000,420,500.00 |
|               3 |            0.00 |            0.00 |  998,407,000.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,002.00 |        1,000.66 |      999,343.61 |
|               2 |        1,000.00 |          999.58 |    1,000,420.50 |
|               3 |          998.00 |          999.59 |    1,000,407.82 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,317,792.00 |    1,316,025.50 |          759.86 |
|               2 |    1,317,792.00 |    1,317,238.10 |          759.16 |
|               3 |    1,317,792.00 |    1,319,894.59 |          757.64 |


