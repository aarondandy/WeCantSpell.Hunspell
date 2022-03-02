# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/02/2022 04:22:36_
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
|TotalBytesAllocated |           bytes |    5,866,912.00 |    5,866,912.00 |    5,866,912.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           74.00 |           74.00 |           74.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,041.00 |        1,017.00 |        1,003.00 |           20.88 |
|[Counter] _wordsChecked |      operations |      646,464.00 |      646,464.00 |      646,464.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,848,330.10 |    5,770,955.35 |    5,636,607.54 |      116,794.94 |
|TotalCollections [Gen0] |     collections |           73.77 |           72.79 |           71.10 |            1.47 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.31 |        1,000.09 |          999.82 |            0.25 |
|[Counter] _wordsChecked |      operations |      644,416.50 |      635,890.72 |      621,087.19 |       12,869.41 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    5,866,912.00 |    5,848,330.10 |          170.99 |
|               2 |    5,866,912.00 |    5,827,928.40 |          171.59 |
|               3 |    5,866,912.00 |    5,636,607.54 |          177.41 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           74.00 |           73.77 |   13,556,450.00 |
|               2 |           74.00 |           73.51 |   13,603,906.76 |
|               3 |           74.00 |           71.10 |   14,065,658.11 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,003,177,300.00 |
|               2 |            0.00 |            0.00 |1,006,689,100.00 |
|               3 |            0.00 |            0.00 |1,040,858,700.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,003,177,300.00 |
|               2 |            0.00 |            0.00 |1,006,689,100.00 |
|               3 |            0.00 |            0.00 |1,040,858,700.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,003.00 |          999.82 |    1,000,176.77 |
|               2 |        1,007.00 |        1,000.31 |      999,691.26 |
|               3 |        1,041.00 |        1,000.14 |      999,864.27 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      646,464.00 |      644,416.50 |        1,551.79 |
|               2 |      646,464.00 |      642,168.47 |        1,557.22 |
|               3 |      646,464.00 |      621,087.19 |        1,610.08 |


