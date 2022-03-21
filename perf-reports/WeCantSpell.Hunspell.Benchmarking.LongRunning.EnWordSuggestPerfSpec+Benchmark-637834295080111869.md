# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/21/2022 3:18:28 AM_
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
|TotalBytesAllocated |           bytes |    3,922,600.00 |    3,922,557.33 |    3,922,504.00 |           48.88 |
|TotalCollections [Gen0] |     collections |            5.00 |            5.00 |            5.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,238.00 |        1,237.67 |        1,237.00 |            0.58 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,170,385.21 |    3,169,251.14 |    3,168,236.54 |        1,079.30 |
|TotalCollections [Gen0] |     collections |            4.04 |            4.04 |            4.04 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.22 |          999.98 |          999.79 |            0.22 |
|[Counter] SuggestionQueries |      operations |          161.65 |          161.59 |          161.54 |            0.05 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,922,568.00 |    3,168,236.54 |          315.63 |
|               2 |    3,922,504.00 |    3,169,131.68 |          315.54 |
|               3 |    3,922,600.00 |    3,170,385.21 |          315.42 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            5.00 |            4.04 |  247,618,380.00 |
|               2 |            5.00 |            4.04 |  247,544,400.00 |
|               3 |            5.00 |            4.04 |  247,452,580.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,238,091,900.00 |
|               2 |            0.00 |            0.00 |1,237,722,000.00 |
|               3 |            0.00 |            0.00 |1,237,262,900.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,238,091,900.00 |
|               2 |            0.00 |            0.00 |1,237,722,000.00 |
|               3 |            0.00 |            0.00 |1,237,262,900.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,238.00 |          999.93 |    1,000,074.23 |
|               2 |        1,238.00 |        1,000.22 |      999,775.44 |
|               3 |        1,237.00 |          999.79 |    1,000,212.53 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          161.54 |    6,190,459.50 |
|               2 |          200.00 |          161.59 |    6,188,610.00 |
|               3 |          200.00 |          161.65 |    6,186,314.50 |


