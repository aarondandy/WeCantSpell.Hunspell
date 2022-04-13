# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_4/13/2022 11:06:26 PM_
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
|TotalBytesAllocated |           bytes |  133,655,096.00 |  133,655,096.00 |  133,655,096.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          330.00 |          330.00 |          330.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          168.00 |          168.00 |          168.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           27.00 |           27.00 |           27.00 |            0.00 |
|    Elapsed Time |              ms |       13,417.00 |       13,417.00 |       13,417.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    9,961,828.98 |    9,961,828.98 |    9,961,828.98 |            0.00 |
|TotalCollections [Gen0] |     collections |           24.60 |           24.60 |           24.60 |            0.00 |
|TotalCollections [Gen1] |     collections |           12.52 |           12.52 |           12.52 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.01 |            2.01 |            2.01 |            0.00 |
|    Elapsed Time |              ms |        1,000.02 |        1,000.02 |        1,000.02 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.40 |            4.40 |            4.40 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  133,655,096.00 |    9,961,828.98 |          100.38 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          330.00 |           24.60 |   40,656,735.15 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          168.00 |           12.52 |   79,861,444.05 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           27.00 |            2.01 |  496,915,651.85 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       13,417.00 |        1,000.02 |      999,979.32 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.40 |  227,402,077.97 |


