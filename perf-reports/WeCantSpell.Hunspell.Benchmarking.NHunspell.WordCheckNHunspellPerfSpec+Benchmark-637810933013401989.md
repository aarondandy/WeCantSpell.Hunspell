# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_02/22/2022 02:21:41_
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
|    Elapsed Time |              ms |        1,008.00 |        1,007.67 |        1,007.00 |            0.58 |
|[Counter] _wordsChecked |      operations |    1,326,080.00 |    1,326,080.00 |    1,326,080.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.66 |        1,000.24 |          999.86 |            0.40 |
|[Counter] _wordsChecked |      operations |    1,316,677.34 |    1,316,305.22 |    1,315,813.50 |          444.17 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,007,141,200.00 |
|               2 |            0.00 |            0.00 |1,007,802,400.00 |
|               3 |            0.00 |            0.00 |1,007,334,400.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,007,141,200.00 |
|               2 |            0.00 |            0.00 |1,007,802,400.00 |
|               3 |            0.00 |            0.00 |1,007,334,400.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,007,141,200.00 |
|               2 |            0.00 |            0.00 |1,007,802,400.00 |
|               3 |            0.00 |            0.00 |1,007,334,400.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,007,141,200.00 |
|               2 |            0.00 |            0.00 |1,007,802,400.00 |
|               3 |            0.00 |            0.00 |1,007,334,400.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,007.00 |          999.86 |    1,000,140.22 |
|               2 |        1,008.00 |        1,000.20 |      999,803.97 |
|               3 |        1,008.00 |        1,000.66 |      999,339.68 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,326,080.00 |    1,316,677.34 |          759.49 |
|               2 |    1,326,080.00 |    1,315,813.50 |          759.99 |
|               3 |    1,326,080.00 |    1,316,424.81 |          759.63 |


