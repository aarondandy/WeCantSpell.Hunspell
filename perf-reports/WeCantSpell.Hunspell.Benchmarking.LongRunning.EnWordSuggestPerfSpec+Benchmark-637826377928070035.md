# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/11/2022 11:23:12 PM_
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
|    Elapsed Time |              ms |        1,609.00 |        1,599.33 |        1,580.00 |           16.74 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,942,865.81 |    3,896,546.51 |    3,873,161.96 |       40,114.32 |
|TotalCollections [Gen0] |     collections |           16.45 |           16.26 |           16.16 |            0.17 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.22 |        1,000.02 |          999.75 |            0.24 |
|[Counter] SuggestionQueries |      operations |          126.55 |          125.06 |          124.31 |            1.29 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    6,231,392.00 |    3,873,161.96 |          258.19 |
|               2 |    6,231,272.00 |    3,873,611.76 |          258.16 |
|               3 |    6,231,272.00 |    3,942,865.81 |          253.62 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           26.00 |           16.16 |   61,879,396.15 |
|               2 |           26.00 |           16.16 |   61,871,019.23 |
|               3 |           26.00 |           16.45 |   60,784,292.31 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,608,864,300.00 |
|               2 |            0.00 |            0.00 |1,608,646,500.00 |
|               3 |            0.00 |            0.00 |1,580,391,600.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,608,864,300.00 |
|               2 |            0.00 |            0.00 |1,608,646,500.00 |
|               3 |            0.00 |            0.00 |1,580,391,600.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,609.00 |        1,000.08 |      999,915.66 |
|               2 |        1,609.00 |        1,000.22 |      999,780.30 |
|               3 |        1,580.00 |          999.75 |    1,000,247.85 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          124.31 |    8,044,321.50 |
|               2 |          200.00 |          124.33 |    8,043,232.50 |
|               3 |          200.00 |          126.55 |    7,901,958.00 |


