# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/11/2022 3:56:53 AM_
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
|TotalBytesAllocated |           bytes |    6,242,360.00 |    6,242,136.00 |    6,241,688.00 |          387.98 |
|TotalCollections [Gen0] |     collections |           26.00 |           26.00 |           26.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,588.00 |        1,586.33 |        1,585.00 |            1.53 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,937,548.11 |    3,934,417.80 |    3,929,493.98 |        4,316.23 |
|TotalCollections [Gen0] |     collections |           16.40 |           16.39 |           16.37 |            0.02 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.08 |          999.87 |          999.74 |            0.18 |
|[Counter] SuggestionQueries |      operations |          126.16 |          126.06 |          125.91 |            0.13 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    6,242,360.00 |    3,937,548.11 |          253.97 |
|               2 |    6,241,688.00 |    3,929,493.98 |          254.49 |
|               3 |    6,242,360.00 |    3,936,211.33 |          254.05 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           26.00 |           16.40 |   60,974,688.46 |
|               2 |           26.00 |           16.37 |   61,093,088.46 |
|               3 |           26.00 |           16.39 |   60,995,396.15 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,585,341,900.00 |
|               2 |            0.00 |            0.00 |1,588,420,300.00 |
|               3 |            0.00 |            0.00 |1,585,880,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,585,341,900.00 |
|               2 |            0.00 |            0.00 |1,588,420,300.00 |
|               3 |            0.00 |            0.00 |1,585,880,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,585.00 |          999.78 |    1,000,215.71 |
|               2 |        1,588.00 |          999.74 |    1,000,264.67 |
|               3 |        1,586.00 |        1,000.08 |      999,924.53 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          126.16 |    7,926,709.50 |
|               2 |          200.00 |          125.91 |    7,942,101.50 |
|               3 |          200.00 |          126.11 |    7,929,401.50 |


