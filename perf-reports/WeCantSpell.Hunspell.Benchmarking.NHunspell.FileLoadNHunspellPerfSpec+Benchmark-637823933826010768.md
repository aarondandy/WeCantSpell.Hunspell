# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/09/2022 03:29:42_
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
|TotalBytesAllocated |           bytes |   89,624,152.00 |   46,688,760.00 |    3,753,368.00 |   60,719,813.67 |
|TotalCollections [Gen0] |     collections |           16.00 |           14.50 |           13.00 |            2.12 |
|TotalCollections [Gen1] |     collections |           16.00 |           14.50 |           13.00 |            2.12 |
|TotalCollections [Gen2] |     collections |           16.00 |           14.50 |           13.00 |            2.12 |
|    Elapsed Time |              ms |        4,022.00 |        4,012.50 |        4,003.00 |           13.44 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,390,102.40 |   11,661,635.85 |      933,169.30 |   15,172,342.90 |
|TotalCollections [Gen0] |     collections |            3.98 |            3.61 |            3.25 |            0.52 |
|TotalCollections [Gen1] |     collections |            3.98 |            3.61 |            3.25 |            0.52 |
|TotalCollections [Gen2] |     collections |            3.98 |            3.61 |            3.25 |            0.52 |
|    Elapsed Time |              ms |        1,000.04 |        1,000.00 |          999.96 |            0.06 |
|[Counter] FilePairsLoaded |      operations |           14.74 |           14.70 |           14.67 |            0.05 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,390,102.40 |           44.66 |
|               2 |    3,753,368.00 |      933,169.30 |        1,071.62 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.25 |  307,911,300.00 |
|               2 |           16.00 |            3.98 |  251,385,787.50 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.25 |  307,911,300.00 |
|               2 |           16.00 |            3.98 |  251,385,787.50 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.25 |  307,911,300.00 |
|               2 |           16.00 |            3.98 |  251,385,787.50 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        4,003.00 |        1,000.04 |      999,961.75 |
|               2 |        4,022.00 |          999.96 |    1,000,042.91 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.74 |   67,844,862.71 |
|               2 |           59.00 |           14.67 |   68,172,416.95 |


