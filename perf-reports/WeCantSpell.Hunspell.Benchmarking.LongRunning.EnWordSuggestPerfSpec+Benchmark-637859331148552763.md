# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_4/19/2022 2:45:14 AM_
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
|TotalBytesAllocated |           bytes |    3,831,912.00 |    3,831,816.00 |    3,831,768.00 |           83.14 |
|TotalCollections [Gen0] |     collections |            1.00 |            1.00 |            1.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,184.00 |        1,182.33 |        1,181.00 |            1.53 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,246,661.08 |    3,242,340.76 |    3,237,558.77 |        4,568.69 |
|TotalCollections [Gen0] |     collections |            0.85 |            0.85 |            0.84 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.62 |        1,000.45 |        1,000.32 |            0.16 |
|[Counter] SuggestionQueries |      operations |          169.45 |          169.23 |          168.99 |            0.24 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,831,912.00 |    3,246,661.08 |          308.01 |
|               2 |    3,831,768.00 |    3,242,802.44 |          308.38 |
|               3 |    3,831,768.00 |    3,237,558.77 |          308.87 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            1.00 |            0.85 |1,180,262,400.00 |
|               2 |            1.00 |            0.85 |1,181,622,400.00 |
|               3 |            1.00 |            0.84 |1,183,536,200.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,180,262,400.00 |
|               2 |            0.00 |            0.00 |1,181,622,400.00 |
|               3 |            0.00 |            0.00 |1,183,536,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,180,262,400.00 |
|               2 |            0.00 |            0.00 |1,181,622,400.00 |
|               3 |            0.00 |            0.00 |1,183,536,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,181.00 |        1,000.62 |      999,375.44 |
|               2 |        1,182.00 |        1,000.32 |      999,680.54 |
|               3 |        1,184.00 |        1,000.39 |      999,608.28 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          169.45 |    5,901,312.00 |
|               2 |          200.00 |          169.26 |    5,908,112.00 |
|               3 |          200.00 |          168.99 |    5,917,681.00 |


