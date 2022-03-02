# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/02/2022 03:07:37_
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
|    Elapsed Time |              ms |        4,027.00 |        4,015.50 |        4,004.00 |           16.26 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,383,759.44 |   22,320,821.92 |   22,257,884.39 |       89,007.10 |
|TotalCollections [Gen0] |     collections |            3.25 |            3.24 |            3.23 |            0.01 |
|TotalCollections [Gen1] |     collections |            3.25 |            3.24 |            3.23 |            0.01 |
|TotalCollections [Gen2] |     collections |            3.25 |            3.24 |            3.23 |            0.01 |
|    Elapsed Time |              ms |        1,000.09 |        1,000.05 |        1,000.00 |            0.06 |
|[Counter] FilePairsLoaded |      operations |           14.74 |           14.69 |           14.65 |            0.06 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,383,759.44 |           44.68 |
|               2 |   89,624,176.00 |   22,257,884.39 |           44.93 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.25 |  307,998,553.85 |
|               2 |           13.00 |            3.23 |  309,740,461.54 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.25 |  307,998,553.85 |
|               2 |           13.00 |            3.23 |  309,740,461.54 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.25 |  307,998,553.85 |
|               2 |           13.00 |            3.23 |  309,740,461.54 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        4,004.00 |        1,000.00 |      999,995.30 |
|               2 |        4,027.00 |        1,000.09 |      999,907.13 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.74 |   67,864,088.14 |
|               2 |           59.00 |           14.65 |   68,247,898.31 |


