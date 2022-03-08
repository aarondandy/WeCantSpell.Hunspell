# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/8/2022 5:22:24 AM_
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
|TotalBytesAllocated |           bytes |  115,456,384.00 |  115,456,384.00 |  115,456,384.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          409.00 |          409.00 |          409.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          191.00 |          191.00 |          191.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           28.00 |           28.00 |           28.00 |            0.00 |
|    Elapsed Time |              ms |       13,315.00 |       13,315.00 |       13,315.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,670,870.70 |    8,670,870.70 |    8,670,870.70 |            0.00 |
|TotalCollections [Gen0] |     collections |           30.72 |           30.72 |           30.72 |            0.00 |
|TotalCollections [Gen1] |     collections |           14.34 |           14.34 |           14.34 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.10 |            2.10 |            2.10 |            0.00 |
|    Elapsed Time |              ms |          999.97 |          999.97 |          999.97 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.43 |            4.43 |            4.43 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  115,456,384.00 |    8,670,870.70 |          115.33 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          409.00 |           30.72 |   32,556,066.99 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          191.00 |           14.34 |   69,714,300.52 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           28.00 |            2.10 |  475,551,121.43 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       13,315.00 |          999.97 |    1,000,032.40 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.43 |  225,685,277.97 |


