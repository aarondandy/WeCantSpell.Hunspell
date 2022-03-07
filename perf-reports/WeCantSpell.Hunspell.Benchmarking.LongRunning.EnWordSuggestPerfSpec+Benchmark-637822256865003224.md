# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/7/2022 4:54:46 AM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.2,IsMono=False,MaxGcGeneration=2
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
|    Elapsed Time |              ms |        1,552.00 |        1,541.00 |        1,535.00 |            9.54 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,316,825.84 |    5,295,898.95 |    5,257,795.16 |       33,052.08 |
|TotalCollections [Gen0] |     collections |           16.28 |           16.22 |           16.10 |            0.10 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          999.82 |          999.76 |          999.67 |            0.08 |
|[Counter] SuggestionQueries |      operations |          130.27 |          129.76 |          128.82 |            0.81 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    8,162,768.00 |    5,257,795.16 |          190.19 |
|               2 |    8,162,768.00 |    5,313,075.86 |          188.21 |
|               3 |    8,162,768.00 |    5,316,825.84 |          188.08 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |           16.10 |   62,100,312.00 |
|               2 |           25.00 |           16.27 |   61,454,180.00 |
|               3 |           25.00 |           16.28 |   61,410,836.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,552,507,800.00 |
|               2 |            0.00 |            0.00 |1,536,354,500.00 |
|               3 |            0.00 |            0.00 |1,535,270,900.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,552,507,800.00 |
|               2 |            0.00 |            0.00 |1,536,354,500.00 |
|               3 |            0.00 |            0.00 |1,535,270,900.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,552.00 |          999.67 |    1,000,327.19 |
|               2 |        1,536.00 |          999.77 |    1,000,230.79 |
|               3 |        1,535.00 |          999.82 |    1,000,176.48 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          128.82 |    7,762,539.00 |
|               2 |          200.00 |          130.18 |    7,681,772.50 |
|               3 |          200.00 |          130.27 |    7,676,354.50 |


