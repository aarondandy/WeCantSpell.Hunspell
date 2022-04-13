# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_4/13/2022 10:49:33 PM_
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
|TotalBytesAllocated |           bytes |  103,765,784.00 |  103,765,784.00 |  103,765,784.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          322.00 |          322.00 |          322.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          159.00 |          159.00 |          159.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           21.00 |           21.00 |           21.00 |            0.00 |
|    Elapsed Time |              ms |       11,266.00 |       11,266.00 |       11,266.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    9,210,524.44 |    9,210,524.44 |    9,210,524.44 |            0.00 |
|TotalCollections [Gen0] |     collections |           28.58 |           28.58 |           28.58 |            0.00 |
|TotalCollections [Gen1] |     collections |           14.11 |           14.11 |           14.11 |            0.00 |
|TotalCollections [Gen2] |     collections |            1.86 |            1.86 |            1.86 |            0.00 |
|    Elapsed Time |              ms |        1,000.00 |        1,000.00 |        1,000.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            5.24 |            5.24 |            5.24 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  103,765,784.00 |    9,210,524.44 |          108.57 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          322.00 |           28.58 |   34,987,582.92 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          159.00 |           14.11 |   70,855,356.60 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           21.00 |            1.86 |  536,476,271.43 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       11,266.00 |        1,000.00 |    1,000,000.15 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            5.24 |  190,949,181.36 |


