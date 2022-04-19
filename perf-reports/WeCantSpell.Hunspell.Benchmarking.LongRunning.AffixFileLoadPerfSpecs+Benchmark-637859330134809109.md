# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_4/19/2022 2:43:33 AM_
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
|TotalBytesAllocated |           bytes |   13,913,232.00 |   13,913,232.00 |   13,913,232.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           95.00 |           95.00 |           95.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           34.00 |           34.00 |           34.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           11.00 |           11.00 |           11.00 |            0.00 |
|    Elapsed Time |              ms |        1,261.00 |        1,261.00 |        1,261.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          177.00 |          177.00 |          177.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   11,034,615.34 |   11,034,615.34 |   11,034,615.34 |            0.00 |
|TotalCollections [Gen0] |     collections |           75.34 |           75.34 |           75.34 |            0.00 |
|TotalCollections [Gen1] |     collections |           26.97 |           26.97 |           26.97 |            0.00 |
|TotalCollections [Gen2] |     collections |            8.72 |            8.72 |            8.72 |            0.00 |
|    Elapsed Time |              ms |        1,000.10 |        1,000.10 |        1,000.10 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          140.38 |          140.38 |          140.38 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   13,913,232.00 |   11,034,615.34 |           90.62 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           95.00 |           75.34 |   13,272,331.58 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           34.00 |           26.97 |   37,084,455.88 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           11.00 |            8.72 |  114,624,681.82 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,261.00 |        1,000.10 |      999,898.10 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          177.00 |          140.38 |    7,123,567.80 |


