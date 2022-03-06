# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/6/2022 10:59:11 PM_
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
|TotalBytesAllocated |           bytes |  140,086,000.00 |  140,086,000.00 |  140,086,000.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          485.00 |          485.00 |          485.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          242.00 |          242.00 |          242.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           33.00 |           33.00 |           33.00 |            0.00 |
|    Elapsed Time |              ms |       14,444.00 |       14,444.00 |       14,444.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    9,699,040.81 |    9,699,040.81 |    9,699,040.81 |            0.00 |
|TotalCollections [Gen0] |     collections |           33.58 |           33.58 |           33.58 |            0.00 |
|TotalCollections [Gen1] |     collections |           16.76 |           16.76 |           16.76 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.28 |            2.28 |            2.28 |            0.00 |
|    Elapsed Time |              ms |        1,000.05 |        1,000.05 |        1,000.05 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.08 |            4.08 |            4.08 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  140,086,000.00 |    9,699,040.81 |          103.10 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          485.00 |           33.58 |   29,779,966.80 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          242.00 |           16.76 |   59,682,991.32 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           33.00 |            2.28 |  437,675,269.70 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       14,444.00 |        1,000.05 |      999,950.42 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.08 |  244,801,422.03 |


