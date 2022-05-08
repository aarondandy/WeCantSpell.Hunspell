# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_05/08/2022 21:00:43_
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
|TotalBytesAllocated |           bytes |    5,645,056.00 |    5,645,056.00 |    5,645,056.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            9.00 |            9.00 |            9.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,006.00 |        1,004.33 |        1,003.00 |            1.53 |
|[Counter] _wordsChecked |      operations |      671,328.00 |      671,328.00 |      671,328.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,623,771.71 |    5,620,162.79 |    5,613,386.40 |        5,872.68 |
|TotalCollections [Gen0] |     collections |            8.97 |            8.96 |            8.95 |            0.01 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.36 |          999.90 |          999.14 |            0.67 |
|[Counter] _wordsChecked |      operations |      668,796.80 |      668,367.62 |      667,561.75 |          698.40 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    5,645,056.00 |    5,623,330.26 |          177.83 |
|               2 |    5,645,056.00 |    5,623,771.71 |          177.82 |
|               3 |    5,645,056.00 |    5,613,386.40 |          178.15 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            9.00 |            8.97 |  111,540,388.89 |
|               2 |            9.00 |            8.97 |  111,531,633.33 |
|               3 |            9.00 |            8.95 |  111,737,977.78 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,003,863,500.00 |
|               2 |            0.00 |            0.00 |1,003,784,700.00 |
|               3 |            0.00 |            0.00 |1,005,641,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,003,863,500.00 |
|               2 |            0.00 |            0.00 |1,003,784,700.00 |
|               3 |            0.00 |            0.00 |1,005,641,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,003.00 |          999.14 |    1,000,860.92 |
|               2 |        1,004.00 |        1,000.21 |      999,785.56 |
|               3 |        1,006.00 |        1,000.36 |      999,643.94 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      671,328.00 |      668,744.31 |        1,495.34 |
|               2 |      671,328.00 |      668,796.80 |        1,495.22 |
|               3 |      671,328.00 |      667,561.75 |        1,497.99 |


