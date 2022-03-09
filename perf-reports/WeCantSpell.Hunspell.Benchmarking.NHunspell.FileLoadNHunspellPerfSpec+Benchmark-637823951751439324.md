# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/09/2022 03:59:35_
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
|    Elapsed Time |              ms |        4,055.00 |        4,035.50 |        4,016.00 |           27.58 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,320,327.94 |   22,212,129.37 |   22,103,930.79 |      153,015.89 |
|TotalCollections [Gen0] |     collections |            3.24 |            3.22 |            3.21 |            0.02 |
|TotalCollections [Gen1] |     collections |            3.24 |            3.22 |            3.21 |            0.02 |
|TotalCollections [Gen2] |     collections |            3.24 |            3.22 |            3.21 |            0.02 |
|    Elapsed Time |              ms |        1,000.16 |        1,000.12 |        1,000.08 |            0.06 |
|[Counter] FilePairsLoaded |      operations |           14.69 |           14.62 |           14.55 |            0.10 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,320,327.94 |           44.80 |
|               2 |   89,624,176.00 |   22,103,930.79 |           45.24 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.24 |  308,873,846.15 |
|               2 |           13.00 |            3.21 |  311,897,800.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.24 |  308,873,846.15 |
|               2 |           13.00 |            3.21 |  311,897,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.24 |  308,873,846.15 |
|               2 |           13.00 |            3.21 |  311,897,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        4,016.00 |        1,000.16 |      999,840.64 |
|               2 |        4,055.00 |        1,000.08 |      999,918.96 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.69 |   68,056,949.15 |
|               2 |           59.00 |           14.55 |   68,723,244.07 |


