# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_2/27/2022 10:58:38 PM_
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
|TotalBytesAllocated |           bytes |  117,651,400.00 |  117,651,400.00 |  117,651,400.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          641.00 |          641.00 |          641.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          229.00 |          229.00 |          229.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           35.00 |           35.00 |           35.00 |            0.00 |
|    Elapsed Time |              ms |       15,892.00 |       15,892.00 |       15,892.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,403,225.08 |    7,403,225.08 |    7,403,225.08 |            0.00 |
|TotalCollections [Gen0] |     collections |           40.33 |           40.33 |           40.33 |            0.00 |
|TotalCollections [Gen1] |     collections |           14.41 |           14.41 |           14.41 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.20 |            2.20 |            2.20 |            0.00 |
|    Elapsed Time |              ms |        1,000.01 |        1,000.01 |        1,000.01 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            3.71 |            3.71 |            3.71 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  117,651,400.00 |    7,403,225.08 |          135.08 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          641.00 |           40.33 |   24,792,374.10 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          229.00 |           14.41 |   69,396,994.76 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           35.00 |            2.20 |  454,054,622.86 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       15,892.00 |        1,000.01 |      999,994.45 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.71 |  269,354,437.29 |


