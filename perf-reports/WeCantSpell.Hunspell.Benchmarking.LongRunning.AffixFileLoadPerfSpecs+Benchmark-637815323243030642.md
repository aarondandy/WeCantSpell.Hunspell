# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_2/27/2022 4:18:44 AM_
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
|TotalBytesAllocated |           bytes |   13,603,560.00 |   13,603,560.00 |   13,603,560.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          133.00 |          133.00 |          133.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           32.00 |           32.00 |           32.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            9.00 |            9.00 |            9.00 |            0.00 |
|    Elapsed Time |              ms |        1,904.00 |        1,904.00 |        1,904.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,143,156.45 |    7,143,156.45 |    7,143,156.45 |            0.00 |
|TotalCollections [Gen0] |     collections |           69.84 |           69.84 |           69.84 |            0.00 |
|TotalCollections [Gen1] |     collections |           16.80 |           16.80 |           16.80 |            0.00 |
|TotalCollections [Gen2] |     collections |            4.73 |            4.73 |            4.73 |            0.00 |
|    Elapsed Time |              ms |          999.78 |          999.78 |          999.78 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           30.98 |           30.98 |           30.98 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   13,603,560.00 |    7,143,156.45 |          139.99 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          133.00 |           69.84 |   14,318,936.84 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           32.00 |           16.80 |   59,513,081.25 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            9.00 |            4.73 |  211,602,066.67 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,904.00 |          999.78 |    1,000,219.85 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           30.98 |   32,278,281.36 |


