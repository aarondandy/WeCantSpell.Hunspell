# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_3/5/2022 7:43:22 AM_
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
|TotalBytesAllocated |           bytes |   10,608,072.00 |   10,608,072.00 |   10,608,072.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           41.00 |           41.00 |           41.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           14.00 |           14.00 |           14.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            6.00 |            6.00 |            6.00 |            0.00 |
|    Elapsed Time |              ms |        1,433.00 |        1,433.00 |        1,433.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,400,862.91 |    7,400,862.91 |    7,400,862.91 |            0.00 |
|TotalCollections [Gen0] |     collections |           28.60 |           28.60 |           28.60 |            0.00 |
|TotalCollections [Gen1] |     collections |            9.77 |            9.77 |            9.77 |            0.00 |
|TotalCollections [Gen2] |     collections |            4.19 |            4.19 |            4.19 |            0.00 |
|    Elapsed Time |              ms |          999.75 |          999.75 |          999.75 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           41.16 |           41.16 |           41.16 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   10,608,072.00 |    7,400,862.91 |          135.12 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           41.00 |           28.60 |   34,959,904.88 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           14.00 |            9.77 |  102,382,578.57 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            6.00 |            4.19 |  238,892,683.33 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,433.00 |          999.75 |    1,000,248.50 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           41.16 |   24,294,171.19 |


