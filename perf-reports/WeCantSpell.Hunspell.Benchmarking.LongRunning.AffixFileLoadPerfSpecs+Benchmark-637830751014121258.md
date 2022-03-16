# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_3/17/2022 12:51:41 AM_
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
|TotalBytesAllocated |           bytes |   39,767,432.00 |   39,767,432.00 |   39,767,432.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           98.00 |           98.00 |           98.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           39.00 |           39.00 |           39.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           11.00 |           11.00 |           11.00 |            0.00 |
|    Elapsed Time |              ms |        1,357.00 |        1,357.00 |        1,357.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          177.00 |          177.00 |          177.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   29,308,850.18 |   29,308,850.18 |   29,308,850.18 |            0.00 |
|TotalCollections [Gen0] |     collections |           72.23 |           72.23 |           72.23 |            0.00 |
|TotalCollections [Gen1] |     collections |           28.74 |           28.74 |           28.74 |            0.00 |
|TotalCollections [Gen2] |     collections |            8.11 |            8.11 |            8.11 |            0.00 |
|    Elapsed Time |              ms |        1,000.12 |        1,000.12 |        1,000.12 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          130.45 |          130.45 |          130.45 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   39,767,432.00 |   29,308,850.18 |           34.12 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           98.00 |           72.23 |   13,845,310.20 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           39.00 |           28.74 |   34,790,779.49 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           11.00 |            8.11 |  123,349,127.27 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,357.00 |        1,000.12 |      999,882.39 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          177.00 |          130.45 |    7,665,764.97 |


