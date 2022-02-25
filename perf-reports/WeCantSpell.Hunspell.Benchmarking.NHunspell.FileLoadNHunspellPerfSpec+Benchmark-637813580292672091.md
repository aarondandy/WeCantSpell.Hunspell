# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_02/25/2022 03:53:49_
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
|    Elapsed Time |              ms |        4,038.00 |        4,029.00 |        4,020.00 |           12.73 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,294,918.45 |   22,246,240.09 |   22,197,561.73 |       68,841.60 |
|TotalCollections [Gen0] |     collections |            3.23 |            3.23 |            3.22 |            0.01 |
|TotalCollections [Gen1] |     collections |            3.23 |            3.23 |            3.22 |            0.01 |
|TotalCollections [Gen2] |     collections |            3.23 |            3.23 |            3.22 |            0.01 |
|    Elapsed Time |              ms |        1,000.11 |        1,000.06 |        1,000.02 |            0.06 |
|[Counter] FilePairsLoaded |      operations |           14.68 |           14.64 |           14.61 |            0.05 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,294,918.45 |           44.85 |
|               2 |   89,624,176.00 |   22,197,561.73 |           45.05 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.23 |  309,225,869.23 |
|               2 |           13.00 |            3.22 |  310,582,192.31 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.23 |  309,225,869.23 |
|               2 |           13.00 |            3.22 |  310,582,192.31 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.23 |  309,225,869.23 |
|               2 |           13.00 |            3.22 |  310,582,192.31 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        4,020.00 |        1,000.02 |      999,984.15 |
|               2 |        4,038.00 |        1,000.11 |      999,893.14 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.68 |   68,134,513.56 |
|               2 |           59.00 |           14.61 |   68,433,364.41 |


