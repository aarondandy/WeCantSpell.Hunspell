# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/08/2022 05:23:25_
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
|    Elapsed Time |              ms |        4,051.00 |        4,013.00 |        3,975.00 |           53.74 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,543,962.53 |   11,714,222.45 |      884,482.38 |   15,315,565.29 |
|TotalCollections [Gen0] |     collections |            4.20 |            3.73 |            3.27 |            0.65 |
|TotalCollections [Gen1] |     collections |            4.20 |            3.73 |            3.27 |            0.65 |
|TotalCollections [Gen2] |     collections |            4.20 |            3.73 |            3.27 |            0.65 |
|    Elapsed Time |              ms |          999.94 |          999.90 |          999.87 |            0.05 |
|[Counter] FilePairsLoaded |      operations |           14.84 |           14.70 |           14.56 |            0.20 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,543,962.53 |           44.36 |
|               2 |    3,583,264.00 |      884,482.38 |        1,130.60 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.27 |  305,809,838.46 |
|               2 |           17.00 |            4.20 |  238,309,141.18 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.27 |  305,809,838.46 |
|               2 |           17.00 |            4.20 |  238,309,141.18 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.27 |  305,809,838.46 |
|               2 |           17.00 |            4.20 |  238,309,141.18 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        3,975.00 |          999.87 |    1,000,132.81 |
|               2 |        4,051.00 |          999.94 |    1,000,063.05 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.84 |   67,381,828.81 |
|               2 |           59.00 |           14.56 |   68,665,345.76 |


