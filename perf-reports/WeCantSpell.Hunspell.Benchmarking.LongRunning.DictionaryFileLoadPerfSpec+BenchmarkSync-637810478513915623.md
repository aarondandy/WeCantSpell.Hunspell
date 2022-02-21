# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_2/21/2022 1:44:11 PM_
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
|TotalBytesAllocated |           bytes |  117,321,800.00 |  117,321,800.00 |  117,321,800.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          432.00 |          432.00 |          432.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          159.00 |          159.00 |          159.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           31.00 |           31.00 |           31.00 |            0.00 |
|    Elapsed Time |              ms |        9,468.00 |        9,468.00 |        9,468.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           50.00 |           50.00 |           50.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   12,391,492.79 |   12,391,492.79 |   12,391,492.79 |            0.00 |
|TotalCollections [Gen0] |     collections |           45.63 |           45.63 |           45.63 |            0.00 |
|TotalCollections [Gen1] |     collections |           16.79 |           16.79 |           16.79 |            0.00 |
|TotalCollections [Gen2] |     collections |            3.27 |            3.27 |            3.27 |            0.00 |
|    Elapsed Time |              ms |        1,000.01 |        1,000.01 |        1,000.01 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            5.28 |            5.28 |            5.28 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  117,321,800.00 |   12,391,492.79 |           80.70 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          432.00 |           45.63 |   21,916,507.18 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          159.00 |           16.79 |   59,546,736.48 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           31.00 |            3.27 |  305,417,132.26 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        9,468.00 |        1,000.01 |      999,992.72 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           50.00 |            5.28 |  189,358,622.00 |


