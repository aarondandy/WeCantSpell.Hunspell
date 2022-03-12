# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/12/2022 2:05:13 AM_
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
|TotalBytesAllocated |           bytes |    6,231,392.00 |    6,231,312.00 |    6,231,272.00 |           69.28 |
|TotalCollections [Gen0] |     collections |           26.00 |           26.00 |           26.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,625.00 |        1,593.00 |        1,555.00 |           35.38 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,006,307.08 |    3,913,103.90 |    3,835,505.31 |       86,463.51 |
|TotalCollections [Gen0] |     collections |           16.72 |           16.33 |           16.00 |            0.36 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.23 |        1,000.04 |          999.75 |            0.26 |
|[Counter] SuggestionQueries |      operations |          128.58 |          125.59 |          123.11 |            2.77 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    6,231,272.00 |    3,835,505.31 |          260.72 |
|               2 |    6,231,272.00 |    3,897,499.30 |          256.57 |
|               3 |    6,231,392.00 |    4,006,307.08 |          249.61 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           26.00 |           16.00 |   62,485,719.23 |
|               2 |           26.00 |           16.26 |   61,491,815.38 |
|               3 |           26.00 |           16.72 |   59,822,903.85 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,624,628,700.00 |
|               2 |            0.00 |            0.00 |1,598,787,200.00 |
|               3 |            0.00 |            0.00 |1,555,395,500.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,624,628,700.00 |
|               2 |            0.00 |            0.00 |1,598,787,200.00 |
|               3 |            0.00 |            0.00 |1,555,395,500.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,625.00 |        1,000.23 |      999,771.51 |
|               2 |        1,599.00 |        1,000.13 |      999,866.92 |
|               3 |        1,555.00 |          999.75 |    1,000,254.34 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          123.11 |    8,123,143.50 |
|               2 |          200.00 |          125.09 |    7,993,936.00 |
|               3 |          200.00 |          128.58 |    7,776,977.50 |


