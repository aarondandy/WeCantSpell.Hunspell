# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_2/27/2022 4:20:54 AM_
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
|    Elapsed Time |              ms |        1,577.00 |        1,574.67 |        1,571.00 |            3.21 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,198,110.68 |    5,183,779.04 |    5,175,370.59 |       12,473.62 |
|TotalCollections [Gen0] |     collections |           15.92 |           15.87 |           15.85 |            0.04 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.34 |          999.90 |          999.61 |            0.38 |
|[Counter] SuggestionQueries |      operations |          127.35 |          127.00 |          126.79 |            0.31 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    8,163,480.00 |    5,177,855.84 |          193.13 |
|               2 |    8,163,480.00 |    5,198,110.68 |          192.38 |
|               3 |    8,163,480.00 |    5,175,370.59 |          193.22 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |           15.86 |   63,064,560.00 |
|               2 |           25.00 |           15.92 |   62,818,824.00 |
|               3 |           25.00 |           15.85 |   63,094,844.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,576,614,000.00 |
|               2 |            0.00 |            0.00 |1,570,470,600.00 |
|               3 |            0.00 |            0.00 |1,577,371,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,576,614,000.00 |
|               2 |            0.00 |            0.00 |1,570,470,600.00 |
|               3 |            0.00 |            0.00 |1,577,371,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,576.00 |          999.61 |    1,000,389.59 |
|               2 |        1,571.00 |        1,000.34 |      999,663.02 |
|               3 |        1,577.00 |          999.76 |    1,000,235.32 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          126.85 |    7,883,070.00 |
|               2 |          200.00 |          127.35 |    7,852,353.00 |
|               3 |          200.00 |          126.79 |    7,886,855.50 |


