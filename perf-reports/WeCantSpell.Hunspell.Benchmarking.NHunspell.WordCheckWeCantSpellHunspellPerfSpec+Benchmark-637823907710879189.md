# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/09/2022 02:46:11_
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
|TotalBytesAllocated |           bytes |    4,982,176.00 |    4,982,176.00 |    4,982,176.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           78.00 |           78.00 |           78.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,069.00 |        1,040.67 |        1,008.00 |           30.73 |
|[Counter] _wordsChecked |      operations |      679,616.00 |      679,616.00 |      679,616.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,944,204.02 |    4,791,574.72 |    4,662,476.51 |      142,330.18 |
|TotalCollections [Gen0] |     collections |           77.41 |           75.02 |           72.99 |            2.23 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.40 |        1,000.27 |        1,000.09 |            0.16 |
|[Counter] _wordsChecked |      operations |      674,436.26 |      653,616.18 |      636,005.96 |       19,415.19 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    4,982,176.00 |    4,944,204.02 |          202.26 |
|               2 |    4,982,176.00 |    4,662,476.51 |          214.48 |
|               3 |    4,982,176.00 |    4,768,043.64 |          209.73 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           78.00 |           77.41 |   12,918,975.64 |
|               2 |           78.00 |           72.99 |   13,699,597.44 |
|               3 |           78.00 |           74.65 |   13,396,280.77 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,007,680,100.00 |
|               2 |            0.00 |            0.00 |1,068,568,600.00 |
|               3 |            0.00 |            0.00 |1,044,909,900.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,007,680,100.00 |
|               2 |            0.00 |            0.00 |1,068,568,600.00 |
|               3 |            0.00 |            0.00 |1,044,909,900.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,008.00 |        1,000.32 |      999,682.64 |
|               2 |        1,069.00 |        1,000.40 |      999,596.45 |
|               3 |        1,045.00 |        1,000.09 |      999,913.78 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      679,616.00 |      674,436.26 |        1,482.72 |
|               2 |      679,616.00 |      636,005.96 |        1,572.31 |
|               3 |      679,616.00 |      650,406.32 |        1,537.50 |


