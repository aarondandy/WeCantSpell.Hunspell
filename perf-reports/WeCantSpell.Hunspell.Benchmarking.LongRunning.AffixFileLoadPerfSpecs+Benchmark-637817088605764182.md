# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_3/1/2022 5:21:00 AM_
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
|TotalBytesAllocated |           bytes |    5,271,816.00 |    5,271,816.00 |    5,271,816.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           71.00 |           71.00 |           71.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           28.00 |           28.00 |           28.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            9.00 |            9.00 |            9.00 |            0.00 |
|    Elapsed Time |              ms |        1,700.00 |        1,700.00 |        1,700.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,101,283.87 |    3,101,283.87 |    3,101,283.87 |            0.00 |
|TotalCollections [Gen0] |     collections |           41.77 |           41.77 |           41.77 |            0.00 |
|TotalCollections [Gen1] |     collections |           16.47 |           16.47 |           16.47 |            0.00 |
|TotalCollections [Gen2] |     collections |            5.29 |            5.29 |            5.29 |            0.00 |
|    Elapsed Time |              ms |        1,000.07 |        1,000.07 |        1,000.07 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           34.71 |           34.71 |           34.71 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    5,271,816.00 |    3,101,283.87 |          322.45 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           71.00 |           41.77 |   23,941,997.18 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           28.00 |           16.47 |   60,710,064.29 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            9.00 |            5.29 |  188,875,755.56 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,700.00 |        1,000.07 |      999,930.47 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           34.71 |   28,811,555.93 |


