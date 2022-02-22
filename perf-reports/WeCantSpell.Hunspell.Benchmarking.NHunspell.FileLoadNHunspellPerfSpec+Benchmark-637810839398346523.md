# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_02/21/2022 23:45:39_
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
|    Elapsed Time |              ms |        4,001.00 |        3,995.50 |        3,990.00 |            7.78 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,463,468.10 |   22,429,878.98 |   22,396,289.86 |       47,502.19 |
|TotalCollections [Gen0] |     collections |            3.26 |            3.25 |            3.25 |            0.01 |
|TotalCollections [Gen1] |     collections |            3.26 |            3.25 |            3.25 |            0.01 |
|TotalCollections [Gen2] |     collections |            3.26 |            3.25 |            3.25 |            0.01 |
|    Elapsed Time |              ms |        1,000.06 |          999.94 |          999.81 |            0.17 |
|[Counter] FilePairsLoaded |      operations |           14.79 |           14.77 |           14.74 |            0.03 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,463,468.10 |           44.52 |
|               2 |   89,624,176.00 |   22,396,289.86 |           44.65 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.26 |  306,905,661.54 |
|               2 |           13.00 |            3.25 |  307,826,315.38 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.26 |  306,905,661.54 |
|               2 |           13.00 |            3.25 |  307,826,315.38 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.26 |  306,905,661.54 |
|               2 |           13.00 |            3.25 |  307,826,315.38 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        3,990.00 |        1,000.06 |      999,943.26 |
|               2 |        4,001.00 |          999.81 |    1,000,185.48 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.79 |   67,623,281.36 |
|               2 |           59.00 |           14.74 |   67,826,137.29 |


