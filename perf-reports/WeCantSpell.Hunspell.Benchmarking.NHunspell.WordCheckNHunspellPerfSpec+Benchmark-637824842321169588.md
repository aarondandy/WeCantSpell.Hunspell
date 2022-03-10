# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_03/10/2022 04:43:52_
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
|    Elapsed Time |              ms |        1,002.00 |          994.67 |          986.00 |            8.08 |
|[Counter] _wordsChecked |      operations |    1,284,640.00 |    1,284,640.00 |    1,284,640.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.47 |        1,000.15 |          999.74 |            0.37 |
|[Counter] _wordsChecked |      operations |    1,302,540.95 |    1,291,772.12 |    1,282,374.81 |       10,152.79 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,001,766,400.00 |
|               2 |            0.00 |            0.00 |  986,256,900.00 |
|               3 |            0.00 |            0.00 |  995,535,800.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,001,766,400.00 |
|               2 |            0.00 |            0.00 |  986,256,900.00 |
|               3 |            0.00 |            0.00 |  995,535,800.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,001,766,400.00 |
|               2 |            0.00 |            0.00 |  986,256,900.00 |
|               3 |            0.00 |            0.00 |  995,535,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,001,766,400.00 |
|               2 |            0.00 |            0.00 |  986,256,900.00 |
|               3 |            0.00 |            0.00 |  995,535,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,002.00 |        1,000.23 |      999,766.87 |
|               2 |          986.00 |          999.74 |    1,000,260.55 |
|               3 |          996.00 |        1,000.47 |      999,533.94 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,284,640.00 |    1,282,374.81 |          779.80 |
|               2 |    1,284,640.00 |    1,302,540.95 |          767.73 |
|               3 |    1,284,640.00 |    1,290,400.61 |          774.95 |


