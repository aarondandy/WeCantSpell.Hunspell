# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/9/2022 3:58:59 AM_
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
|TotalBytesAllocated |           bytes |    1,791,040.00 |    1,791,040.00 |    1,791,040.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           26.00 |           26.00 |           26.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,599.00 |        1,592.67 |        1,586.00 |            6.51 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,129,305.21 |    1,124,738.67 |    1,120,417.76 |        4,448.81 |
|TotalCollections [Gen0] |     collections |           16.39 |           16.33 |           16.26 |            0.06 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.28 |        1,000.15 |        1,000.02 |            0.13 |
|[Counter] SuggestionQueries |      operations |          126.11 |          125.60 |          125.11 |            0.50 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,791,040.00 |    1,124,493.04 |          889.29 |
|               2 |    1,791,040.00 |    1,120,417.76 |          892.52 |
|               3 |    1,791,040.00 |    1,129,305.21 |          885.50 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           26.00 |           16.32 |   61,259,742.31 |
|               2 |           26.00 |           16.26 |   61,482,561.54 |
|               3 |           26.00 |           16.39 |   60,998,703.85 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,592,753,300.00 |
|               2 |            0.00 |            0.00 |1,598,546,600.00 |
|               3 |            0.00 |            0.00 |1,585,966,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,592,753,300.00 |
|               2 |            0.00 |            0.00 |1,598,546,600.00 |
|               3 |            0.00 |            0.00 |1,585,966,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,593.00 |        1,000.15 |      999,845.13 |
|               2 |        1,599.00 |        1,000.28 |      999,716.45 |
|               3 |        1,586.00 |        1,000.02 |      999,978.75 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          125.57 |    7,963,766.50 |
|               2 |          200.00 |          125.11 |    7,992,733.00 |
|               3 |          200.00 |          126.11 |    7,929,831.50 |


