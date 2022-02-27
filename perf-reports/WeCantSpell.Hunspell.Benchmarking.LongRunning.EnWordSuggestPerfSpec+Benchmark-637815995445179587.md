# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_2/27/2022 10:59:04 PM_
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
|    Elapsed Time |              ms |        1,572.00 |        1,541.67 |        1,519.00 |           27.32 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,374,635.78 |    5,297,178.02 |    5,194,598.27 |       92,610.54 |
|TotalCollections [Gen0] |     collections |           16.46 |           16.22 |           15.91 |            0.28 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.30 |        1,000.16 |        1,000.07 |            0.12 |
|[Counter] SuggestionQueries |      operations |          131.68 |          129.78 |          127.26 |            2.27 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    8,163,480.00 |    5,322,300.01 |          187.89 |
|               2 |    8,163,480.00 |    5,194,598.27 |          192.51 |
|               3 |    8,163,480.00 |    5,374,635.78 |          186.06 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |           16.30 |   61,353,024.00 |
|               2 |           25.00 |           15.91 |   62,861,300.00 |
|               3 |           25.00 |           16.46 |   60,755,596.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,533,825,600.00 |
|               2 |            0.00 |            0.00 |1,571,532,500.00 |
|               3 |            0.00 |            0.00 |1,518,889,900.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,533,825,600.00 |
|               2 |            0.00 |            0.00 |1,571,532,500.00 |
|               3 |            0.00 |            0.00 |1,518,889,900.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,534.00 |        1,000.11 |      999,886.31 |
|               2 |        1,572.00 |        1,000.30 |      999,702.61 |
|               3 |        1,519.00 |        1,000.07 |      999,927.52 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          130.39 |    7,669,128.00 |
|               2 |          200.00 |          127.26 |    7,857,662.50 |
|               3 |          200.00 |          131.68 |    7,594,449.50 |


