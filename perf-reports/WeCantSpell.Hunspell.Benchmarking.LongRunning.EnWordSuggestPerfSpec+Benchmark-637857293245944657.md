# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_4/16/2022 6:08:44 PM_
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
|TotalBytesAllocated |           bytes |    7,299,656.00 |    7,299,656.00 |    7,299,656.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            1.00 |            1.00 |            1.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,201.00 |        1,199.33 |        1,198.00 |            1.53 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,089,738.78 |    6,083,409.99 |    6,073,970.64 |        8,331.58 |
|TotalCollections [Gen0] |     collections |            0.83 |            0.83 |            0.83 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          999.74 |          999.50 |          999.34 |            0.21 |
|[Counter] SuggestionQueries |      operations |          166.85 |          166.68 |          166.42 |            0.23 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    7,299,656.00 |    6,089,738.78 |          164.21 |
|               2 |    7,299,656.00 |    6,073,970.64 |          164.64 |
|               3 |    7,299,656.00 |    6,086,520.55 |          164.30 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            1.00 |            0.83 |1,198,681,300.00 |
|               2 |            1.00 |            0.83 |1,201,793,100.00 |
|               3 |            1.00 |            0.83 |1,199,315,100.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,198,681,300.00 |
|               2 |            0.00 |            0.00 |1,201,793,100.00 |
|               3 |            0.00 |            0.00 |1,199,315,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,198,681,300.00 |
|               2 |            0.00 |            0.00 |1,201,793,100.00 |
|               3 |            0.00 |            0.00 |1,199,315,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,198.00 |          999.43 |    1,000,568.70 |
|               2 |        1,201.00 |          999.34 |    1,000,660.37 |
|               3 |        1,199.00 |          999.74 |    1,000,262.80 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          166.85 |    5,993,406.50 |
|               2 |          200.00 |          166.42 |    6,008,965.50 |
|               3 |          200.00 |          166.76 |    5,996,575.50 |


