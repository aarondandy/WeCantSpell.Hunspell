# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_11/20/2023 03:59:21_
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
|TotalBytesAllocated |           bytes |   79,364,184.00 |   79,364,184.00 |   79,364,184.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          358.00 |          358.00 |          358.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          141.00 |          141.00 |          141.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           39.00 |           39.00 |           39.00 |            0.00 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,283,541.35 |    4,283,541.35 |    4,283,541.35 |            0.00 |
|TotalCollections [Gen0] |     collections |           19.32 |           19.32 |           19.32 |            0.00 |
|TotalCollections [Gen1] |     collections |            7.61 |            7.61 |            7.61 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.10 |            2.10 |            2.10 |            0.00 |
|[Counter] FilePairsLoaded |      operations |            3.18 |            3.18 |            3.18 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   79,364,184.00 |    4,283,541.35 |          233.45 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          358.00 |           19.32 |   51,753,361.73 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          141.00 |            7.61 |  131,402,152.48 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           39.00 |            2.10 |  475,069,320.51 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.18 |  314,028,872.88 |


