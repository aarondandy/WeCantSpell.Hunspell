# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/12/2022 3:32:39 AM_
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
|TotalBytesAllocated |           bytes |   95,446,432.00 |   95,446,432.00 |   95,446,432.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          325.00 |          325.00 |          325.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          161.00 |          161.00 |          161.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           23.00 |           23.00 |           23.00 |            0.00 |
|    Elapsed Time |              ms |       11,596.00 |       11,596.00 |       11,596.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,230,983.50 |    8,230,983.50 |    8,230,983.50 |            0.00 |
|TotalCollections [Gen0] |     collections |           28.03 |           28.03 |           28.03 |            0.00 |
|TotalCollections [Gen1] |     collections |           13.88 |           13.88 |           13.88 |            0.00 |
|TotalCollections [Gen2] |     collections |            1.98 |            1.98 |            1.98 |            0.00 |
|    Elapsed Time |              ms |        1,000.00 |        1,000.00 |        1,000.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            5.09 |            5.09 |            5.09 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   95,446,432.00 |    8,230,983.50 |          121.49 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          325.00 |           28.03 |   35,679,980.31 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          161.00 |           13.88 |   72,024,804.97 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           23.00 |            1.98 |  504,173,634.78 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       11,596.00 |        1,000.00 |      999,999.45 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            5.09 |  196,542,264.41 |


