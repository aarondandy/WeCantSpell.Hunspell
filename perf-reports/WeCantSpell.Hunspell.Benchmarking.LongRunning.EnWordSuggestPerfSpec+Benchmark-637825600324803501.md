# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/11/2022 1:47:12 AM_
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
|    Elapsed Time |              ms |        1,525.00 |        1,519.67 |        1,516.00 |            4.73 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,181,201.81 |    1,178,726.31 |    1,174,485.73 |        3,689.59 |
|TotalCollections [Gen0] |     collections |           17.15 |           17.11 |           17.05 |            0.05 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.53 |        1,000.12 |          999.81 |            0.37 |
|[Counter] SuggestionQueries |      operations |          131.90 |          131.62 |          131.15 |            0.41 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,791,040.00 |    1,180,491.39 |          847.10 |
|               2 |    1,791,040.00 |    1,174,485.73 |          851.44 |
|               3 |    1,791,040.00 |    1,181,201.81 |          846.60 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           26.00 |           17.14 |   58,353,796.15 |
|               2 |           26.00 |           17.05 |   58,652,184.62 |
|               3 |           26.00 |           17.15 |   58,318,700.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,517,198,700.00 |
|               2 |            0.00 |            0.00 |1,524,956,800.00 |
|               3 |            0.00 |            0.00 |1,516,286,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,517,198,700.00 |
|               2 |            0.00 |            0.00 |1,524,956,800.00 |
|               3 |            0.00 |            0.00 |1,516,286,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,518.00 |        1,000.53 |      999,472.13 |
|               2 |        1,525.00 |        1,000.03 |      999,971.67 |
|               3 |        1,516.00 |          999.81 |    1,000,188.79 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          131.82 |    7,585,993.50 |
|               2 |          200.00 |          131.15 |    7,624,784.00 |
|               3 |          200.00 |          131.90 |    7,581,431.00 |


