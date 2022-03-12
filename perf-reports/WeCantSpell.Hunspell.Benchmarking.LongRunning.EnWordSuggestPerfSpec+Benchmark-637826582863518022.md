# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/12/2022 5:04:46 AM_
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
|TotalBytesAllocated |           bytes |    3,046,384.00 |    3,046,384.00 |    3,046,384.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           26.00 |           26.00 |           26.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,566.00 |        1,556.33 |        1,545.00 |           10.60 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,971,826.76 |    1,957,123.62 |    1,944,668.95 |       13,717.81 |
|TotalCollections [Gen0] |     collections |           16.83 |           16.70 |           16.60 |            0.12 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.03 |          999.82 |          999.66 |            0.19 |
|[Counter] SuggestionQueries |      operations |          129.45 |          128.49 |          127.67 |            0.90 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,046,384.00 |    1,954,875.16 |          511.54 |
|               2 |    3,046,384.00 |    1,944,668.95 |          514.23 |
|               3 |    3,046,384.00 |    1,971,826.76 |          507.14 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           26.00 |           16.68 |   59,936,623.08 |
|               2 |           26.00 |           16.60 |   60,251,188.46 |
|               3 |           26.00 |           16.83 |   59,421,353.85 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,558,352,200.00 |
|               2 |            0.00 |            0.00 |1,566,530,900.00 |
|               3 |            0.00 |            0.00 |1,544,955,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,558,352,200.00 |
|               2 |            0.00 |            0.00 |1,566,530,900.00 |
|               3 |            0.00 |            0.00 |1,544,955,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,558.00 |          999.77 |    1,000,226.06 |
|               2 |        1,566.00 |          999.66 |    1,000,339.02 |
|               3 |        1,545.00 |        1,000.03 |      999,971.00 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          128.34 |    7,791,761.00 |
|               2 |          200.00 |          127.67 |    7,832,654.50 |
|               3 |          200.00 |          129.45 |    7,724,776.00 |


