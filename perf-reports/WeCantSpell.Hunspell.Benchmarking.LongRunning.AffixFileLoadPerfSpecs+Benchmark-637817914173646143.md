# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_3/2/2022 4:16:57 AM_
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
|TotalBytesAllocated |           bytes |   11,938,232.00 |   11,938,232.00 |   11,938,232.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           49.00 |           49.00 |           49.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           21.00 |           21.00 |           21.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            8.00 |            8.00 |            8.00 |            0.00 |
|    Elapsed Time |              ms |        1,452.00 |        1,452.00 |        1,452.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,226,513.74 |    8,226,513.74 |    8,226,513.74 |            0.00 |
|TotalCollections [Gen0] |     collections |           33.77 |           33.77 |           33.77 |            0.00 |
|TotalCollections [Gen1] |     collections |           14.47 |           14.47 |           14.47 |            0.00 |
|TotalCollections [Gen2] |     collections |            5.51 |            5.51 |            5.51 |            0.00 |
|    Elapsed Time |              ms |        1,000.56 |        1,000.56 |        1,000.56 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           40.66 |           40.66 |           40.66 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   11,938,232.00 |    8,226,513.74 |          121.56 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           49.00 |           33.77 |   29,616,116.33 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           21.00 |           14.47 |   69,104,271.43 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            8.00 |            5.51 |  181,398,712.50 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,452.00 |        1,000.56 |      999,441.94 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           40.66 |   24,596,435.59 |


