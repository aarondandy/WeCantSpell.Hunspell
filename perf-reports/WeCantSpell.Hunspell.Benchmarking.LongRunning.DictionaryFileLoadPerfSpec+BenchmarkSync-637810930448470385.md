# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_2/22/2022 2:17:24 AM_
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
|TotalBytesAllocated |           bytes |  117,401,560.00 |  117,401,560.00 |  117,401,560.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          662.00 |          662.00 |          662.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          237.00 |          237.00 |          237.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           38.00 |           38.00 |           38.00 |            0.00 |
|    Elapsed Time |              ms |       14,352.00 |       14,352.00 |       14,352.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,179,780.43 |    8,179,780.43 |    8,179,780.43 |            0.00 |
|TotalCollections [Gen0] |     collections |           46.12 |           46.12 |           46.12 |            0.00 |
|TotalCollections [Gen1] |     collections |           16.51 |           16.51 |           16.51 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.65 |            2.65 |            2.65 |            0.00 |
|    Elapsed Time |              ms |          999.95 |          999.95 |          999.95 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.11 |            4.11 |            4.11 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  117,401,560.00 |    8,179,780.43 |          122.25 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          662.00 |           46.12 |   21,680,746.53 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          237.00 |           16.51 |   60,559,722.36 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           38.00 |            2.65 |  377,701,426.32 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       14,352.00 |          999.95 |    1,000,045.58 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.11 |  243,265,325.42 |


