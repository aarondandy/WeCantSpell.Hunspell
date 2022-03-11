# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/11/2022 02:48:14_
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
|TotalBytesAllocated |           bytes |      525,904.00 |      525,904.00 |      525,904.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           80.00 |           80.00 |           80.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,019.00 |        1,015.33 |        1,013.00 |            3.21 |
|[Counter] _wordsChecked |      operations |      679,616.00 |      679,616.00 |      679,616.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      518,818.04 |      517,784.48 |      515,893.90 |        1,639.67 |
|TotalCollections [Gen0] |     collections |           78.92 |           78.76 |           78.48 |            0.25 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.00 |          999.65 |          999.35 |            0.33 |
|[Counter] _wordsChecked |      operations |      670,458.94 |      669,123.30 |      666,680.14 |        2,118.91 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      525,904.00 |      518,818.04 |        1,927.46 |
|               2 |      525,904.00 |      518,641.51 |        1,928.11 |
|               3 |      525,904.00 |      515,893.90 |        1,938.38 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           80.00 |           78.92 |   12,670,723.75 |
|               2 |           80.00 |           78.90 |   12,675,036.25 |
|               3 |           80.00 |           78.48 |   12,742,542.50 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,013,657,900.00 |
|               2 |            0.00 |            0.00 |1,014,002,900.00 |
|               3 |            0.00 |            0.00 |1,019,403,400.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,013,657,900.00 |
|               2 |            0.00 |            0.00 |1,014,002,900.00 |
|               3 |            0.00 |            0.00 |1,019,403,400.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,013.00 |          999.35 |    1,000,649.46 |
|               2 |        1,014.00 |        1,000.00 |    1,000,002.86 |
|               3 |        1,019.00 |          999.60 |    1,000,395.88 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      679,616.00 |      670,458.94 |        1,491.52 |
|               2 |      679,616.00 |      670,230.82 |        1,492.02 |
|               3 |      679,616.00 |      666,680.14 |        1,499.97 |


