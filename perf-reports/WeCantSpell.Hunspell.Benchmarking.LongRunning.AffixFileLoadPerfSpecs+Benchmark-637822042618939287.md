# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_3/6/2022 10:57:41 PM_
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
|TotalBytesAllocated |           bytes |       43,528.00 |       43,528.00 |       43,528.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          109.00 |          109.00 |          109.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           38.00 |           38.00 |           38.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           14.00 |           14.00 |           14.00 |            0.00 |
|    Elapsed Time |              ms |        1,378.00 |        1,378.00 |        1,378.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          177.00 |          177.00 |          177.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |       31,594.06 |       31,594.06 |       31,594.06 |            0.00 |
|TotalCollections [Gen0] |     collections |           79.12 |           79.12 |           79.12 |            0.00 |
|TotalCollections [Gen1] |     collections |           27.58 |           27.58 |           27.58 |            0.00 |
|TotalCollections [Gen2] |     collections |           10.16 |           10.16 |           10.16 |            0.00 |
|    Elapsed Time |              ms |        1,000.20 |        1,000.20 |        1,000.20 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          128.47 |          128.47 |          128.47 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       43,528.00 |       31,594.06 |       31,651.52 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          109.00 |           79.12 |   12,639,699.08 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           38.00 |           27.58 |   36,255,978.95 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           14.00 |           10.16 |   98,409,085.71 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,378.00 |        1,000.20 |      999,802.03 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          177.00 |          128.47 |    7,783,769.49 |


