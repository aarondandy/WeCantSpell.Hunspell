# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_3/7/2022 4:52:50 AM_
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
|TotalBytesAllocated |           bytes |    8,836,360.00 |    8,836,360.00 |    8,836,360.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           99.00 |           99.00 |           99.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           38.00 |           38.00 |           38.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           10.00 |           10.00 |           10.00 |            0.00 |
|    Elapsed Time |              ms |        1,462.00 |        1,462.00 |        1,462.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          177.00 |          177.00 |          177.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,044,039.66 |    6,044,039.66 |    6,044,039.66 |            0.00 |
|TotalCollections [Gen0] |     collections |           67.72 |           67.72 |           67.72 |            0.00 |
|TotalCollections [Gen1] |     collections |           25.99 |           25.99 |           25.99 |            0.00 |
|TotalCollections [Gen2] |     collections |            6.84 |            6.84 |            6.84 |            0.00 |
|    Elapsed Time |              ms |        1,000.00 |        1,000.00 |        1,000.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          121.07 |          121.07 |          121.07 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    8,836,360.00 |    6,044,039.66 |          165.45 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           99.00 |           67.72 |   14,767,633.33 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           38.00 |           25.99 |   38,473,571.05 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           10.00 |            6.84 |  146,199,570.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,462.00 |        1,000.00 |      999,997.06 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          177.00 |          121.07 |    8,259,862.71 |


