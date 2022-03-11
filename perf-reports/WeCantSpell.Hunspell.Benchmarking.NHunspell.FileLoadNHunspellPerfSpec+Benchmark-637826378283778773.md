# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/11/2022 23:23:48_
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
|    Elapsed Time |              ms |        4,051.00 |        4,038.50 |        4,026.00 |           17.68 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,260,011.77 |   22,193,684.17 |   22,127,356.58 |       93,801.38 |
|TotalCollections [Gen0] |     collections |            3.23 |            3.22 |            3.21 |            0.01 |
|TotalCollections [Gen1] |     collections |            3.23 |            3.22 |            3.21 |            0.01 |
|TotalCollections [Gen2] |     collections |            3.23 |            3.22 |            3.21 |            0.01 |
|    Elapsed Time |              ms |        1,000.15 |        1,000.05 |          999.94 |            0.15 |
|[Counter] FilePairsLoaded |      operations |           14.65 |           14.61 |           14.57 |            0.06 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,260,011.77 |           44.92 |
|               2 |   89,624,176.00 |   22,127,356.58 |           45.19 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.23 |  309,710,776.92 |
|               2 |           13.00 |            3.21 |  311,567,600.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.23 |  309,710,776.92 |
|               2 |           13.00 |            3.21 |  311,567,600.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.23 |  309,710,776.92 |
|               2 |           13.00 |            3.21 |  311,567,600.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        4,026.00 |          999.94 |    1,000,059.64 |
|               2 |        4,051.00 |        1,000.15 |      999,846.66 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.65 |   68,241,357.63 |
|               2 |           59.00 |           14.57 |   68,650,488.14 |


