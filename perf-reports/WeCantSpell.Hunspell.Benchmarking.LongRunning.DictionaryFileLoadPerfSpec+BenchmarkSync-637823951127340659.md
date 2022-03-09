# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/9/2022 3:58:32 AM_
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
|TotalBytesAllocated |           bytes |  103,579,408.00 |  103,579,408.00 |  103,579,408.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          335.00 |          335.00 |          335.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          171.00 |          171.00 |          171.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           30.00 |           30.00 |           30.00 |            0.00 |
|    Elapsed Time |              ms |       11,790.00 |       11,790.00 |       11,790.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,784,800.09 |    8,784,800.09 |    8,784,800.09 |            0.00 |
|TotalCollections [Gen0] |     collections |           28.41 |           28.41 |           28.41 |            0.00 |
|TotalCollections [Gen1] |     collections |           14.50 |           14.50 |           14.50 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.54 |            2.54 |            2.54 |            0.00 |
|    Elapsed Time |              ms |          999.94 |          999.94 |          999.94 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            5.00 |            5.00 |            5.00 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  103,579,408.00 |    8,784,800.09 |          113.83 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          335.00 |           28.41 |   35,196,277.61 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          171.00 |           14.50 |   68,951,771.93 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           30.00 |            2.54 |  393,025,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       11,790.00 |          999.94 |    1,000,063.87 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            5.00 |  199,843,271.19 |


