# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_2/21/2022 6:34:44 PM_
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
|TotalBytesAllocated |           bytes |    1,007,472.00 |    1,007,450.67 |    1,007,408.00 |           36.95 |
|TotalCollections [Gen0] |     collections |           25.00 |           25.00 |           25.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,540.00 |        1,538.67 |        1,538.00 |            1.15 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      655,026.27 |      654,836.31 |      654,468.55 |          318.54 |
|TotalCollections [Gen0] |     collections |           16.25 |           16.25 |           16.24 |            0.01 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.47 |        1,000.12 |          999.94 |            0.30 |
|[Counter] SuggestionQueries |      operations |          130.03 |          130.00 |          129.93 |            0.06 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,007,472.00 |      655,026.27 |        1,526.66 |
|               2 |    1,007,408.00 |      654,468.55 |        1,527.96 |
|               3 |    1,007,472.00 |      655,014.09 |        1,526.68 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |           16.25 |   61,522,540.00 |
|               2 |           25.00 |           16.24 |   61,571,056.00 |
|               3 |           25.00 |           16.25 |   61,523,684.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,538,063,500.00 |
|               2 |            0.00 |            0.00 |1,539,276,400.00 |
|               3 |            0.00 |            0.00 |1,538,092,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,538,063,500.00 |
|               2 |            0.00 |            0.00 |1,539,276,400.00 |
|               3 |            0.00 |            0.00 |1,538,092,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,538.00 |          999.96 |    1,000,041.29 |
|               2 |        1,540.00 |        1,000.47 |      999,530.13 |
|               3 |        1,538.00 |          999.94 |    1,000,059.88 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          130.03 |    7,690,317.50 |
|               2 |          200.00 |          129.93 |    7,696,382.00 |
|               3 |          200.00 |          130.03 |    7,690,460.50 |


