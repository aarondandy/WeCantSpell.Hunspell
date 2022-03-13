# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/13/2022 12:16:03 AM_
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
|TotalBytesAllocated |           bytes |    3,046,384.00 |    3,046,384.00 |    3,046,384.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           26.00 |           26.00 |           26.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,247.00 |        1,229.67 |        1,200.00 |           25.81 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,537,780.76 |    2,477,959.51 |    2,442,906.46 |       52,061.32 |
|TotalCollections [Gen0] |     collections |           21.66 |           21.15 |           20.85 |            0.44 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.16 |          999.93 |          999.66 |            0.25 |
|[Counter] SuggestionQueries |      operations |          166.61 |          162.68 |          160.38 |            3.42 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,046,384.00 |    2,442,906.46 |          409.35 |
|               2 |    3,046,384.00 |    2,453,191.30 |          407.63 |
|               3 |    3,046,384.00 |    2,537,780.76 |          394.05 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           26.00 |           20.85 |   47,962,792.31 |
|               2 |           26.00 |           20.94 |   47,761,711.54 |
|               3 |           26.00 |           21.66 |   46,169,715.38 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,247,032,600.00 |
|               2 |            0.00 |            0.00 |1,241,804,500.00 |
|               3 |            0.00 |            0.00 |1,200,412,600.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,247,032,600.00 |
|               2 |            0.00 |            0.00 |1,241,804,500.00 |
|               3 |            0.00 |            0.00 |1,200,412,600.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,247.00 |          999.97 |    1,000,026.14 |
|               2 |        1,242.00 |        1,000.16 |      999,842.59 |
|               3 |        1,200.00 |          999.66 |    1,000,343.83 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          160.38 |    6,235,163.00 |
|               2 |          200.00 |          161.06 |    6,209,022.50 |
|               3 |          200.00 |          166.61 |    6,002,063.00 |


