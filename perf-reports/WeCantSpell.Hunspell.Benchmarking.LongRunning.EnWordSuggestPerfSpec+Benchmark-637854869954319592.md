# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_4/13/2022 10:49:55 PM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.4,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |    1,653,160.00 |    1,653,160.00 |    1,653,160.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            5.00 |            5.00 |            5.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,136.00 |        1,134.33 |        1,133.00 |            1.53 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,458,712.20 |    1,457,419.63 |    1,455,331.54 |        1,825.34 |
|TotalCollections [Gen0] |     collections |            4.41 |            4.41 |            4.40 |            0.01 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.28 |        1,000.02 |          999.73 |            0.27 |
|[Counter] SuggestionQueries |      operations |          176.48 |          176.32 |          176.07 |            0.22 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,653,160.00 |    1,458,712.20 |          685.54 |
|               2 |    1,653,160.00 |    1,458,215.15 |          685.77 |
|               3 |    1,653,160.00 |    1,455,331.54 |          687.13 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            5.00 |            4.41 |  226,660,200.00 |
|               2 |            5.00 |            4.41 |  226,737,460.00 |
|               3 |            5.00 |            4.40 |  227,186,720.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,133,301,000.00 |
|               2 |            0.00 |            0.00 |1,133,687,300.00 |
|               3 |            0.00 |            0.00 |1,135,933,600.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,133,301,000.00 |
|               2 |            0.00 |            0.00 |1,133,687,300.00 |
|               3 |            0.00 |            0.00 |1,135,933,600.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,133.00 |          999.73 |    1,000,265.67 |
|               2 |        1,134.00 |        1,000.28 |      999,724.25 |
|               3 |        1,136.00 |        1,000.06 |      999,941.55 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          176.48 |    5,666,505.00 |
|               2 |          200.00 |          176.42 |    5,668,436.50 |
|               3 |          200.00 |          176.07 |    5,679,668.00 |


