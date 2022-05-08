# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_05/08/2022 22:38:03_
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
|TotalBytesAllocated |           bytes |   79,392,472.00 |   79,392,472.00 |   79,392,472.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          352.00 |          352.00 |          352.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          136.00 |          136.00 |          136.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           33.00 |           33.00 |           33.00 |            0.00 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,410,195.93 |    4,410,195.93 |    4,410,195.93 |            0.00 |
|TotalCollections [Gen0] |     collections |           19.55 |           19.55 |           19.55 |            0.00 |
|TotalCollections [Gen1] |     collections |            7.55 |            7.55 |            7.55 |            0.00 |
|TotalCollections [Gen2] |     collections |            1.83 |            1.83 |            1.83 |            0.00 |
|[Counter] FilePairsLoaded |      operations |            3.28 |            3.28 |            3.28 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   79,392,472.00 |    4,410,195.93 |          226.75 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          352.00 |           19.55 |   51,142,125.85 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          136.00 |            7.55 |  132,367,855.15 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           33.00 |            1.83 |  545,516,009.09 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.28 |  305,119,123.73 |


