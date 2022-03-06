# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_3/6/2022 1:13:12 AM_
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
|TotalBytesAllocated |           bytes |   10,643,216.00 |   10,643,216.00 |   10,643,216.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           41.00 |           41.00 |           41.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           14.00 |           14.00 |           14.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            6.00 |            6.00 |            6.00 |            0.00 |
|    Elapsed Time |              ms |        1,371.00 |        1,371.00 |        1,371.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,761,580.86 |    7,761,580.86 |    7,761,580.86 |            0.00 |
|TotalCollections [Gen0] |     collections |           29.90 |           29.90 |           29.90 |            0.00 |
|TotalCollections [Gen1] |     collections |           10.21 |           10.21 |           10.21 |            0.00 |
|TotalCollections [Gen2] |     collections |            4.38 |            4.38 |            4.38 |            0.00 |
|    Elapsed Time |              ms |          999.80 |          999.80 |          999.80 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           43.03 |           43.03 |           43.03 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   10,643,216.00 |    7,761,580.86 |          128.84 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           41.00 |           29.90 |   33,445,587.80 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           14.00 |           10.21 |   97,947,792.86 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            6.00 |            4.38 |  228,544,850.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,371.00 |          999.80 |    1,000,196.28 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           43.03 |   23,241,849.15 |


