# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_2/23/2022 3:55:25 AM_
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
|TotalBytesAllocated |           bytes |  117,033,136.00 |  117,033,136.00 |  117,033,136.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          696.00 |          696.00 |          696.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          240.00 |          240.00 |          240.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           39.00 |           39.00 |           39.00 |            0.00 |
|    Elapsed Time |              ms |       15,012.00 |       15,012.00 |       15,012.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,795,877.31 |    7,795,877.31 |    7,795,877.31 |            0.00 |
|TotalCollections [Gen0] |     collections |           46.36 |           46.36 |           46.36 |            0.00 |
|TotalCollections [Gen1] |     collections |           15.99 |           15.99 |           15.99 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.60 |            2.60 |            2.60 |            0.00 |
|    Elapsed Time |              ms |          999.99 |          999.99 |          999.99 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            3.93 |            3.93 |            3.93 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  117,033,136.00 |    7,795,877.31 |          128.27 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          696.00 |           46.36 |   21,569,228.30 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          240.00 |           15.99 |   62,550,762.08 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           39.00 |            2.60 |  384,927,766.67 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       15,012.00 |          999.99 |    1,000,012.18 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.93 |  254,443,777.97 |


