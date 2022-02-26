# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_2/26/2022 4:56:39 AM_
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
|TotalBytesAllocated |           bytes |    6,822,672.00 |    6,822,672.00 |    6,822,672.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           25.00 |           25.00 |           25.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,541.00 |        1,539.00 |        1,536.00 |            2.65 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,444,185.92 |    4,433,828.36 |    4,427,135.05 |        9,096.87 |
|TotalCollections [Gen0] |     collections |           16.28 |           16.25 |           16.22 |            0.03 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.53 |        1,000.14 |          999.93 |            0.33 |
|[Counter] SuggestionQueries |      operations |          130.28 |          129.97 |          129.78 |            0.27 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    6,822,672.00 |    4,427,135.05 |          225.88 |
|               2 |    6,822,672.00 |    4,444,185.92 |          225.01 |
|               3 |    6,822,672.00 |    4,430,164.10 |          225.73 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |           16.22 |   61,644,128.00 |
|               2 |           25.00 |           16.28 |   61,407,620.00 |
|               3 |           25.00 |           16.23 |   61,601,980.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,541,103,200.00 |
|               2 |            0.00 |            0.00 |1,535,190,500.00 |
|               3 |            0.00 |            0.00 |1,540,049,500.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,541,103,200.00 |
|               2 |            0.00 |            0.00 |1,535,190,500.00 |
|               3 |            0.00 |            0.00 |1,540,049,500.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,541.00 |          999.93 |    1,000,066.97 |
|               2 |        1,536.00 |        1,000.53 |      999,472.98 |
|               3 |        1,540.00 |          999.97 |    1,000,032.14 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          129.78 |    7,705,516.00 |
|               2 |          200.00 |          130.28 |    7,675,952.50 |
|               3 |          200.00 |          129.87 |    7,700,247.50 |


