# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_4/13/2022 10:58:19 PM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.4,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |    1,414,472.00 |    1,414,472.00 |    1,414,472.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            2.00 |            2.00 |            2.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,281.00 |        1,271.67 |        1,266.00 |            8.14 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,117,388.55 |    1,112,039.65 |    1,104,004.17 |        7,085.11 |
|TotalCollections [Gen0] |     collections |            1.58 |            1.57 |            1.56 |            0.01 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.10 |          999.74 |          999.29 |            0.41 |
|[Counter] SuggestionQueries |      operations |          157.99 |          157.24 |          156.10 |            1.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,414,472.00 |    1,114,726.24 |          897.08 |
|               2 |    1,414,472.00 |    1,104,004.17 |          905.79 |
|               3 |    1,414,472.00 |    1,117,388.55 |          894.94 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            2.00 |            1.58 |  634,448,150.00 |
|               2 |            2.00 |            1.56 |  640,609,900.00 |
|               3 |            2.00 |            1.58 |  632,936,500.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,268,896,300.00 |
|               2 |            0.00 |            0.00 |1,281,219,800.00 |
|               3 |            0.00 |            0.00 |1,265,873,000.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,268,896,300.00 |
|               2 |            0.00 |            0.00 |1,281,219,800.00 |
|               3 |            0.00 |            0.00 |1,265,873,000.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,268.00 |          999.29 |    1,000,706.86 |
|               2 |        1,281.00 |          999.83 |    1,000,171.58 |
|               3 |        1,266.00 |        1,000.10 |      999,899.68 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          157.62 |    6,344,481.50 |
|               2 |          200.00 |          156.10 |    6,406,099.00 |
|               3 |          200.00 |          157.99 |    6,329,365.00 |


