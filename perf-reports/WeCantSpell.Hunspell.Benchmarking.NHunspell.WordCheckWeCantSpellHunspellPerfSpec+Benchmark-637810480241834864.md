# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_02/21/2022 13:47:04_
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
|TotalBytesAllocated |           bytes |    2,724,016.00 |    2,724,016.00 |    2,724,016.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           83.00 |           83.00 |           83.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,230.00 |        1,188.67 |        1,138.00 |           46.70 |
|[Counter] _wordsChecked |      operations |      721,056.00 |      721,056.00 |      721,056.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,393,785.82 |    2,293,541.31 |    2,213,741.50 |       91,746.82 |
|TotalCollections [Gen0] |     collections |           72.94 |           69.88 |           67.45 |            2.80 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.04 |          999.77 |          999.59 |            0.24 |
|[Counter] _wordsChecked |      operations |      633,642.99 |      607,107.93 |      585,984.66 |       24,285.69 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,724,016.00 |    2,393,785.82 |          417.75 |
|               2 |    2,724,016.00 |    2,213,741.50 |          451.72 |
|               3 |    2,724,016.00 |    2,273,096.60 |          439.93 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           83.00 |           72.94 |   13,710,278.31 |
|               2 |           83.00 |           67.45 |   14,825,339.76 |
|               3 |           83.00 |           69.26 |   14,438,220.48 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,137,953,100.00 |
|               2 |            0.00 |            0.00 |1,230,503,200.00 |
|               3 |            0.00 |            0.00 |1,198,372,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,137,953,100.00 |
|               2 |            0.00 |            0.00 |1,230,503,200.00 |
|               3 |            0.00 |            0.00 |1,198,372,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,138.00 |        1,000.04 |      999,958.79 |
|               2 |        1,230.00 |          999.59 |    1,000,409.11 |
|               3 |        1,198.00 |          999.69 |    1,000,310.77 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      721,056.00 |      633,642.99 |        1,578.18 |
|               2 |      721,056.00 |      585,984.66 |        1,706.53 |
|               3 |      721,056.00 |      601,696.15 |        1,661.97 |


