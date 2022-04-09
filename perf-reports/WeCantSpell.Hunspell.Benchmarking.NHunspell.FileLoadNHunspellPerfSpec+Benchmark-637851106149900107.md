# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_04/09/2022 14:16:54_
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
NumberOfIterations=2, MaximumRunTime=00:00:01
Concurrent=False
Tracing=False
```

## Data
-------------------

### Totals
|          Metric |           Units |             Max |         Average |             Min |          StdDev |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   89,624,152.00 |   46,603,708.00 |    3,583,264.00 |   60,840,095.36 |
|TotalCollections [Gen0] |     collections |           17.00 |           15.00 |           13.00 |            2.83 |
|TotalCollections [Gen1] |     collections |           17.00 |           15.00 |           13.00 |            2.83 |
|TotalCollections [Gen2] |     collections |           17.00 |           15.00 |           13.00 |            2.83 |
|    Elapsed Time |              ms |        3,934.00 |        3,930.00 |        3,926.00 |            5.66 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,829,711.79 |   11,870,218.82 |      910,725.86 |   15,499,063.59 |
|TotalCollections [Gen0] |     collections |            4.32 |            3.82 |            3.31 |            0.71 |
|TotalCollections [Gen1] |     collections |            4.32 |            3.82 |            3.31 |            0.71 |
|TotalCollections [Gen2] |     collections |            4.32 |            3.82 |            3.31 |            0.71 |
|    Elapsed Time |              ms |        1,000.06 |          999.96 |          999.87 |            0.13 |
|[Counter] FilePairsLoaded |      operations |           15.03 |           15.01 |           15.00 |            0.02 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,829,711.79 |           43.80 |
|               2 |    3,583,264.00 |      910,725.86 |        1,098.03 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.31 |  301,982,153.85 |
|               2 |           17.00 |            4.32 |  231,442,023.53 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.31 |  301,982,153.85 |
|               2 |           17.00 |            4.32 |  231,442,023.53 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.31 |  301,982,153.85 |
|               2 |           17.00 |            4.32 |  231,442,023.53 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        3,926.00 |        1,000.06 |      999,940.91 |
|               2 |        3,934.00 |          999.87 |    1,000,130.76 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           15.03 |   66,538,440.68 |
|               2 |           59.00 |           15.00 |   66,686,684.75 |


