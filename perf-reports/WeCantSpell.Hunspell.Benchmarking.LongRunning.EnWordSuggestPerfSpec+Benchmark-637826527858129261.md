# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/12/2022 3:33:05 AM_
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
|TotalBytesAllocated |           bytes |    2,766,344.00 |    2,766,109.33 |    2,765,992.00 |          203.23 |
|TotalCollections [Gen0] |     collections |           26.00 |           26.00 |           26.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,586.00 |        1,577.67 |        1,572.00 |            7.37 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,759,448.32 |    1,752,836.03 |    1,743,171.27 |        8,557.09 |
|TotalCollections [Gen0] |     collections |           16.54 |           16.48 |           16.39 |            0.08 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          999.83 |          999.73 |          999.52 |            0.18 |
|[Counter] SuggestionQueries |      operations |          127.20 |          126.74 |          126.04 |            0.61 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,765,992.00 |    1,743,171.27 |          573.67 |
|               2 |    2,765,992.00 |    1,755,888.51 |          569.51 |
|               3 |    2,766,344.00 |    1,759,448.32 |          568.36 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           26.00 |           16.39 |   61,029,176.92 |
|               2 |           26.00 |           16.51 |   60,587,165.38 |
|               3 |           26.00 |           16.54 |   60,472,276.92 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,586,758,600.00 |
|               2 |            0.00 |            0.00 |1,575,266,300.00 |
|               3 |            0.00 |            0.00 |1,572,279,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,586,758,600.00 |
|               2 |            0.00 |            0.00 |1,575,266,300.00 |
|               3 |            0.00 |            0.00 |1,572,279,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,586.00 |          999.52 |    1,000,478.31 |
|               2 |        1,575.00 |          999.83 |    1,000,169.08 |
|               3 |        1,572.00 |          999.82 |    1,000,177.61 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          126.04 |    7,933,793.00 |
|               2 |          200.00 |          126.96 |    7,876,331.50 |
|               3 |          200.00 |          127.20 |    7,861,396.00 |


