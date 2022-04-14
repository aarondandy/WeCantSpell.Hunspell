# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_4/14/2022 12:58:24 PM_
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
|TotalBytesAllocated |           bytes |    7,741,920.00 |    7,741,898.67 |    7,741,888.00 |           18.48 |
|TotalCollections [Gen0] |     collections |            1.00 |            1.00 |            1.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,236.00 |        1,218.33 |        1,208.00 |           15.37 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,409,276.38 |    6,354,480.50 |    6,261,753.04 |       80,745.82 |
|TotalCollections [Gen0] |     collections |            0.83 |            0.82 |            0.81 |            0.01 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.07 |          999.89 |          999.69 |            0.19 |
|[Counter] SuggestionQueries |      operations |          165.57 |          164.16 |          161.76 |            2.09 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    7,741,888.00 |    6,409,276.38 |          156.02 |
|               2 |    7,741,920.00 |    6,261,753.04 |          159.70 |
|               3 |    7,741,888.00 |    6,392,412.09 |          156.44 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            1.00 |            0.83 |1,207,919,200.00 |
|               2 |            1.00 |            0.81 |1,236,382,200.00 |
|               3 |            1.00 |            0.83 |1,211,105,900.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,207,919,200.00 |
|               2 |            0.00 |            0.00 |1,236,382,200.00 |
|               3 |            0.00 |            0.00 |1,211,105,900.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,207,919,200.00 |
|               2 |            0.00 |            0.00 |1,236,382,200.00 |
|               3 |            0.00 |            0.00 |1,211,105,900.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,208.00 |        1,000.07 |      999,933.11 |
|               2 |        1,236.00 |          999.69 |    1,000,309.22 |
|               3 |        1,211.00 |          999.91 |    1,000,087.45 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          165.57 |    6,039,596.00 |
|               2 |          200.00 |          161.76 |    6,181,911.00 |
|               3 |          200.00 |          165.14 |    6,055,529.50 |


