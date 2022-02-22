# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_2/22/2022 2:17:51 AM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.2,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |    1,007,472.00 |    1,007,472.00 |    1,007,472.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           25.00 |           25.00 |           25.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,520.00 |        1,517.00 |        1,515.00 |            2.65 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      664,832.10 |      664,201.92 |      663,025.40 |        1,019.76 |
|TotalCollections [Gen0] |     collections |           16.50 |           16.48 |           16.45 |            0.03 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.32 |        1,000.12 |          999.75 |            0.32 |
|[Counter] SuggestionQueries |      operations |          131.98 |          131.86 |          131.62 |            0.20 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,007,472.00 |      663,025.40 |        1,508.24 |
|               2 |    1,007,472.00 |      664,748.27 |        1,504.33 |
|               3 |    1,007,472.00 |      664,832.10 |        1,504.14 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |           16.45 |   60,780,296.00 |
|               2 |           25.00 |           16.50 |   60,622,768.00 |
|               3 |           25.00 |           16.50 |   60,615,124.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,519,507,400.00 |
|               2 |            0.00 |            0.00 |1,515,569,200.00 |
|               3 |            0.00 |            0.00 |1,515,378,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,519,507,400.00 |
|               2 |            0.00 |            0.00 |1,515,569,200.00 |
|               3 |            0.00 |            0.00 |1,515,378,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,520.00 |        1,000.32 |      999,675.92 |
|               2 |        1,516.00 |        1,000.28 |      999,715.83 |
|               3 |        1,515.00 |          999.75 |    1,000,249.57 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          131.62 |    7,597,537.00 |
|               2 |          200.00 |          131.96 |    7,577,846.00 |
|               3 |          200.00 |          131.98 |    7,576,890.50 |


