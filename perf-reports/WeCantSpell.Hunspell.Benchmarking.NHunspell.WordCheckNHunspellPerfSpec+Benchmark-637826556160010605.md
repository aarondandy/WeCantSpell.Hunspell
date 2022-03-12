# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_03/12/2022 04:20:16_
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
|    Elapsed Time |              ms |        1,006.00 |        1,000.67 |          994.00 |            6.11 |
|[Counter] _wordsChecked |      operations |    1,276,352.00 |    1,276,352.00 |    1,276,352.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.66 |        1,000.04 |          999.56 |            0.56 |
|[Counter] _wordsChecked |      operations |    1,283,497.75 |    1,275,580.70 |    1,268,597.07 |        7,494.06 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,001,337,400.00 |
|               2 |            0.00 |            0.00 |  994,432,600.00 |
|               3 |            0.00 |            0.00 |1,006,113,000.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,001,337,400.00 |
|               2 |            0.00 |            0.00 |  994,432,600.00 |
|               3 |            0.00 |            0.00 |1,006,113,000.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,001,337,400.00 |
|               2 |            0.00 |            0.00 |  994,432,600.00 |
|               3 |            0.00 |            0.00 |1,006,113,000.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,001,337,400.00 |
|               2 |            0.00 |            0.00 |  994,432,600.00 |
|               3 |            0.00 |            0.00 |1,006,113,000.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,002.00 |        1,000.66 |      999,338.72 |
|               2 |          994.00 |          999.56 |    1,000,435.21 |
|               3 |        1,006.00 |          999.89 |    1,000,112.33 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,276,352.00 |    1,274,647.29 |          784.53 |
|               2 |    1,276,352.00 |    1,283,497.75 |          779.12 |
|               3 |    1,276,352.00 |    1,268,597.07 |          788.27 |


