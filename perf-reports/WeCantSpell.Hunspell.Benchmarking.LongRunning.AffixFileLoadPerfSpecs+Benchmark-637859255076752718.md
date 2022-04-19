# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_4/19/2022 12:38:27 AM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.4,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |   44,555,128.00 |   44,555,128.00 |   44,555,128.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           96.00 |           96.00 |           96.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           36.00 |           36.00 |           36.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|    Elapsed Time |              ms |        1,342.00 |        1,342.00 |        1,342.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          177.00 |          177.00 |          177.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   33,200,159.01 |   33,200,159.01 |   33,200,159.01 |            0.00 |
|TotalCollections [Gen0] |     collections |           71.53 |           71.53 |           71.53 |            0.00 |
|TotalCollections [Gen1] |     collections |           26.83 |           26.83 |           26.83 |            0.00 |
|TotalCollections [Gen2] |     collections |            9.69 |            9.69 |            9.69 |            0.00 |
|    Elapsed Time |              ms |          999.99 |          999.99 |          999.99 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          131.89 |          131.89 |          131.89 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   44,555,128.00 |   33,200,159.01 |           30.12 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           96.00 |           71.53 |   13,979,328.12 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           36.00 |           26.83 |   37,278,208.33 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            9.69 |  103,231,961.54 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,342.00 |          999.99 |    1,000,011.55 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          177.00 |          131.89 |    7,582,008.47 |


