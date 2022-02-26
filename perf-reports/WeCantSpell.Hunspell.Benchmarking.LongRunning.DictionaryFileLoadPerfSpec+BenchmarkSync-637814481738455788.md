# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_2/26/2022 4:56:13 AM_
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
|TotalBytesAllocated |           bytes |  115,373,456.00 |  115,373,456.00 |  115,373,456.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          704.00 |          704.00 |          704.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          249.00 |          249.00 |          249.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           46.00 |           46.00 |           46.00 |            0.00 |
|    Elapsed Time |              ms |       15,356.00 |       15,356.00 |       15,356.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,513,120.80 |    7,513,120.80 |    7,513,120.80 |            0.00 |
|TotalCollections [Gen0] |     collections |           45.84 |           45.84 |           45.84 |            0.00 |
|TotalCollections [Gen1] |     collections |           16.21 |           16.21 |           16.21 |            0.00 |
|TotalCollections [Gen2] |     collections |            3.00 |            3.00 |            3.00 |            0.00 |
|    Elapsed Time |              ms |          999.98 |          999.98 |          999.98 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            3.84 |            3.84 |            3.84 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  115,373,456.00 |    7,513,120.80 |          133.10 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          704.00 |           45.84 |   21,812,873.01 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          249.00 |           16.21 |   61,671,737.35 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           46.00 |            3.00 |  333,831,795.65 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       15,356.00 |          999.98 |    1,000,017.10 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.84 |  260,275,637.29 |


