# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/6/2022 2:08:07 AM_
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
|TotalBytesAllocated |           bytes |  139,446,704.00 |  139,446,704.00 |  139,446,704.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          490.00 |          490.00 |          490.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          241.00 |          241.00 |          241.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           37.00 |           37.00 |           37.00 |            0.00 |
|    Elapsed Time |              ms |       14,607.00 |       14,607.00 |       14,607.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    9,546,603.33 |    9,546,603.33 |    9,546,603.33 |            0.00 |
|TotalCollections [Gen0] |     collections |           33.55 |           33.55 |           33.55 |            0.00 |
|TotalCollections [Gen1] |     collections |           16.50 |           16.50 |           16.50 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.53 |            2.53 |            2.53 |            0.00 |
|    Elapsed Time |              ms |        1,000.00 |        1,000.00 |        1,000.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.04 |            4.04 |            4.04 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  139,446,704.00 |    9,546,603.33 |          104.75 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          490.00 |           33.55 |   29,810,090.61 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          241.00 |           16.50 |   60,609,727.80 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           37.00 |            2.53 |  394,782,281.08 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       14,607.00 |        1,000.00 |      999,996.19 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.04 |  247,575,328.81 |


