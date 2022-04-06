# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_4/6/2022 8:02:58 PM_
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
|TotalBytesAllocated |           bytes |    1,884,936.00 |    1,884,901.33 |    1,884,840.00 |           53.27 |
|TotalCollections [Gen0] |     collections |            2.00 |            2.00 |            2.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,236.00 |        1,230.33 |        1,225.00 |            5.51 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,538,990.64 |    1,532,406.44 |    1,525,125.74 |        6,958.64 |
|TotalCollections [Gen0] |     collections |            1.63 |            1.63 |            1.62 |            0.01 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.42 |        1,000.24 |        1,000.06 |            0.18 |
|[Counter] SuggestionQueries |      operations |          163.30 |          162.60 |          161.82 |            0.74 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,884,936.00 |    1,525,125.74 |          655.68 |
|               2 |    1,884,928.00 |    1,533,102.95 |          652.27 |
|               3 |    1,884,840.00 |    1,538,990.64 |          649.78 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            2.00 |            1.62 |  617,960,850.00 |
|               2 |            2.00 |            1.63 |  614,742,800.00 |
|               3 |            2.00 |            1.63 |  612,362,400.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,235,921,700.00 |
|               2 |            0.00 |            0.00 |1,229,485,600.00 |
|               3 |            0.00 |            0.00 |1,224,724,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,235,921,700.00 |
|               2 |            0.00 |            0.00 |1,229,485,600.00 |
|               3 |            0.00 |            0.00 |1,224,724,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,236.00 |        1,000.06 |      999,936.65 |
|               2 |        1,230.00 |        1,000.42 |      999,581.79 |
|               3 |        1,225.00 |        1,000.22 |      999,775.35 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          161.82 |    6,179,608.50 |
|               2 |          200.00 |          162.67 |    6,147,428.00 |
|               3 |          200.00 |          163.30 |    6,123,624.00 |


