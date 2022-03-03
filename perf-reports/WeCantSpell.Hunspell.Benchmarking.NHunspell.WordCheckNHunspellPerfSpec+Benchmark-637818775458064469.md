# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckNHunspellPerfSpec+Benchmark
__How fast can NHunspell check English (US) words?__
_03/03/2022 04:12:25_
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
|    Elapsed Time |              ms |        1,012.00 |        1,009.67 |        1,007.00 |            2.52 |
|[Counter] _wordsChecked |      operations |    1,317,792.00 |    1,317,792.00 |    1,317,792.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.11 |          999.91 |          999.70 |            0.21 |
|[Counter] _wordsChecked |      operations |    1,308,515.28 |    1,305,057.21 |    1,301,769.05 |        3,376.33 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,012,308,600.00 |
|               2 |            0.00 |            0.00 |1,009,889,500.00 |
|               3 |            0.00 |            0.00 |1,007,089,500.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,012,308,600.00 |
|               2 |            0.00 |            0.00 |1,009,889,500.00 |
|               3 |            0.00 |            0.00 |1,007,089,500.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,012,308,600.00 |
|               2 |            0.00 |            0.00 |1,009,889,500.00 |
|               3 |            0.00 |            0.00 |1,007,089,500.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,012,308,600.00 |
|               2 |            0.00 |            0.00 |1,009,889,500.00 |
|               3 |            0.00 |            0.00 |1,007,089,500.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,012.00 |          999.70 |    1,000,304.94 |
|               2 |        1,010.00 |        1,000.11 |      999,890.59 |
|               3 |        1,007.00 |          999.91 |    1,000,088.88 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,317,792.00 |    1,301,769.05 |          768.19 |
|               2 |    1,317,792.00 |    1,304,887.32 |          766.35 |
|               3 |    1,317,792.00 |    1,308,515.28 |          764.22 |


