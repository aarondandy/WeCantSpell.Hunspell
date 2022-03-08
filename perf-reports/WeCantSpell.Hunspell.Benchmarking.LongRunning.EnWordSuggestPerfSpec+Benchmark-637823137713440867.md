# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/8/2022 5:22:51 AM_
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
|TotalBytesAllocated |           bytes |    8,162,768.00 |    8,162,768.00 |    8,162,768.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           25.00 |           25.00 |           25.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,517.00 |        1,515.67 |        1,514.00 |            1.53 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,388,974.82 |    5,384,467.56 |    5,379,896.90 |        4,539.29 |
|TotalCollections [Gen0] |     collections |           16.50 |           16.49 |           16.48 |            0.01 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.02 |          999.79 |          999.53 |            0.25 |
|[Counter] SuggestionQueries |      operations |          132.04 |          131.93 |          131.82 |            0.11 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    8,162,768.00 |    5,379,896.90 |          185.88 |
|               2 |    8,162,768.00 |    5,384,530.95 |          185.72 |
|               3 |    8,162,768.00 |    5,388,974.82 |          185.56 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |           16.48 |   60,690,888.00 |
|               2 |           25.00 |           16.49 |   60,638,656.00 |
|               3 |           25.00 |           16.50 |   60,588,652.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,517,272,200.00 |
|               2 |            0.00 |            0.00 |1,515,966,400.00 |
|               3 |            0.00 |            0.00 |1,514,716,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,517,272,200.00 |
|               2 |            0.00 |            0.00 |1,515,966,400.00 |
|               3 |            0.00 |            0.00 |1,514,716,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,517.00 |          999.82 |    1,000,179.43 |
|               2 |        1,516.00 |        1,000.02 |      999,977.84 |
|               3 |        1,514.00 |          999.53 |    1,000,473.12 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          131.82 |    7,586,361.00 |
|               2 |          200.00 |          131.93 |    7,579,832.00 |
|               3 |          200.00 |          132.04 |    7,573,581.50 |


