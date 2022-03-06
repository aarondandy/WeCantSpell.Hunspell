# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/6/2022 1:15:11 AM_
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
|TotalBytesAllocated |           bytes |    8,163,480.00 |    8,163,480.00 |    8,163,480.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           25.00 |           25.00 |           25.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,550.00 |        1,544.00 |        1,539.00 |            5.57 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,305,455.86 |    5,288,787.36 |    5,269,111.31 |       18,357.98 |
|TotalCollections [Gen0] |     collections |           16.25 |           16.20 |           16.14 |            0.06 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.45 |        1,000.29 |        1,000.20 |            0.14 |
|[Counter] SuggestionQueries |      operations |          129.98 |          129.57 |          129.09 |            0.45 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    8,163,480.00 |    5,269,111.31 |          189.79 |
|               2 |    8,163,480.00 |    5,291,794.89 |          188.97 |
|               3 |    8,163,480.00 |    5,305,455.86 |          188.49 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |           16.14 |   61,972,348.00 |
|               2 |           25.00 |           16.21 |   61,706,700.00 |
|               3 |           25.00 |           16.25 |   61,547,812.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,549,308,700.00 |
|               2 |            0.00 |            0.00 |1,542,667,500.00 |
|               3 |            0.00 |            0.00 |1,538,695,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,549,308,700.00 |
|               2 |            0.00 |            0.00 |1,542,667,500.00 |
|               3 |            0.00 |            0.00 |1,538,695,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,550.00 |        1,000.45 |      999,554.00 |
|               2 |        1,543.00 |        1,000.22 |      999,784.51 |
|               3 |        1,539.00 |        1,000.20 |      999,802.01 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          129.09 |    7,746,543.50 |
|               2 |          200.00 |          129.65 |    7,713,337.50 |
|               3 |          200.00 |          129.98 |    7,693,476.50 |


