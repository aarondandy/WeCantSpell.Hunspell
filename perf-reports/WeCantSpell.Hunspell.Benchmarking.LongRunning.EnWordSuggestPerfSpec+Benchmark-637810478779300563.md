# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_2/21/2022 1:44:37 PM_
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
|    Elapsed Time |              ms |        1,518.00 |        1,517.33 |        1,517.00 |            0.58 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      664,356.29 |      663,986.42 |      663,633.91 |          361.50 |
|TotalCollections [Gen0] |     collections |           16.49 |           16.48 |           16.47 |            0.01 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.35 |        1,000.02 |          999.77 |            0.30 |
|[Counter] SuggestionQueries |      operations |          131.89 |          131.81 |          131.74 |            0.07 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,007,472.00 |      664,356.29 |        1,505.22 |
|               2 |    1,007,472.00 |      663,969.06 |        1,506.09 |
|               3 |    1,007,472.00 |      663,633.91 |        1,506.85 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |           16.49 |   60,658,536.00 |
|               2 |           25.00 |           16.48 |   60,693,912.00 |
|               3 |           25.00 |           16.47 |   60,724,564.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,516,463,400.00 |
|               2 |            0.00 |            0.00 |1,517,347,800.00 |
|               3 |            0.00 |            0.00 |1,518,114,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,516,463,400.00 |
|               2 |            0.00 |            0.00 |1,517,347,800.00 |
|               3 |            0.00 |            0.00 |1,518,114,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,517.00 |        1,000.35 |      999,646.28 |
|               2 |        1,517.00 |          999.77 |    1,000,229.27 |
|               3 |        1,518.00 |          999.92 |    1,000,075.16 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          131.89 |    7,582,317.00 |
|               2 |          200.00 |          131.81 |    7,586,739.00 |
|               3 |          200.00 |          131.74 |    7,590,570.50 |


