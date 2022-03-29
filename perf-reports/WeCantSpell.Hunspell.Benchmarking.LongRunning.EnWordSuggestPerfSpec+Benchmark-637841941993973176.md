# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/29/2022 11:43:19 PM_
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
|TotalBytesAllocated |           bytes |    1,925,736.00 |    1,925,736.00 |    1,925,736.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            2.00 |            2.00 |            2.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,249.00 |        1,247.00 |        1,246.00 |            1.73 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,546,038.52 |    1,544,414.59 |    1,541,564.06 |        2,476.62 |
|TotalCollections [Gen0] |     collections |            1.61 |            1.60 |            1.60 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.33 |        1,000.08 |          999.83 |            0.25 |
|[Counter] SuggestionQueries |      operations |          160.57 |          160.40 |          160.10 |            0.26 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,925,736.00 |    1,541,564.06 |          648.69 |
|               2 |    1,925,736.00 |    1,546,038.52 |          646.81 |
|               3 |    1,925,736.00 |    1,545,641.19 |          646.98 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            2.00 |            1.60 |  624,604,600.00 |
|               2 |            2.00 |            1.61 |  622,796,900.00 |
|               3 |            2.00 |            1.61 |  622,957,000.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,249,209,200.00 |
|               2 |            0.00 |            0.00 |1,245,593,800.00 |
|               3 |            0.00 |            0.00 |1,245,914,000.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,249,209,200.00 |
|               2 |            0.00 |            0.00 |1,245,593,800.00 |
|               3 |            0.00 |            0.00 |1,245,914,000.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,249.00 |          999.83 |    1,000,167.49 |
|               2 |        1,246.00 |        1,000.33 |      999,674.00 |
|               3 |        1,246.00 |        1,000.07 |      999,930.98 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          160.10 |    6,246,046.00 |
|               2 |          200.00 |          160.57 |    6,227,969.00 |
|               3 |          200.00 |          160.52 |    6,229,570.00 |


