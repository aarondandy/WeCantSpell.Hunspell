# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_4/6/2022 8:01:10 PM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.3,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |   44,629,912.00 |   44,629,912.00 |   44,629,912.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           96.00 |           96.00 |           96.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           37.00 |           37.00 |           37.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|    Elapsed Time |              ms |        1,355.00 |        1,355.00 |        1,355.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          177.00 |          177.00 |          177.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   32,936,808.21 |   32,936,808.21 |   32,936,808.21 |            0.00 |
|TotalCollections [Gen0] |     collections |           70.85 |           70.85 |           70.85 |            0.00 |
|TotalCollections [Gen1] |     collections |           27.31 |           27.31 |           27.31 |            0.00 |
|TotalCollections [Gen2] |     collections |            9.59 |            9.59 |            9.59 |            0.00 |
|    Elapsed Time |              ms |          999.99 |          999.99 |          999.99 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          130.63 |          130.63 |          130.63 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   44,629,912.00 |   32,936,808.21 |           30.36 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           96.00 |           70.85 |   14,114,753.12 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           37.00 |           27.31 |   36,622,062.16 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            9.59 |  104,232,023.08 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,355.00 |          999.99 |    1,000,012.03 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          177.00 |          130.63 |    7,655,459.32 |


