# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_2/28/2022 1:03:08 AM_
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
|TotalBytesAllocated |           bytes |    8,163,480.00 |    8,163,418.67 |    8,163,296.00 |          106.23 |
|TotalCollections [Gen0] |     collections |           25.00 |           25.00 |           25.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,554.00 |        1,551.67 |        1,549.00 |            2.52 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,270,323.35 |    5,260,892.94 |    5,253,302.24 |        8,658.40 |
|TotalCollections [Gen0] |     collections |           16.14 |           16.11 |           16.09 |            0.03 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.04 |          999.97 |          999.82 |            0.12 |
|[Counter] SuggestionQueries |      operations |          129.12 |          128.89 |          128.71 |            0.21 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    8,163,480.00 |    5,270,323.35 |          189.74 |
|               2 |    8,163,296.00 |    5,253,302.24 |          190.36 |
|               3 |    8,163,480.00 |    5,259,053.21 |          190.15 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |           16.14 |   61,958,096.00 |
|               2 |           25.00 |           16.09 |   62,157,444.00 |
|               3 |           25.00 |           16.11 |   62,090,872.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,548,952,400.00 |
|               2 |            0.00 |            0.00 |1,553,936,100.00 |
|               3 |            0.00 |            0.00 |1,552,271,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,548,952,400.00 |
|               2 |            0.00 |            0.00 |1,553,936,100.00 |
|               3 |            0.00 |            0.00 |1,552,271,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,549.00 |        1,000.03 |      999,969.27 |
|               2 |        1,554.00 |        1,000.04 |      999,958.88 |
|               3 |        1,552.00 |          999.82 |    1,000,175.13 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          129.12 |    7,744,762.00 |
|               2 |          200.00 |          128.71 |    7,769,680.50 |
|               3 |          200.00 |          128.84 |    7,761,359.00 |


