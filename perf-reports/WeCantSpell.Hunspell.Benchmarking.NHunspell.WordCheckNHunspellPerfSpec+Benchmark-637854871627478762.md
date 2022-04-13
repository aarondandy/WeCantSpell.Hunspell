# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_04/13/2022 22:52:42_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 6.2.9200.0
ProcessorCount=16
CLR=4.0.30319.42000,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,008.00 |        1,005.33 |        1,004.00 |            2.31 |
|[Counter] _wordsChecked |      operations |    1,284,640.00 |    1,284,640.00 |    1,284,640.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.77 |        1,000.23 |          999.44 |            0.70 |
|[Counter] _wordsChecked |      operations |    1,280,137.12 |    1,278,118.77 |    1,275,419.74 |        2,431.24 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,003,517,500.00 |
|               2 |            0.00 |            0.00 |1,007,229,200.00 |
|               3 |            0.00 |            0.00 |1,004,567,200.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,003,517,500.00 |
|               2 |            0.00 |            0.00 |1,007,229,200.00 |
|               3 |            0.00 |            0.00 |1,004,567,200.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,003,517,500.00 |
|               2 |            0.00 |            0.00 |1,007,229,200.00 |
|               3 |            0.00 |            0.00 |1,004,567,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,003,517,500.00 |
|               2 |            0.00 |            0.00 |1,007,229,200.00 |
|               3 |            0.00 |            0.00 |1,004,567,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,004.00 |        1,000.48 |      999,519.42 |
|               2 |        1,008.00 |        1,000.77 |      999,235.32 |
|               3 |        1,004.00 |          999.44 |    1,000,564.94 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,284,640.00 |    1,280,137.12 |          781.17 |
|               2 |    1,284,640.00 |    1,275,419.74 |          784.06 |
|               3 |    1,284,640.00 |    1,278,799.47 |          781.98 |


