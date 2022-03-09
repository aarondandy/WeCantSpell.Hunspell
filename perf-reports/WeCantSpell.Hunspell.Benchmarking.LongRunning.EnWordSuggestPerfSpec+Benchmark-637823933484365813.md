# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/9/2022 3:29:08 AM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.3,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |    8,162,968.00 |    8,160,202.67 |    8,154,872.00 |        4,617.58 |
|TotalCollections [Gen0] |     collections |           25.00 |           25.00 |           25.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,541.00 |        1,539.00 |        1,537.00 |            2.00 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,311,404.86 |    5,303,400.38 |    5,298,119.36 |        7,049.04 |
|TotalCollections [Gen0] |     collections |           16.27 |           16.25 |           16.23 |            0.02 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.35 |        1,000.21 |        1,000.08 |            0.14 |
|[Counter] SuggestionQueries |      operations |          130.13 |          129.98 |          129.81 |            0.16 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    8,162,968.00 |    5,311,404.86 |          188.27 |
|               2 |    8,162,768.00 |    5,298,119.36 |          188.75 |
|               3 |    8,154,872.00 |    5,300,676.92 |          188.66 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |           16.27 |   61,475,020.00 |
|               2 |           25.00 |           16.23 |   61,627,664.00 |
|               3 |           25.00 |           16.25 |   61,538,344.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,536,875,500.00 |
|               2 |            0.00 |            0.00 |1,540,691,600.00 |
|               3 |            0.00 |            0.00 |1,538,458,600.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,536,875,500.00 |
|               2 |            0.00 |            0.00 |1,540,691,600.00 |
|               3 |            0.00 |            0.00 |1,538,458,600.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,537.00 |        1,000.08 |      999,919.00 |
|               2 |        1,541.00 |        1,000.20 |      999,799.87 |
|               3 |        1,539.00 |        1,000.35 |      999,648.21 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          130.13 |    7,684,377.50 |
|               2 |          200.00 |          129.81 |    7,703,458.00 |
|               3 |          200.00 |          130.00 |    7,692,293.00 |


