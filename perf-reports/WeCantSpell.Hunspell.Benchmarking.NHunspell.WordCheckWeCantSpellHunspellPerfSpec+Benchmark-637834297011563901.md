# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/21/2022 03:21:41_
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
|TotalBytesAllocated |           bytes |    3,538,856.00 |    3,538,856.00 |    3,538,856.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           61.00 |           61.00 |           61.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,010.00 |        1,005.33 |        1,002.00 |            4.16 |
|[Counter] _wordsChecked |      operations |      605,024.00 |      605,024.00 |      605,024.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,531,797.00 |    3,520,271.40 |    3,503,934.04 |       14,541.35 |
|TotalCollections [Gen0] |     collections |           60.88 |           60.68 |           60.40 |            0.25 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.09 |        1,000.04 |        1,000.00 |            0.05 |
|[Counter] _wordsChecked |      operations |      603,817.15 |      601,846.66 |      599,053.53 |        2,486.08 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,538,856.00 |    3,531,797.00 |          283.14 |
|               2 |    3,538,856.00 |    3,503,934.04 |          285.39 |
|               3 |    3,538,856.00 |    3,525,083.15 |          283.68 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           61.00 |           60.88 |   16,426,208.20 |
|               2 |           61.00 |           60.40 |   16,556,827.87 |
|               3 |           61.00 |           60.76 |   16,457,493.44 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,001,998,700.00 |
|               2 |            0.00 |            0.00 |1,009,966,500.00 |
|               3 |            0.00 |            0.00 |1,003,907,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,001,998,700.00 |
|               2 |            0.00 |            0.00 |1,009,966,500.00 |
|               3 |            0.00 |            0.00 |1,003,907,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,002.00 |        1,000.00 |      999,998.70 |
|               2 |        1,010.00 |        1,000.03 |      999,966.83 |
|               3 |        1,004.00 |        1,000.09 |      999,907.47 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      605,024.00 |      603,817.15 |        1,656.13 |
|               2 |      605,024.00 |      599,053.53 |        1,669.30 |
|               3 |      605,024.00 |      602,669.31 |        1,659.28 |


