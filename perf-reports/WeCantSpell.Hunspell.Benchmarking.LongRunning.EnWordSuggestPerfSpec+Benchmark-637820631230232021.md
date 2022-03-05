# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/5/2022 7:45:23 AM_
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
|TotalBytesAllocated |           bytes |    8,163,480.00 |    8,160,688.00 |    8,155,104.00 |        4,835.89 |
|TotalCollections [Gen0] |     collections |           25.00 |           25.00 |           25.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,528.00 |        1,525.67 |        1,524.00 |            2.08 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,355,551.85 |    5,349,100.71 |    5,337,421.88 |       10,132.64 |
|TotalCollections [Gen0] |     collections |           16.40 |           16.39 |           16.36 |            0.02 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.23 |        1,000.03 |          999.80 |            0.22 |
|[Counter] SuggestionQueries |      operations |          131.21 |          131.09 |          130.90 |            0.17 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    8,163,480.00 |    5,355,551.85 |          186.72 |
|               2 |    8,163,480.00 |    5,354,328.39 |          186.76 |
|               3 |    8,155,104.00 |    5,337,421.88 |          187.36 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |           16.40 |   60,972,092.00 |
|               2 |           25.00 |           16.40 |   60,986,024.00 |
|               3 |           25.00 |           16.36 |   61,116,428.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,524,302,300.00 |
|               2 |            0.00 |            0.00 |1,524,650,600.00 |
|               3 |            0.00 |            0.00 |1,527,910,700.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,524,302,300.00 |
|               2 |            0.00 |            0.00 |1,524,650,600.00 |
|               3 |            0.00 |            0.00 |1,527,910,700.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,524.00 |          999.80 |    1,000,198.36 |
|               2 |        1,525.00 |        1,000.23 |      999,770.89 |
|               3 |        1,528.00 |        1,000.06 |      999,941.56 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          131.21 |    7,621,511.50 |
|               2 |          200.00 |          131.18 |    7,623,253.00 |
|               3 |          200.00 |          130.90 |    7,639,553.50 |


