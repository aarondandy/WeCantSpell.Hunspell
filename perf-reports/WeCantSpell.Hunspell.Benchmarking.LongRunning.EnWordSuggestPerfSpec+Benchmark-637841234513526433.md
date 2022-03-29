# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/29/2022 4:04:11 AM_
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
|TotalBytesAllocated |           bytes |    5,604,152.00 |    5,604,130.67 |    5,604,120.00 |           18.48 |
|TotalCollections [Gen0] |     collections |            2.00 |            2.00 |            2.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,258.00 |        1,243.33 |        1,231.00 |           13.65 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,552,646.76 |    4,508,282.91 |    4,455,238.69 |       49,280.77 |
|TotalCollections [Gen0] |     collections |            1.62 |            1.61 |            1.59 |            0.02 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.26 |        1,000.13 |        1,000.03 |            0.12 |
|[Counter] SuggestionQueries |      operations |          162.47 |          160.89 |          159.00 |            1.76 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    5,604,152.00 |    4,552,646.76 |          219.65 |
|               2 |    5,604,120.00 |    4,455,238.69 |          224.45 |
|               3 |    5,604,120.00 |    4,516,963.28 |          221.39 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            2.00 |            1.62 |  615,482,850.00 |
|               2 |            2.00 |            1.59 |  628,936,000.00 |
|               3 |            2.00 |            1.61 |  620,341,550.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,230,965,700.00 |
|               2 |            0.00 |            0.00 |1,257,872,000.00 |
|               3 |            0.00 |            0.00 |1,240,683,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,230,965,700.00 |
|               2 |            0.00 |            0.00 |1,257,872,000.00 |
|               3 |            0.00 |            0.00 |1,240,683,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,231.00 |        1,000.03 |      999,972.14 |
|               2 |        1,258.00 |        1,000.10 |      999,898.25 |
|               3 |        1,241.00 |        1,000.26 |      999,744.64 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          162.47 |    6,154,828.50 |
|               2 |          200.00 |          159.00 |    6,289,360.00 |
|               3 |          200.00 |          161.20 |    6,203,415.50 |


