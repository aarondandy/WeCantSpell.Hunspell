# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_4/9/2022 2:16:20 PM_
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
|TotalBytesAllocated |           bytes |    1,653,200.00 |    1,653,173.33 |    1,653,160.00 |           23.09 |
|TotalCollections [Gen0] |     collections |            5.00 |            5.00 |            5.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,138.00 |        1,137.33 |        1,137.00 |            0.58 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,454,584.75 |    1,453,659.15 |    1,453,163.82 |          802.25 |
|TotalCollections [Gen0] |     collections |            4.40 |            4.40 |            4.40 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.43 |        1,000.07 |          999.47 |            0.53 |
|[Counter] SuggestionQueries |      operations |          175.98 |          175.86 |          175.80 |            0.10 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,653,160.00 |    1,453,163.82 |          688.15 |
|               2 |    1,653,200.00 |    1,453,228.88 |          688.12 |
|               3 |    1,653,160.00 |    1,454,584.75 |          687.48 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            5.00 |            4.40 |  227,525,620.00 |
|               2 |            5.00 |            4.40 |  227,520,940.00 |
|               3 |            5.00 |            4.40 |  227,303,360.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,137,628,100.00 |
|               2 |            0.00 |            0.00 |1,137,604,700.00 |
|               3 |            0.00 |            0.00 |1,136,516,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,137,628,100.00 |
|               2 |            0.00 |            0.00 |1,137,604,700.00 |
|               3 |            0.00 |            0.00 |1,136,516,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,138.00 |        1,000.33 |      999,673.20 |
|               2 |        1,137.00 |          999.47 |    1,000,531.84 |
|               3 |        1,137.00 |        1,000.43 |      999,575.02 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          175.80 |    5,688,140.50 |
|               2 |          200.00 |          175.81 |    5,688,023.50 |
|               3 |          200.00 |          175.98 |    5,682,584.00 |


