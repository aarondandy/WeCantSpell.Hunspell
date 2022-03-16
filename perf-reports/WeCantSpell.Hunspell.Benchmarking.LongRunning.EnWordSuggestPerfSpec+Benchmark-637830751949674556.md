# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/17/2022 12:53:14 AM_
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
|TotalBytesAllocated |           bytes |      591,976.00 |      591,896.00 |      591,856.00 |           69.28 |
|TotalCollections [Gen0] |     collections |            5.00 |            5.00 |            5.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,109.00 |        1,107.67 |        1,107.00 |            1.15 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      534,837.15 |      534,276.16 |      533,577.59 |          640.95 |
|TotalCollections [Gen0] |     collections |            4.52 |            4.51 |            4.51 |            0.01 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.15 |          999.84 |          999.56 |            0.30 |
|[Counter] SuggestionQueries |      operations |          180.70 |          180.53 |          180.31 |            0.20 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      591,976.00 |      534,837.15 |        1,869.73 |
|               2 |      591,856.00 |      534,413.74 |        1,871.21 |
|               3 |      591,856.00 |      533,577.59 |        1,874.14 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            5.00 |            4.52 |  221,366,820.00 |
|               2 |            5.00 |            4.51 |  221,497,300.00 |
|               3 |            5.00 |            4.51 |  221,844,400.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,106,834,100.00 |
|               2 |            0.00 |            0.00 |1,107,486,500.00 |
|               3 |            0.00 |            0.00 |1,109,222,000.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,106,834,100.00 |
|               2 |            0.00 |            0.00 |1,107,486,500.00 |
|               3 |            0.00 |            0.00 |1,109,222,000.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,107.00 |        1,000.15 |      999,850.14 |
|               2 |        1,107.00 |          999.56 |    1,000,439.48 |
|               3 |        1,109.00 |          999.80 |    1,000,200.18 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          180.70 |    5,534,170.50 |
|               2 |          200.00 |          180.59 |    5,537,432.50 |
|               3 |          200.00 |          180.31 |    5,546,110.00 |


