# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/12/2022 4:17:28 AM_
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
|TotalBytesAllocated |           bytes |    2,811,664.00 |    2,811,664.00 |    2,811,664.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           26.00 |           26.00 |           26.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,566.00 |        1,563.67 |        1,561.00 |            2.52 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,801,115.99 |    1,798,066.73 |    1,794,990.75 |        3,062.71 |
|TotalCollections [Gen0] |     collections |           16.66 |           16.63 |           16.60 |            0.03 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.20 |          999.97 |          999.75 |            0.22 |
|[Counter] SuggestionQueries |      operations |          128.12 |          127.90 |          127.68 |            0.22 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,811,664.00 |    1,798,093.46 |          556.14 |
|               2 |    2,811,664.00 |    1,801,115.99 |          555.21 |
|               3 |    2,811,664.00 |    1,794,990.75 |          557.11 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           26.00 |           16.63 |   60,141,992.31 |
|               2 |           26.00 |           16.66 |   60,041,065.38 |
|               3 |           26.00 |           16.60 |   60,245,950.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,563,691,800.00 |
|               2 |            0.00 |            0.00 |1,561,067,700.00 |
|               3 |            0.00 |            0.00 |1,566,394,700.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,563,691,800.00 |
|               2 |            0.00 |            0.00 |1,561,067,700.00 |
|               3 |            0.00 |            0.00 |1,566,394,700.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,564.00 |        1,000.20 |      999,802.94 |
|               2 |        1,561.00 |          999.96 |    1,000,043.37 |
|               3 |        1,566.00 |          999.75 |    1,000,252.04 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          127.90 |    7,818,459.00 |
|               2 |          200.00 |          128.12 |    7,805,338.50 |
|               3 |          200.00 |          127.68 |    7,831,973.50 |


