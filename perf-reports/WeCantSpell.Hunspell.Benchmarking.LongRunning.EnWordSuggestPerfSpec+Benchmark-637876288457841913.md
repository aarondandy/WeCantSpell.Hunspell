# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_5/8/2022 5:47:25 PM_
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
|TotalBytesAllocated |           bytes |    2,831,824.00 |    2,831,824.00 |    2,831,824.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            1.00 |            1.00 |            1.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,193.00 |        1,168.00 |        1,150.00 |           22.34 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,462,958.95 |    2,424,651.18 |    2,371,952.66 |       47,178.98 |
|TotalCollections [Gen0] |     collections |            0.87 |            0.86 |            0.84 |            0.02 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.20 |          999.81 |          999.26 |            0.49 |
|[Counter] SuggestionQueries |      operations |          173.95 |          171.24 |          167.52 |            3.33 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,831,824.00 |    2,462,958.95 |          406.02 |
|               2 |    2,831,824.00 |    2,439,041.91 |          410.00 |
|               3 |    2,831,824.00 |    2,371,952.66 |          421.59 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            1.00 |            0.87 |1,149,765,000.00 |
|               2 |            1.00 |            0.86 |1,161,039,500.00 |
|               3 |            1.00 |            0.84 |1,193,878,800.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,149,765,000.00 |
|               2 |            0.00 |            0.00 |1,161,039,500.00 |
|               3 |            0.00 |            0.00 |1,193,878,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,149,765,000.00 |
|               2 |            0.00 |            0.00 |1,161,039,500.00 |
|               3 |            0.00 |            0.00 |1,193,878,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,150.00 |        1,000.20 |      999,795.65 |
|               2 |        1,161.00 |          999.97 |    1,000,034.02 |
|               3 |        1,193.00 |          999.26 |    1,000,736.63 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          173.95 |    5,748,825.00 |
|               2 |          200.00 |          172.26 |    5,805,197.50 |
|               3 |          200.00 |          167.52 |    5,969,394.00 |


