# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/10/2022 4:41:06 AM_
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
|TotalBytesAllocated |           bytes |    1,791,040.00 |    1,791,040.00 |    1,791,040.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           26.00 |           26.00 |           26.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,506.00 |        1,503.33 |        1,500.00 |            3.06 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,193,589.81 |    1,191,239.89 |    1,189,352.67 |        2,156.13 |
|TotalCollections [Gen0] |     collections |           17.33 |           17.29 |           17.27 |            0.03 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.07 |          999.88 |          999.63 |            0.22 |
|[Counter] SuggestionQueries |      operations |          133.28 |          133.02 |          132.81 |            0.24 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,791,040.00 |    1,193,589.81 |          837.81 |
|               2 |    1,791,040.00 |    1,190,777.19 |          839.79 |
|               3 |    1,791,040.00 |    1,189,352.67 |          840.79 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           26.00 |           17.33 |   57,713,423.08 |
|               2 |           26.00 |           17.29 |   57,849,742.31 |
|               3 |           26.00 |           17.27 |   57,919,030.77 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,500,549,000.00 |
|               2 |            0.00 |            0.00 |1,504,093,300.00 |
|               3 |            0.00 |            0.00 |1,505,894,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,500,549,000.00 |
|               2 |            0.00 |            0.00 |1,504,093,300.00 |
|               3 |            0.00 |            0.00 |1,505,894,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,500.00 |          999.63 |    1,000,366.00 |
|               2 |        1,504.00 |          999.94 |    1,000,062.03 |
|               3 |        1,506.00 |        1,000.07 |      999,930.15 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          133.28 |    7,502,745.00 |
|               2 |          200.00 |          132.97 |    7,520,466.50 |
|               3 |          200.00 |          132.81 |    7,529,474.00 |


