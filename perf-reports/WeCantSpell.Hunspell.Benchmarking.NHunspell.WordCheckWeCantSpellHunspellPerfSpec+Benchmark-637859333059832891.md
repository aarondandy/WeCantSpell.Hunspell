# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_04/19/2022 02:48:25_
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
|TotalBytesAllocated |           bytes |    2,884,112.00 |    2,884,112.00 |    2,884,112.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           12.00 |           12.00 |           12.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,017.00 |        1,016.33 |        1,016.00 |            0.58 |
|[Counter] _wordsChecked |      operations |      629,888.00 |      629,888.00 |      629,888.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,840,476.04 |    2,838,622.26 |    2,835,932.90 |        2,384.04 |
|TotalCollections [Gen0] |     collections |           11.82 |           11.81 |           11.80 |            0.01 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.63 |        1,000.30 |        1,000.01 |            0.31 |
|[Counter] _wordsChecked |      operations |      620,357.94 |      619,953.07 |      619,365.72 |          520.67 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,884,112.00 |    2,835,932.90 |          352.62 |
|               2 |    2,884,112.00 |    2,839,457.83 |          352.18 |
|               3 |    2,884,112.00 |    2,840,476.04 |          352.05 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           12.00 |           11.80 |   84,749,066.67 |
|               2 |           12.00 |           11.81 |   84,643,858.33 |
|               3 |           12.00 |           11.82 |   84,613,516.67 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,016,988,800.00 |
|               2 |            0.00 |            0.00 |1,015,726,300.00 |
|               3 |            0.00 |            0.00 |1,015,362,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,016,988,800.00 |
|               2 |            0.00 |            0.00 |1,015,726,300.00 |
|               3 |            0.00 |            0.00 |1,015,362,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,017.00 |        1,000.01 |      999,988.99 |
|               2 |        1,016.00 |        1,000.27 |      999,730.61 |
|               3 |        1,016.00 |        1,000.63 |      999,372.24 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      629,888.00 |      619,365.72 |        1,614.55 |
|               2 |      629,888.00 |      620,135.56 |        1,612.55 |
|               3 |      629,888.00 |      620,357.94 |        1,611.97 |


