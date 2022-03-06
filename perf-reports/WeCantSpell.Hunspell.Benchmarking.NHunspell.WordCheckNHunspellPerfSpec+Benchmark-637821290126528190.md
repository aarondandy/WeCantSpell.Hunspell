# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_03/06/2022 02:03:32_
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
|    Elapsed Time |              ms |        1,002.00 |          999.00 |          996.00 |            3.00 |
|[Counter] _wordsChecked |      operations |    1,309,504.00 |    1,309,504.00 |    1,309,504.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.42 |        1,000.22 |        1,000.08 |            0.18 |
|[Counter] _wordsChecked |      operations |    1,314,963.07 |    1,311,110.05 |    1,307,442.29 |        3,763.81 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  995,848,500.00 |
|               2 |            0.00 |            0.00 |1,001,576,900.00 |
|               3 |            0.00 |            0.00 |  998,916,200.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  995,848,500.00 |
|               2 |            0.00 |            0.00 |1,001,576,900.00 |
|               3 |            0.00 |            0.00 |  998,916,200.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  995,848,500.00 |
|               2 |            0.00 |            0.00 |1,001,576,900.00 |
|               3 |            0.00 |            0.00 |  998,916,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  995,848,500.00 |
|               2 |            0.00 |            0.00 |1,001,576,900.00 |
|               3 |            0.00 |            0.00 |  998,916,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          996.00 |        1,000.15 |      999,847.89 |
|               2 |        1,002.00 |        1,000.42 |      999,577.74 |
|               3 |          999.00 |        1,000.08 |      999,916.12 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,309,504.00 |    1,314,963.07 |          760.48 |
|               2 |    1,309,504.00 |    1,307,442.29 |          764.85 |
|               3 |    1,309,504.00 |    1,310,924.78 |          762.82 |


