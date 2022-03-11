# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_03/11/2022 02:48:04_
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
|    Elapsed Time |              ms |        1,013.00 |        1,009.67 |        1,007.00 |            3.06 |
|[Counter] _wordsChecked |      operations |    1,326,080.00 |    1,326,080.00 |    1,326,080.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.52 |        1,000.00 |          999.71 |            0.46 |
|[Counter] _wordsChecked |      operations |    1,316,545.32 |    1,313,388.57 |    1,308,680.70 |        4,155.42 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,008,472,100.00 |
|               2 |            0.00 |            0.00 |1,013,295,300.00 |
|               3 |            0.00 |            0.00 |1,007,242,200.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,008,472,100.00 |
|               2 |            0.00 |            0.00 |1,013,295,300.00 |
|               3 |            0.00 |            0.00 |1,007,242,200.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,008,472,100.00 |
|               2 |            0.00 |            0.00 |1,013,295,300.00 |
|               3 |            0.00 |            0.00 |1,007,242,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,008,472,100.00 |
|               2 |            0.00 |            0.00 |1,013,295,300.00 |
|               3 |            0.00 |            0.00 |1,007,242,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,009.00 |        1,000.52 |      999,476.81 |
|               2 |        1,013.00 |          999.71 |    1,000,291.51 |
|               3 |        1,007.00 |          999.76 |    1,000,240.52 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,326,080.00 |    1,314,939.70 |          760.49 |
|               2 |    1,326,080.00 |    1,308,680.70 |          764.13 |
|               3 |    1,326,080.00 |    1,316,545.32 |          759.56 |


