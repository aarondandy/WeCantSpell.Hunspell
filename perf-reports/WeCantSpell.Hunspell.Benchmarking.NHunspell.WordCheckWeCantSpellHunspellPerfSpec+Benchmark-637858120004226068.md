# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_04/17/2022 17:06:40_
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
|TotalBytesAllocated |           bytes |    2,949,704.00 |    2,949,704.00 |    2,949,704.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,026.00 |        1,023.67 |        1,019.00 |            4.04 |
|[Counter] _wordsChecked |      operations |      679,616.00 |      679,616.00 |      679,616.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,894,385.63 |    2,881,910.04 |    2,875,072.02 |       10,820.84 |
|TotalCollections [Gen0] |     collections |           12.76 |           12.70 |           12.67 |            0.05 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.46 |        1,000.13 |          999.89 |            0.29 |
|[Counter] _wordsChecked |      operations |      666,870.57 |      663,996.18 |      662,420.69 |        2,493.14 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,949,704.00 |    2,876,272.48 |          347.67 |
|               2 |    2,949,704.00 |    2,894,385.63 |          345.50 |
|               3 |    2,949,704.00 |    2,875,072.02 |          347.82 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |           12.68 |   78,886,930.77 |
|               2 |           13.00 |           12.76 |   78,393,253.85 |
|               3 |           13.00 |           12.67 |   78,919,869.23 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,025,530,100.00 |
|               2 |            0.00 |            0.00 |1,019,112,300.00 |
|               3 |            0.00 |            0.00 |1,025,958,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,025,530,100.00 |
|               2 |            0.00 |            0.00 |1,019,112,300.00 |
|               3 |            0.00 |            0.00 |1,025,958,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,026.00 |        1,000.46 |      999,542.01 |
|               2 |        1,019.00 |          999.89 |    1,000,110.21 |
|               3 |        1,026.00 |        1,000.04 |      999,959.36 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      679,616.00 |      662,697.27 |        1,508.98 |
|               2 |      679,616.00 |      666,870.57 |        1,499.54 |
|               3 |      679,616.00 |      662,420.69 |        1,509.61 |


