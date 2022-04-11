# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_4/11/2022 3:09:05 PM_
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
|TotalBytesAllocated |           bytes |    1,442,408.00 |    1,442,338.67 |    1,442,304.00 |           60.04 |
|TotalCollections [Gen0] |     collections |            2.00 |            2.00 |            2.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,246.00 |        1,241.67 |        1,239.00 |            3.79 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,164,221.91 |    1,161,549.29 |    1,157,122.95 |        3,860.76 |
|TotalCollections [Gen0] |     collections |            1.61 |            1.61 |            1.60 |            0.01 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.12 |          999.94 |          999.63 |            0.26 |
|[Counter] SuggestionQueries |      operations |          161.44 |          161.06 |          160.45 |            0.53 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,442,408.00 |    1,163,302.99 |          859.62 |
|               2 |    1,442,304.00 |    1,164,221.91 |          858.94 |
|               3 |    1,442,304.00 |    1,157,122.95 |          864.21 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            2.00 |            1.61 |  619,962,300.00 |
|               2 |            2.00 |            1.61 |  619,428,300.00 |
|               3 |            2.00 |            1.60 |  623,228,500.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,239,924,600.00 |
|               2 |            0.00 |            0.00 |1,238,856,600.00 |
|               3 |            0.00 |            0.00 |1,246,457,000.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,239,924,600.00 |
|               2 |            0.00 |            0.00 |1,238,856,600.00 |
|               3 |            0.00 |            0.00 |1,246,457,000.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,240.00 |        1,000.06 |      999,939.19 |
|               2 |        1,239.00 |        1,000.12 |      999,884.26 |
|               3 |        1,246.00 |          999.63 |    1,000,366.77 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          161.30 |    6,199,623.00 |
|               2 |          200.00 |          161.44 |    6,194,283.00 |
|               3 |          200.00 |          160.45 |    6,232,285.00 |


