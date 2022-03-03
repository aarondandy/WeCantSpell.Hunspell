# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_03/03/2022 04:12:36_
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
|TotalBytesAllocated |           bytes |    5,645,816.00 |    5,645,816.00 |    5,645,816.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           75.00 |           75.00 |           75.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,141.00 |        1,060.67 |        1,019.00 |           69.59 |
|[Counter] _wordsChecked |      operations |      654,752.00 |      654,752.00 |      654,752.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,541,184.04 |    5,337,816.66 |    4,948,222.52 |      337,507.26 |
|TotalCollections [Gen0] |     collections |           73.61 |           70.91 |           65.73 |            4.48 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.12 |        1,000.03 |          999.96 |            0.08 |
|[Counter] _wordsChecked |      operations |      642,617.71 |      619,032.95 |      573,851.25 |       39,141.12 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    5,645,816.00 |    5,541,184.04 |          180.47 |
|               2 |    5,645,816.00 |    5,524,043.43 |          181.03 |
|               3 |    5,645,816.00 |    4,948,222.52 |          202.09 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           75.00 |           73.61 |   13,585,101.33 |
|               2 |           75.00 |           73.38 |   13,627,254.67 |
|               3 |           75.00 |           65.73 |   15,213,048.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,018,882,600.00 |
|               2 |            0.00 |            0.00 |1,022,044,100.00 |
|               3 |            0.00 |            0.00 |1,140,978,600.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,018,882,600.00 |
|               2 |            0.00 |            0.00 |1,022,044,100.00 |
|               3 |            0.00 |            0.00 |1,140,978,600.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,019.00 |        1,000.12 |      999,884.79 |
|               2 |        1,022.00 |          999.96 |    1,000,043.15 |
|               3 |        1,141.00 |        1,000.02 |      999,981.24 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      654,752.00 |      642,617.71 |        1,556.14 |
|               2 |      654,752.00 |      640,629.89 |        1,560.96 |
|               3 |      654,752.00 |      573,851.25 |        1,742.61 |


