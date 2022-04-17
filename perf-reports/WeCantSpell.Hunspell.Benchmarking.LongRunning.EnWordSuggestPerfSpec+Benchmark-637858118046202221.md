# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_4/17/2022 5:03:24 PM_
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
|TotalBytesAllocated |           bytes |    3,954,912.00 |    3,954,872.00 |    3,954,792.00 |           69.28 |
|TotalCollections [Gen0] |     collections |            1.00 |            1.00 |            1.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,213.00 |        1,208.33 |        1,205.00 |            4.16 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,281,541.99 |    3,272,218.64 |    3,259,628.65 |       11,316.00 |
|TotalCollections [Gen0] |     collections |            0.83 |            0.83 |            0.82 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          999.86 |          999.75 |          999.65 |            0.11 |
|[Counter] SuggestionQueries |      operations |          165.95 |          165.48 |          164.84 |            0.57 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,954,792.00 |    3,281,541.99 |          304.73 |
|               2 |    3,954,912.00 |    3,259,628.65 |          306.78 |
|               3 |    3,954,912.00 |    3,275,485.29 |          305.30 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            1.00 |            0.83 |1,205,162,700.00 |
|               2 |            1.00 |            0.82 |1,213,301,400.00 |
|               3 |            1.00 |            0.83 |1,207,427,800.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,205,162,700.00 |
|               2 |            0.00 |            0.00 |1,213,301,400.00 |
|               3 |            0.00 |            0.00 |1,207,427,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,205,162,700.00 |
|               2 |            0.00 |            0.00 |1,213,301,400.00 |
|               3 |            0.00 |            0.00 |1,207,427,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,205.00 |          999.86 |    1,000,135.02 |
|               2 |        1,213.00 |          999.75 |    1,000,248.47 |
|               3 |        1,207.00 |          999.65 |    1,000,354.43 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          165.95 |    6,025,813.50 |
|               2 |          200.00 |          164.84 |    6,066,507.00 |
|               3 |          200.00 |          165.64 |    6,037,139.00 |


