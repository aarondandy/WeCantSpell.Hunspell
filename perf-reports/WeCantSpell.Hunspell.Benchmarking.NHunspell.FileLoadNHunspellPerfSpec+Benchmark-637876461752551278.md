# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_05/08/2022 22:36:15_
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
NumberOfIterations=1, MaximumRunTime=00:00:01
Concurrent=False
Tracing=False
```

## Data
-------------------

### Totals
|          Metric |           Units |             Max |         Average |             Min |          StdDev |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   13,541,344.00 |   13,541,344.00 |   13,541,344.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           16.00 |           16.00 |           16.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           16.00 |           16.00 |           16.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           16.00 |           16.00 |           16.00 |            0.00 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,411,907.08 |    3,411,907.08 |    3,411,907.08 |            0.00 |
|TotalCollections [Gen0] |     collections |            4.03 |            4.03 |            4.03 |            0.00 |
|TotalCollections [Gen1] |     collections |            4.03 |            4.03 |            4.03 |            0.00 |
|TotalCollections [Gen2] |     collections |            4.03 |            4.03 |            4.03 |            0.00 |
|[Counter] FilePairsLoaded |      operations |           14.87 |           14.87 |           14.87 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   13,541,344.00 |    3,411,907.08 |          293.09 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           16.00 |            4.03 |  248,053,062.50 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           16.00 |            4.03 |  248,053,062.50 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           16.00 |            4.03 |  248,053,062.50 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.87 |   67,268,627.12 |


