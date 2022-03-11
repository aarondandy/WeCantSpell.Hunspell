# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/11/2022 11:22:45 PM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.3,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |  105,286,592.00 |  105,286,592.00 |  105,286,592.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          325.00 |          325.00 |          325.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          161.00 |          161.00 |          161.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           23.00 |           23.00 |           23.00 |            0.00 |
|    Elapsed Time |              ms |       11,534.00 |       11,534.00 |       11,534.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    9,127,979.35 |    9,127,979.35 |    9,127,979.35 |            0.00 |
|TotalCollections [Gen0] |     collections |           28.18 |           28.18 |           28.18 |            0.00 |
|TotalCollections [Gen1] |     collections |           13.96 |           13.96 |           13.96 |            0.00 |
|TotalCollections [Gen2] |     collections |            1.99 |            1.99 |            1.99 |            0.00 |
|    Elapsed Time |              ms |          999.96 |          999.96 |          999.96 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            5.12 |            5.12 |            5.12 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  105,286,592.00 |    9,127,979.35 |          109.55 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          325.00 |           28.18 |   35,490,740.31 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          161.00 |           13.96 |   71,642,798.76 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           23.00 |            1.99 |  501,499,591.30 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       11,534.00 |          999.96 |    1,000,042.54 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            5.12 |  195,499,840.68 |


