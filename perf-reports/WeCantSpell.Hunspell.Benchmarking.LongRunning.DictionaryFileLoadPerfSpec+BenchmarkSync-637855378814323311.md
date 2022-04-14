# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_4/14/2022 12:58:01 PM_
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
|TotalBytesAllocated |           bytes |  106,180,656.00 |  106,180,656.00 |  106,180,656.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          331.00 |          331.00 |          331.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          167.00 |          167.00 |          167.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           27.00 |           27.00 |           27.00 |            0.00 |
|    Elapsed Time |              ms |       13,645.00 |       13,645.00 |       13,645.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,781,411.15 |    7,781,411.15 |    7,781,411.15 |            0.00 |
|TotalCollections [Gen0] |     collections |           24.26 |           24.26 |           24.26 |            0.00 |
|TotalCollections [Gen1] |     collections |           12.24 |           12.24 |           12.24 |            0.00 |
|TotalCollections [Gen2] |     collections |            1.98 |            1.98 |            1.98 |            0.00 |
|    Elapsed Time |              ms |          999.97 |          999.97 |          999.97 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.32 |            4.32 |            4.32 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  106,180,656.00 |    7,781,411.15 |          128.51 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          331.00 |           24.26 |   41,224,846.53 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          167.00 |           12.24 |   81,709,126.95 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           27.00 |            1.98 |  505,386,081.48 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       13,645.00 |          999.97 |    1,000,031.09 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.32 |  231,278,376.27 |


