# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/8/2022 4:54:17 AM_
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
|TotalBytesAllocated |           bytes |  107,678,008.00 |  107,678,008.00 |  107,678,008.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          419.00 |          419.00 |          419.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          200.00 |          200.00 |          200.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           36.00 |           36.00 |           36.00 |            0.00 |
|    Elapsed Time |              ms |       14,148.00 |       14,148.00 |       14,148.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,610,643.53 |    7,610,643.53 |    7,610,643.53 |            0.00 |
|TotalCollections [Gen0] |     collections |           29.61 |           29.61 |           29.61 |            0.00 |
|TotalCollections [Gen1] |     collections |           14.14 |           14.14 |           14.14 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.54 |            2.54 |            2.54 |            0.00 |
|    Elapsed Time |              ms |          999.98 |          999.98 |          999.98 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.17 |            4.17 |            4.17 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  107,678,008.00 |    7,610,643.53 |          131.39 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          419.00 |           29.61 |   33,766,932.46 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |           14.14 |   70,741,723.50 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           36.00 |            2.54 |  393,009,575.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       14,148.00 |          999.98 |    1,000,024.36 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.17 |  239,802,452.54 |


