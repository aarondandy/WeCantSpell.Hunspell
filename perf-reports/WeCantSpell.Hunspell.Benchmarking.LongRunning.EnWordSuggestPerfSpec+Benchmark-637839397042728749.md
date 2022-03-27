# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/27/2022 1:01:44 AM_
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
|TotalBytesAllocated |           bytes |    7,693,392.00 |    7,687,925.33 |    7,685,192.00 |        4,734.27 |
|TotalCollections [Gen0] |     collections |            2.00 |            2.00 |            2.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,262.00 |        1,252.00 |        1,246.00 |            8.72 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,173,637.03 |    6,140,421.86 |    6,088,186.42 |       45,790.61 |
|TotalCollections [Gen0] |     collections |            1.60 |            1.60 |            1.58 |            0.01 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.23 |          999.95 |          999.75 |            0.25 |
|[Counter] SuggestionQueries |      operations |          160.49 |          159.74 |          158.44 |            1.13 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    7,685,192.00 |    6,159,442.13 |          162.35 |
|               2 |    7,693,392.00 |    6,173,637.03 |          161.98 |
|               3 |    7,685,192.00 |    6,088,186.42 |          164.25 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            2.00 |            1.60 |  623,854,550.00 |
|               2 |            2.00 |            1.60 |  623,084,250.00 |
|               3 |            2.00 |            1.58 |  631,156,100.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,247,709,100.00 |
|               2 |            0.00 |            0.00 |1,246,168,500.00 |
|               3 |            0.00 |            0.00 |1,262,312,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,247,709,100.00 |
|               2 |            0.00 |            0.00 |1,246,168,500.00 |
|               3 |            0.00 |            0.00 |1,262,312,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,248.00 |        1,000.23 |      999,766.91 |
|               2 |        1,246.00 |          999.86 |    1,000,135.23 |
|               3 |        1,262.00 |          999.75 |    1,000,247.39 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          160.29 |    6,238,545.50 |
|               2 |          200.00 |          160.49 |    6,230,842.50 |
|               3 |          200.00 |          158.44 |    6,311,561.00 |


