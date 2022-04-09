# WeCantSpell.Hunspell.Benchmarking.NHunspell.WordCheckWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project check English (US) words?__
_04/09/2022 14:19:16_
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
|TotalBytesAllocated |           bytes |    4,489,240.00 |    4,489,240.00 |    4,489,240.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           60.00 |           60.00 |           60.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          919.00 |          916.67 |          915.00 |            2.08 |
|[Counter] _wordsChecked |      operations |      596,736.00 |      596,736.00 |      596,736.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,909,377.67 |    4,897,634.92 |    4,881,140.95 |       14,705.74 |
|TotalCollections [Gen0] |     collections |           65.62 |           65.46 |           65.24 |            0.20 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.63 |        1,000.05 |          999.23 |            0.73 |
|[Counter] _wordsChecked |      operations |      652,583.15 |      651,022.24 |      648,829.76 |        1,954.77 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    4,489,240.00 |    4,909,377.67 |          203.69 |
|               2 |    4,489,240.00 |    4,902,386.14 |          203.98 |
|               3 |    4,489,240.00 |    4,881,140.95 |          204.87 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           60.00 |           65.62 |   15,240,356.67 |
|               2 |           60.00 |           65.52 |   15,262,091.67 |
|               3 |           60.00 |           65.24 |   15,328,520.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  914,421,400.00 |
|               2 |            0.00 |            0.00 |  915,725,500.00 |
|               3 |            0.00 |            0.00 |  919,711,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  914,421,400.00 |
|               2 |            0.00 |            0.00 |  915,725,500.00 |
|               3 |            0.00 |            0.00 |  919,711,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          915.00 |        1,000.63 |      999,367.65 |
|               2 |          916.00 |        1,000.30 |      999,700.33 |
|               3 |          919.00 |          999.23 |    1,000,773.88 |

#### [Counter] _wordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      596,736.00 |      652,583.15 |        1,532.37 |
|               2 |      596,736.00 |      651,653.80 |        1,534.56 |
|               3 |      596,736.00 |      648,829.76 |        1,541.24 |


