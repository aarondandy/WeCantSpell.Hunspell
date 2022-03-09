# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/9/2022 2:43:07 AM_
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
|TotalBytesAllocated |           bytes |    8,162,768.00 |    8,162,768.00 |    8,162,768.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           25.00 |           25.00 |           25.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,573.00 |        1,568.67 |        1,566.00 |            3.79 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,211,510.80 |    5,203,357.34 |    5,189,842.50 |       11,787.16 |
|TotalCollections [Gen0] |     collections |           15.96 |           15.94 |           15.89 |            0.04 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.10 |          999.94 |          999.81 |            0.15 |
|[Counter] SuggestionQueries |      operations |          127.69 |          127.49 |          127.16 |            0.29 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    8,162,768.00 |    5,211,510.80 |          191.88 |
|               2 |    8,162,768.00 |    5,189,842.50 |          192.68 |
|               3 |    8,162,768.00 |    5,208,718.71 |          191.99 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |           15.96 |   62,651,836.00 |
|               2 |           25.00 |           15.89 |   62,913,416.00 |
|               3 |           25.00 |           15.95 |   62,685,420.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,566,295,900.00 |
|               2 |            0.00 |            0.00 |1,572,835,400.00 |
|               3 |            0.00 |            0.00 |1,567,135,500.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,566,295,900.00 |
|               2 |            0.00 |            0.00 |1,572,835,400.00 |
|               3 |            0.00 |            0.00 |1,567,135,500.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,566.00 |          999.81 |    1,000,188.95 |
|               2 |        1,573.00 |        1,000.10 |      999,895.36 |
|               3 |        1,567.00 |          999.91 |    1,000,086.47 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          127.69 |    7,831,479.50 |
|               2 |          200.00 |          127.16 |    7,864,177.00 |
|               3 |          200.00 |          127.62 |    7,835,677.50 |


