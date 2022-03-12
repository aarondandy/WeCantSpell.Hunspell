# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/12/2022 2:46:59 AM_
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
|TotalBytesAllocated |           bytes |    6,190,480.00 |    6,190,480.00 |    6,190,480.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           26.00 |           26.00 |           26.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,624.00 |        1,609.00 |        1,581.00 |           24.27 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,913,560.39 |    3,847,450.15 |    3,810,882.36 |       57,360.80 |
|TotalCollections [Gen0] |     collections |           16.44 |           16.16 |           16.01 |            0.24 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.35 |          999.86 |          999.49 |            0.44 |
|[Counter] SuggestionQueries |      operations |          126.44 |          124.30 |          123.12 |            1.85 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    6,190,480.00 |    3,913,560.39 |          255.52 |
|               2 |    6,190,480.00 |    3,817,907.69 |          261.92 |
|               3 |    6,190,480.00 |    3,810,882.36 |          262.41 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           26.00 |           16.44 |   60,838,561.54 |
|               2 |           26.00 |           16.04 |   62,362,792.31 |
|               3 |           26.00 |           16.01 |   62,477,757.69 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,581,802,600.00 |
|               2 |            0.00 |            0.00 |1,621,432,600.00 |
|               3 |            0.00 |            0.00 |1,624,421,700.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,581,802,600.00 |
|               2 |            0.00 |            0.00 |1,621,432,600.00 |
|               3 |            0.00 |            0.00 |1,624,421,700.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,581.00 |          999.49 |    1,000,507.65 |
|               2 |        1,622.00 |        1,000.35 |      999,650.18 |
|               3 |        1,624.00 |          999.74 |    1,000,259.67 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          126.44 |    7,909,013.00 |
|               2 |          200.00 |          123.35 |    8,107,163.00 |
|               3 |          200.00 |          123.12 |    8,122,108.50 |


