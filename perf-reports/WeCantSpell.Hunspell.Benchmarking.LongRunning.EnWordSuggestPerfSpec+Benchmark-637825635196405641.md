# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/11/2022 2:45:19 AM_
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
|TotalBytesAllocated |           bytes |    1,791,040.00 |    1,791,040.00 |    1,791,040.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           26.00 |           26.00 |           26.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,552.00 |        1,548.33 |        1,544.00 |            4.04 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,160,019.31 |    1,156,897.88 |    1,153,973.85 |        3,027.56 |
|TotalCollections [Gen0] |     collections |           16.84 |           16.79 |           16.75 |            0.04 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.38 |        1,000.12 |          999.96 |            0.23 |
|[Counter] SuggestionQueries |      operations |          129.54 |          129.19 |          128.86 |            0.34 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,791,040.00 |    1,160,019.31 |          862.05 |
|               2 |    1,791,040.00 |    1,156,700.48 |          864.53 |
|               3 |    1,791,040.00 |    1,153,973.85 |          866.57 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           26.00 |           16.84 |   59,383,626.92 |
|               2 |           26.00 |           16.79 |   59,554,011.54 |
|               3 |           26.00 |           16.75 |   59,694,726.92 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,543,974,300.00 |
|               2 |            0.00 |            0.00 |1,548,404,300.00 |
|               3 |            0.00 |            0.00 |1,552,062,900.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,543,974,300.00 |
|               2 |            0.00 |            0.00 |1,548,404,300.00 |
|               3 |            0.00 |            0.00 |1,552,062,900.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,544.00 |        1,000.02 |      999,983.35 |
|               2 |        1,549.00 |        1,000.38 |      999,615.43 |
|               3 |        1,552.00 |          999.96 |    1,000,040.53 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          129.54 |    7,719,871.50 |
|               2 |          200.00 |          129.17 |    7,742,021.50 |
|               3 |          200.00 |          128.86 |    7,760,314.50 |


