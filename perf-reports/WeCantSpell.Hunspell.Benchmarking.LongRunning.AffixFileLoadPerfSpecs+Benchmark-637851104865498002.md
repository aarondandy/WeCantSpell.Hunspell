# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_4/9/2022 2:14:46 PM_
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
|TotalBytesAllocated |           bytes |    4,315,896.00 |    4,315,896.00 |    4,315,896.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           66.00 |           66.00 |           66.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           27.00 |           27.00 |           27.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            7.00 |            7.00 |            7.00 |            0.00 |
|    Elapsed Time |              ms |          950.00 |          950.00 |          950.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          118.00 |          118.00 |          118.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,544,580.66 |    4,544,580.66 |    4,544,580.66 |            0.00 |
|TotalCollections [Gen0] |     collections |           69.50 |           69.50 |           69.50 |            0.00 |
|TotalCollections [Gen1] |     collections |           28.43 |           28.43 |           28.43 |            0.00 |
|TotalCollections [Gen2] |     collections |            7.37 |            7.37 |            7.37 |            0.00 |
|    Elapsed Time |              ms |        1,000.34 |        1,000.34 |        1,000.34 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          124.25 |          124.25 |          124.25 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    4,315,896.00 |    4,544,580.66 |          220.04 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           66.00 |           69.50 |   14,389,086.36 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           27.00 |           28.43 |   35,173,322.22 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            7.00 |            7.37 |  135,668,528.57 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          950.00 |        1,000.34 |      999,662.84 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          118.00 |          124.25 |    8,048,133.05 |


