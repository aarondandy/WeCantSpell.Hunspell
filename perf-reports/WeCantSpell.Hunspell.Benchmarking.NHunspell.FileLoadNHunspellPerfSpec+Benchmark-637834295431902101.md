# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/21/2022 03:19:03_
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
|TotalBytesAllocated |           bytes |   89,624,184.00 |   89,624,180.00 |   89,624,176.00 |            5.66 |
|TotalCollections [Gen0] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|    Elapsed Time |              ms |        4,022.00 |        4,011.50 |        4,001.00 |           14.85 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,397,996.40 |   22,341,393.48 |   22,284,790.55 |       80,048.62 |
|TotalCollections [Gen0] |     collections |            3.25 |            3.24 |            3.23 |            0.01 |
|TotalCollections [Gen1] |     collections |            3.25 |            3.24 |            3.23 |            0.01 |
|TotalCollections [Gen2] |     collections |            3.25 |            3.24 |            3.23 |            0.01 |
|    Elapsed Time |              ms |        1,000.06 |          999.97 |          999.89 |            0.12 |
|[Counter] FilePairsLoaded |      operations |           14.74 |           14.71 |           14.67 |            0.05 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,184.00 |   22,284,790.55 |           44.87 |
|               2 |   89,624,176.00 |   22,397,996.40 |           44.65 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.23 |  309,366,515.38 |
|               2 |           13.00 |            3.25 |  307,802,861.54 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.23 |  309,366,515.38 |
|               2 |           13.00 |            3.25 |  307,802,861.54 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.23 |  309,366,515.38 |
|               2 |           13.00 |            3.25 |  307,802,861.54 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        4,022.00 |        1,000.06 |      999,941.50 |
|               2 |        4,001.00 |          999.89 |    1,000,109.27 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.67 |   68,165,503.39 |
|               2 |           59.00 |           14.74 |   67,820,969.49 |


