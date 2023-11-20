# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_11/20/2023 12:50:59 PM_
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
|TotalBytesAllocated |           bytes |    2,019,616.00 |    2,019,616.00 |    2,019,616.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           22.00 |           22.00 |           22.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           12.00 |           12.00 |           12.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            4.00 |            4.00 |            4.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,807,126.32 |    1,807,126.32 |    1,807,126.32 |            0.00 |
|TotalCollections [Gen0] |     collections |           19.69 |           19.69 |           19.69 |            0.00 |
|TotalCollections [Gen1] |     collections |           10.74 |           10.74 |           10.74 |            0.00 |
|TotalCollections [Gen2] |     collections |            3.58 |            3.58 |            3.58 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           52.79 |           52.79 |           52.79 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,019,616.00 |    1,807,126.32 |          553.36 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           22.00 |           19.69 |   50,799,286.36 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           12.00 |           10.74 |   93,132,025.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            4.00 |            3.58 |  279,396,075.00 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           52.79 |   18,942,106.78 |


