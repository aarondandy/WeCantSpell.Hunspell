# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/13/2022 10:15:37 PM_
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
|TotalBytesAllocated |           bytes |    2,046,760.00 |    2,041,240.00 |    2,038,480.00 |        4,780.46 |
|TotalCollections [Gen0] |     collections |            5.00 |            5.00 |            5.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,101.00 |        1,094.00 |        1,082.00 |           10.44 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,882,738.90 |    1,866,230.56 |    1,855,681.63 |       14,479.63 |
|TotalCollections [Gen0] |     collections |            4.62 |            4.57 |            4.54 |            0.04 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.68 |        1,000.16 |          999.33 |            0.72 |
|[Counter] SuggestionQueries |      operations |          184.72 |          182.85 |          181.78 |            1.62 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,038,480.00 |    1,855,681.63 |          538.89 |
|               2 |    2,046,760.00 |    1,860,271.16 |          537.56 |
|               3 |    2,038,480.00 |    1,882,738.90 |          531.14 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            5.00 |            4.55 |  219,701,480.00 |
|               2 |            5.00 |            4.54 |  220,049,640.00 |
|               3 |            5.00 |            4.62 |  216,544,100.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,098,507,400.00 |
|               2 |            0.00 |            0.00 |1,100,248,200.00 |
|               3 |            0.00 |            0.00 |1,082,720,500.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,098,507,400.00 |
|               2 |            0.00 |            0.00 |1,100,248,200.00 |
|               3 |            0.00 |            0.00 |1,082,720,500.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,099.00 |        1,000.45 |      999,551.77 |
|               2 |        1,101.00 |        1,000.68 |      999,317.17 |
|               3 |        1,082.00 |          999.33 |    1,000,665.90 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          182.07 |    5,492,537.00 |
|               2 |          200.00 |          181.78 |    5,501,241.00 |
|               3 |          200.00 |          184.72 |    5,413,602.50 |


