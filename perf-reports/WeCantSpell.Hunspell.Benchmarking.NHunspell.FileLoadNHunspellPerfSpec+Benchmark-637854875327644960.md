# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_04/13/2022 22:58:52_
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
|    Elapsed Time |              ms |        4,029.00 |        4,023.50 |        4,018.00 |            7.78 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,304,252.41 |   22,273,019.36 |   22,241,786.30 |       44,170.21 |
|TotalCollections [Gen0] |     collections |            3.24 |            3.23 |            3.23 |            0.01 |
|TotalCollections [Gen1] |     collections |            3.24 |            3.23 |            3.23 |            0.01 |
|TotalCollections [Gen2] |     collections |            3.24 |            3.23 |            3.23 |            0.01 |
|    Elapsed Time |              ms |          999.94 |          999.90 |          999.87 |            0.05 |
|[Counter] FilePairsLoaded |      operations |           14.68 |           14.66 |           14.64 |            0.03 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,241,786.30 |           44.96 |
|               2 |   89,624,176.00 |   22,304,252.41 |           44.83 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.23 |  309,964,561.54 |
|               2 |           13.00 |            3.24 |  309,096,546.15 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.23 |  309,964,561.54 |
|               2 |           13.00 |            3.24 |  309,096,546.15 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.23 |  309,964,561.54 |
|               2 |           13.00 |            3.24 |  309,096,546.15 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        4,029.00 |          999.87 |    1,000,133.85 |
|               2 |        4,018.00 |          999.94 |    1,000,063.49 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.64 |   68,297,276.27 |
|               2 |           59.00 |           14.68 |   68,106,018.64 |


