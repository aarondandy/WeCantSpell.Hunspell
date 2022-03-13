# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/13/2022 4:20:25 AM_
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
|    Elapsed Time |              ms |        1,216.00 |        1,212.67 |        1,208.00 |            4.16 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,523,258.30 |    2,512,858.26 |    2,505,124.95 |        9,356.19 |
|TotalCollections [Gen0] |     collections |           21.54 |           21.45 |           21.38 |            0.08 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.56 |        1,000.28 |          999.95 |            0.31 |
|[Counter] SuggestionQueries |      operations |          165.66 |          164.97 |          164.47 |            0.61 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,046,384.00 |    2,505,124.95 |          399.18 |
|               2 |    3,046,384.00 |    2,510,191.53 |          398.38 |
|               3 |    3,046,384.00 |    2,523,258.30 |          396.31 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           26.00 |           21.38 |   46,771,565.38 |
|               2 |           26.00 |           21.42 |   46,677,161.54 |
|               3 |           26.00 |           21.54 |   46,435,442.31 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,216,060,700.00 |
|               2 |            0.00 |            0.00 |1,213,606,200.00 |
|               3 |            0.00 |            0.00 |1,207,321,500.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,216,060,700.00 |
|               2 |            0.00 |            0.00 |1,213,606,200.00 |
|               3 |            0.00 |            0.00 |1,207,321,500.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,216.00 |          999.95 |    1,000,049.92 |
|               2 |        1,214.00 |        1,000.32 |      999,675.62 |
|               3 |        1,208.00 |        1,000.56 |      999,438.33 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          164.47 |    6,080,303.50 |
|               2 |          200.00 |          164.80 |    6,068,031.00 |
|               3 |          200.00 |          165.66 |    6,036,607.50 |


