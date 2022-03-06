# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_03/06/2022 23:02:47_
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
|    Elapsed Time |              ms |        1,007.00 |        1,006.00 |        1,005.00 |            1.00 |
|[Counter] _wordsChecked |      operations |    1,317,792.00 |    1,317,792.00 |    1,317,792.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.03 |          999.78 |          999.65 |            0.21 |
|[Counter] _wordsChecked |      operations |    1,310,809.19 |    1,309,649.63 |    1,308,672.26 |        1,080.05 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,005,327,100.00 |
|               2 |            0.00 |            0.00 |1,006,968,700.00 |
|               3 |            0.00 |            0.00 |1,006,357,200.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,005,327,100.00 |
|               2 |            0.00 |            0.00 |1,006,968,700.00 |
|               3 |            0.00 |            0.00 |1,006,357,200.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,005,327,100.00 |
|               2 |            0.00 |            0.00 |1,006,968,700.00 |
|               3 |            0.00 |            0.00 |1,006,357,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,005,327,100.00 |
|               2 |            0.00 |            0.00 |1,006,968,700.00 |
|               3 |            0.00 |            0.00 |1,006,357,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,005.00 |          999.67 |    1,000,325.47 |
|               2 |        1,007.00 |        1,000.03 |      999,968.92 |
|               3 |        1,006.00 |          999.65 |    1,000,355.07 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,317,792.00 |    1,310,809.19 |          762.89 |
|               2 |    1,317,792.00 |    1,308,672.26 |          764.13 |
|               3 |    1,317,792.00 |    1,309,467.45 |          763.67 |


