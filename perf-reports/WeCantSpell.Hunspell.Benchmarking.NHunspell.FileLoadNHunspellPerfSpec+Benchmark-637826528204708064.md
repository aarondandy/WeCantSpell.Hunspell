# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/12/2022 03:33:40_
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
|TotalBytesAllocated |           bytes |   89,624,176.00 |   89,624,164.00 |   89,624,152.00 |           16.97 |
|TotalCollections [Gen0] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|    Elapsed Time |              ms |        4,047.00 |        4,014.00 |        3,981.00 |           46.67 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,512,035.16 |   22,330,452.43 |   22,148,869.69 |      256,796.77 |
|TotalCollections [Gen0] |     collections |            3.27 |            3.24 |            3.21 |            0.04 |
|TotalCollections [Gen1] |     collections |            3.27 |            3.24 |            3.21 |            0.04 |
|TotalCollections [Gen2] |     collections |            3.27 |            3.24 |            3.21 |            0.04 |
|    Elapsed Time |              ms |        1,000.14 |        1,000.05 |          999.96 |            0.13 |
|[Counter] FilePairsLoaded |      operations |           14.82 |           14.70 |           14.58 |            0.17 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,148,869.69 |           45.15 |
|               2 |   89,624,176.00 |   22,512,035.16 |           44.42 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.21 |  311,264,892.31 |
|               2 |           13.00 |            3.27 |  306,243,630.77 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.21 |  311,264,892.31 |
|               2 |           13.00 |            3.27 |  306,243,630.77 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.21 |  311,264,892.31 |
|               2 |           13.00 |            3.27 |  306,243,630.77 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        4,047.00 |        1,000.14 |      999,862.52 |
|               2 |        3,981.00 |          999.96 |    1,000,042.00 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.58 |   68,583,789.83 |
|               2 |           59.00 |           14.82 |   67,477,410.17 |


