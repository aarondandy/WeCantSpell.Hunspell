# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_3/2/2022 3:04:48 AM_
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
|TotalBytesAllocated |           bytes |   11,914,160.00 |   11,914,160.00 |   11,914,160.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           49.00 |           49.00 |           49.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           21.00 |           21.00 |           21.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            8.00 |            8.00 |            8.00 |            0.00 |
|    Elapsed Time |              ms |        1,495.00 |        1,495.00 |        1,495.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,972,998.97 |    7,972,998.97 |    7,972,998.97 |            0.00 |
|TotalCollections [Gen0] |     collections |           32.79 |           32.79 |           32.79 |            0.00 |
|TotalCollections [Gen1] |     collections |           14.05 |           14.05 |           14.05 |            0.00 |
|TotalCollections [Gen2] |     collections |            5.35 |            5.35 |            5.35 |            0.00 |
|    Elapsed Time |              ms |        1,000.46 |        1,000.46 |        1,000.46 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           39.48 |           39.48 |           39.48 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   11,914,160.00 |    7,972,998.97 |          125.42 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           49.00 |           32.79 |   30,496,193.88 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           21.00 |           14.05 |   71,157,785.71 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            8.00 |            5.35 |  186,789,187.50 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,495.00 |        1,000.46 |      999,540.80 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           39.48 |   25,327,347.46 |


