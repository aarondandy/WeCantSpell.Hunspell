# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/01/2022 05:23:47_
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
|    Elapsed Time |              ms |        3,946.00 |        3,940.50 |        3,935.00 |            7.78 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,772,093.13 |   22,742,371.86 |   22,712,650.59 |       42,032.23 |
|TotalCollections [Gen0] |     collections |            3.30 |            3.30 |            3.29 |            0.01 |
|TotalCollections [Gen1] |     collections |            3.30 |            3.30 |            3.29 |            0.01 |
|TotalCollections [Gen2] |     collections |            3.30 |            3.30 |            3.29 |            0.01 |
|    Elapsed Time |              ms |        1,000.00 |          999.91 |          999.82 |            0.13 |
|[Counter] FilePairsLoaded |      operations |           14.99 |           14.97 |           14.95 |            0.03 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,772,093.13 |           43.91 |
|               2 |   89,624,176.00 |   22,712,650.59 |           44.03 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.30 |  302,746,238.46 |
|               2 |           13.00 |            3.29 |  303,538,653.85 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.30 |  302,746,238.46 |
|               2 |           13.00 |            3.29 |  303,538,653.85 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.30 |  302,746,238.46 |
|               2 |           13.00 |            3.29 |  303,538,653.85 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        3,935.00 |          999.82 |    1,000,178.17 |
|               2 |        3,946.00 |        1,000.00 |    1,000,000.63 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.99 |   66,706,798.31 |
|               2 |           59.00 |           14.95 |   66,881,398.31 |


