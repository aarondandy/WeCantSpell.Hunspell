# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_2/21/2022 11:44:18 PM_
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
|TotalBytesAllocated |           bytes |   28,945,680.00 |   28,945,680.00 |   28,945,680.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          667.00 |          667.00 |          667.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          246.00 |          246.00 |          246.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           42.00 |           42.00 |           42.00 |            0.00 |
|    Elapsed Time |              ms |       14,573.00 |       14,573.00 |       14,573.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,986,259.59 |    1,986,259.59 |    1,986,259.59 |            0.00 |
|TotalCollections [Gen0] |     collections |           45.77 |           45.77 |           45.77 |            0.00 |
|TotalCollections [Gen1] |     collections |           16.88 |           16.88 |           16.88 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.88 |            2.88 |            2.88 |            0.00 |
|    Elapsed Time |              ms |        1,000.00 |        1,000.00 |        1,000.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.05 |            4.05 |            4.05 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   28,945,680.00 |    1,986,259.59 |          503.46 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          667.00 |           45.77 |   21,848,514.54 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          246.00 |           16.88 |   59,239,671.54 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           42.00 |            2.88 |  346,975,219.05 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       14,573.00 |        1,000.00 |      999,997.20 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.05 |  246,999,308.47 |


