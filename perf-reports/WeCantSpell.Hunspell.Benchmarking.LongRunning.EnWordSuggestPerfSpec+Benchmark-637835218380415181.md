# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/22/2022 4:57:18 AM_
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
|TotalBytesAllocated |           bytes |    3,848,928.00 |    3,848,877.33 |    3,848,816.00 |           56.76 |
|TotalCollections [Gen0] |     collections |            5.00 |            5.00 |            5.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,243.00 |        1,241.33 |        1,240.00 |            1.53 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,102,952.48 |    3,099,965.28 |    3,096,692.47 |        3,139.76 |
|TotalCollections [Gen0] |     collections |            4.03 |            4.03 |            4.02 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.07 |          999.79 |          999.62 |            0.24 |
|[Counter] SuggestionQueries |      operations |          161.24 |          161.08 |          160.91 |            0.17 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,848,816.00 |    3,102,952.48 |          322.27 |
|               2 |    3,848,888.00 |    3,100,250.89 |          322.55 |
|               3 |    3,848,928.00 |    3,096,692.47 |          322.93 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            5.00 |            4.03 |  248,074,440.00 |
|               2 |            5.00 |            4.03 |  248,295,260.00 |
|               3 |            5.00 |            4.02 |  248,583,160.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,240,372,200.00 |
|               2 |            0.00 |            0.00 |1,241,476,300.00 |
|               3 |            0.00 |            0.00 |1,242,915,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,240,372,200.00 |
|               2 |            0.00 |            0.00 |1,241,476,300.00 |
|               3 |            0.00 |            0.00 |1,242,915,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,240.00 |          999.70 |    1,000,300.16 |
|               2 |        1,241.00 |          999.62 |    1,000,383.80 |
|               3 |        1,243.00 |        1,000.07 |      999,932.26 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          161.24 |    6,201,861.00 |
|               2 |          200.00 |          161.10 |    6,207,381.50 |
|               3 |          200.00 |          160.91 |    6,214,579.00 |


