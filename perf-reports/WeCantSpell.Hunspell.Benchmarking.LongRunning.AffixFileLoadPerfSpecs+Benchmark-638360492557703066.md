# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_11/20/2023 3:54:15 AM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.22631.0
ProcessorCount=16
CLR=6.0.25,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |    2,023,296.00 |    2,023,296.00 |    2,023,296.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           22.00 |           22.00 |           22.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           12.00 |           12.00 |           12.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            4.00 |            4.00 |            4.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,771,423.96 |    1,771,423.96 |    1,771,423.96 |            0.00 |
|TotalCollections [Gen0] |     collections |           19.26 |           19.26 |           19.26 |            0.00 |
|TotalCollections [Gen1] |     collections |           10.51 |           10.51 |           10.51 |            0.00 |
|TotalCollections [Gen2] |     collections |            3.50 |            3.50 |            3.50 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           51.66 |           51.66 |           51.66 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,023,296.00 |    1,771,423.96 |          564.52 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           22.00 |           19.26 |   51,917,554.55 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           12.00 |           10.51 |   95,182,183.33 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            4.00 |            3.50 |  285,546,550.00 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           51.66 |   19,359,088.14 |


