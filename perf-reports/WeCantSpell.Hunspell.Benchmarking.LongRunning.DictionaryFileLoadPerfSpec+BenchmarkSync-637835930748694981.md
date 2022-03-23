# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/23/2022 12:44:34 AM_
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
|TotalBytesAllocated |           bytes |  109,753,272.00 |  109,753,272.00 |  109,753,272.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          325.00 |          325.00 |          325.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          163.00 |          163.00 |          163.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           23.00 |           23.00 |           23.00 |            0.00 |
|    Elapsed Time |              ms |       13,765.00 |       13,765.00 |       13,765.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,973,373.40 |    7,973,373.40 |    7,973,373.40 |            0.00 |
|TotalCollections [Gen0] |     collections |           23.61 |           23.61 |           23.61 |            0.00 |
|TotalCollections [Gen1] |     collections |           11.84 |           11.84 |           11.84 |            0.00 |
|TotalCollections [Gen2] |     collections |            1.67 |            1.67 |            1.67 |            0.00 |
|    Elapsed Time |              ms |        1,000.00 |        1,000.00 |        1,000.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.29 |            4.29 |            4.29 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  109,753,272.00 |    7,973,373.40 |          125.42 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          325.00 |           23.61 |   42,353,764.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          163.00 |           11.84 |   84,447,688.96 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           23.00 |            1.67 |  598,477,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       13,765.00 |        1,000.00 |      999,998.06 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.29 |  233,304,632.20 |


