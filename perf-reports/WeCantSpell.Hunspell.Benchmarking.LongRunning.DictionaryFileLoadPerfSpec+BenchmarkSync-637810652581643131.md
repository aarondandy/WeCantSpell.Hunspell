# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_2/21/2022 6:34:18 PM_
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
|TotalBytesAllocated |           bytes |  117,538,240.00 |  117,538,240.00 |  117,538,240.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          661.00 |          661.00 |          661.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          237.00 |          237.00 |          237.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           37.00 |           37.00 |           37.00 |            0.00 |
|    Elapsed Time |              ms |       14,377.00 |       14,377.00 |       14,377.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,175,557.06 |    8,175,557.06 |    8,175,557.06 |            0.00 |
|TotalCollections [Gen0] |     collections |           45.98 |           45.98 |           45.98 |            0.00 |
|TotalCollections [Gen1] |     collections |           16.48 |           16.48 |           16.48 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.57 |            2.57 |            2.57 |            0.00 |
|    Elapsed Time |              ms |        1,000.01 |        1,000.01 |        1,000.01 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.10 |            4.10 |            4.10 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  117,538,240.00 |    8,175,557.06 |          122.32 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          661.00 |           45.98 |   21,750,055.52 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          237.00 |           16.48 |   60,661,547.26 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           37.00 |            2.57 |  388,561,802.70 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       14,377.00 |        1,000.01 |      999,985.16 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.10 |  243,674,350.85 |


