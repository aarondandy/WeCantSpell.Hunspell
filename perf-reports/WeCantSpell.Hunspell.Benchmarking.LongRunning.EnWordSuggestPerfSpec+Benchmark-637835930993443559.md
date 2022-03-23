# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/23/2022 12:44:59 AM_
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
|TotalBytesAllocated |           bytes |    3,840,816.00 |    3,840,789.33 |    3,840,776.00 |           23.09 |
|TotalCollections [Gen0] |     collections |            5.00 |            5.00 |            5.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,305.00 |        1,301.00 |        1,296.00 |            4.58 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,961,543.73 |    2,952,035.14 |    2,944,574.22 |        8,668.09 |
|TotalCollections [Gen0] |     collections |            3.86 |            3.84 |            3.83 |            0.01 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.49 |          999.94 |          999.31 |            0.60 |
|[Counter] SuggestionQueries |      operations |          154.21 |          153.72 |          153.33 |            0.45 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,840,816.00 |    2,961,543.73 |          337.66 |
|               2 |    3,840,776.00 |    2,949,987.46 |          338.98 |
|               3 |    3,840,776.00 |    2,944,574.22 |          339.61 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            5.00 |            3.86 |  259,379,320.00 |
|               2 |            5.00 |            3.84 |  260,392,700.00 |
|               3 |            5.00 |            3.83 |  260,871,400.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,296,896,600.00 |
|               2 |            0.00 |            0.00 |1,301,963,500.00 |
|               3 |            0.00 |            0.00 |1,304,357,000.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,296,896,600.00 |
|               2 |            0.00 |            0.00 |1,301,963,500.00 |
|               3 |            0.00 |            0.00 |1,304,357,000.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,296.00 |          999.31 |    1,000,691.82 |
|               2 |        1,302.00 |        1,000.03 |      999,971.97 |
|               3 |        1,305.00 |        1,000.49 |      999,507.28 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          154.21 |    6,484,483.00 |
|               2 |          200.00 |          153.61 |    6,509,817.50 |
|               3 |          200.00 |          153.33 |    6,521,785.00 |


