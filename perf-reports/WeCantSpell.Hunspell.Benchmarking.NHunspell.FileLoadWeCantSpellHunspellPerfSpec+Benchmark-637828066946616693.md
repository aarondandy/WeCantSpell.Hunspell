# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/13/2022 22:18:14_
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
|TotalBytesAllocated |           bytes |   90,006,624.00 |   90,006,068.00 |   90,005,512.00 |          786.30 |
|TotalCollections [Gen0] |     collections |          484.00 |          483.50 |          483.00 |            0.71 |
|TotalCollections [Gen1] |     collections |          186.00 |          185.50 |          185.00 |            0.71 |
|TotalCollections [Gen2] |     collections |           44.00 |           43.50 |           43.00 |            0.71 |
|    Elapsed Time |              ms |       15,684.00 |       15,571.00 |       15,458.00 |          159.81 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,822,701.66 |    5,780,883.11 |    5,739,064.55 |       59,140.37 |
|TotalCollections [Gen0] |     collections |           31.25 |           31.05 |           30.86 |            0.27 |
|TotalCollections [Gen1] |     collections |           11.97 |           11.91 |           11.86 |            0.08 |
|TotalCollections [Gen2] |     collections |            2.81 |            2.79 |            2.78 |            0.02 |
|    Elapsed Time |              ms |        1,000.05 |        1,000.04 |        1,000.02 |            0.02 |
|[Counter] FilePairsLoaded |      operations |            3.82 |            3.79 |            3.76 |            0.04 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   90,005,512.00 |    5,822,701.66 |          171.74 |
|               2 |   90,006,624.00 |    5,739,064.55 |          174.24 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          483.00 |           31.25 |   32,003,497.10 |
|               2 |          484.00 |           30.86 |   32,403,207.44 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          185.00 |           11.97 |   83,555,076.22 |
|               2 |          186.00 |           11.86 |   84,318,023.66 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           43.00 |            2.78 |  359,481,141.86 |
|               2 |           44.00 |            2.81 |  356,435,281.82 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       15,458.00 |        1,000.02 |      999,979.89 |
|               2 |       15,684.00 |        1,000.05 |      999,945.96 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.82 |  261,994,730.51 |
|               2 |           59.00 |            3.76 |  265,816,142.37 |


