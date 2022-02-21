# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_02/21/2022 18:35:39_
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
|TotalBytesAllocated |           bytes |   89,624,184.00 |   46,748,452.00 |    3,872,720.00 |   60,635,441.69 |
|TotalCollections [Gen0] |     collections |           16.00 |           14.50 |           13.00 |            2.12 |
|TotalCollections [Gen1] |     collections |           16.00 |           14.50 |           13.00 |            2.12 |
|TotalCollections [Gen2] |     collections |           16.00 |           14.50 |           13.00 |            2.12 |
|    Elapsed Time |              ms |        4,113.00 |        4,056.50 |        4,000.00 |           79.90 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,403,894.11 |   11,672,701.83 |      941,509.55 |   15,176,197.66 |
|TotalCollections [Gen0] |     collections |            3.89 |            3.57 |            3.25 |            0.45 |
|TotalCollections [Gen1] |     collections |            3.89 |            3.57 |            3.25 |            0.45 |
|TotalCollections [Gen2] |     collections |            3.89 |            3.57 |            3.25 |            0.45 |
|    Elapsed Time |              ms |          999.92 |          999.91 |          999.90 |            0.01 |
|[Counter] FilePairsLoaded |      operations |           14.75 |           14.55 |           14.34 |            0.29 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,184.00 |   22,403,894.11 |           44.64 |
|               2 |    3,872,720.00 |      941,509.55 |        1,062.12 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.25 |  307,721,861.54 |
|               2 |           16.00 |            3.89 |  257,081,831.25 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.25 |  307,721,861.54 |
|               2 |           16.00 |            3.89 |  257,081,831.25 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.25 |  307,721,861.54 |
|               2 |           16.00 |            3.89 |  257,081,831.25 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        4,000.00 |          999.90 |    1,000,096.05 |
|               2 |        4,113.00 |          999.92 |    1,000,075.20 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.75 |   67,803,122.03 |
|               2 |           59.00 |           14.34 |   69,717,106.78 |


