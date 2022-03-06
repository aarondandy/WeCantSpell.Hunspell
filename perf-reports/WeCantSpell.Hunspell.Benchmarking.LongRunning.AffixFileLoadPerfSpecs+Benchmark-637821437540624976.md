# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_3/6/2022 6:09:14 AM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.2,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |       43,256.00 |       43,256.00 |       43,256.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          109.00 |          109.00 |          109.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           38.00 |           38.00 |           38.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           14.00 |           14.00 |           14.00 |            0.00 |
|    Elapsed Time |              ms |        1,426.00 |        1,426.00 |        1,426.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          177.00 |          177.00 |          177.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |       30,343.12 |       30,343.12 |       30,343.12 |            0.00 |
|TotalCollections [Gen0] |     collections |           76.46 |           76.46 |           76.46 |            0.00 |
|TotalCollections [Gen1] |     collections |           26.66 |           26.66 |           26.66 |            0.00 |
|TotalCollections [Gen2] |     collections |            9.82 |            9.82 |            9.82 |            0.00 |
|    Elapsed Time |              ms |        1,000.31 |        1,000.31 |        1,000.31 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          124.16 |          124.16 |          124.16 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       43,256.00 |       30,343.12 |       32,956.40 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          109.00 |           76.46 |   13,078,552.29 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           38.00 |           26.66 |   37,514,794.74 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           14.00 |            9.82 |  101,825,871.43 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,426.00 |        1,000.31 |      999,692.99 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          177.00 |          124.16 |    8,054,023.73 |


