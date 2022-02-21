# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_02/21/2022 18:38:33_
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
|TotalBytesAllocated |           bytes |    3,419,768.00 |    3,419,768.00 |    3,419,768.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           80.00 |           80.00 |           80.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,009.00 |        1,007.67 |        1,007.00 |            1.15 |
|[Counter] _wordsChecked |      operations |      696,192.00 |      696,192.00 |      696,192.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,398,203.00 |    3,394,526.10 |    3,389,442.99 |        4,546.15 |
|TotalCollections [Gen0] |     collections |           79.50 |           79.41 |           79.29 |            0.11 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.65 |        1,000.23 |          999.98 |            0.37 |
|[Counter] _wordsChecked |      operations |      691,801.83 |      691,053.29 |      690,018.47 |          925.50 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,419,768.00 |    3,395,932.29 |          294.47 |
|               2 |    3,419,768.00 |    3,389,442.99 |          295.03 |
|               3 |    3,419,768.00 |    3,398,203.00 |          294.27 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           80.00 |           79.44 |   12,587,736.25 |
|               2 |           80.00 |           79.29 |   12,611,836.25 |
|               3 |           80.00 |           79.50 |   12,579,325.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,007,018,900.00 |
|               2 |            0.00 |            0.00 |1,008,946,900.00 |
|               3 |            0.00 |            0.00 |1,006,346,000.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,007,018,900.00 |
|               2 |            0.00 |            0.00 |1,008,946,900.00 |
|               3 |            0.00 |            0.00 |1,006,346,000.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,007.00 |          999.98 |    1,000,018.77 |
|               2 |        1,009.00 |        1,000.05 |      999,947.37 |
|               3 |        1,007.00 |        1,000.65 |      999,350.55 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      696,192.00 |      691,339.56 |        1,446.47 |
|               2 |      696,192.00 |      690,018.47 |        1,449.24 |
|               3 |      696,192.00 |      691,801.83 |        1,445.50 |


